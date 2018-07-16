using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OCL.Net
{
    public unsafe partial class NativeMemory<T> : MemoryManager<T>
        where T : unmanaged
    {
        public T this[long i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _ptr[i];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _ptr[i] = value;
        }

        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (int) _length;
        }

        public long LongLength
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _length;
        }

        public long Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _length * sizeof(T);
        }

        public Span<T> Span
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSpan();
        }

        public T* Pointer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _ptr;
        }

        public IntPtr Handle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new IntPtr(_ptr);
        }

        private T* _ptr;
        private long _length;

        private uint _references;
        private bool _disposed;

        private readonly object _syncLock = new object();

        public NativeMemory(long length)
        {
            CreateFromLength(length);
        }

        public NativeMemory(T[] array)
        {
            CreateFromArray(array);
        }

        public NativeMemory(IList<T> list)
        {
            if (list is T[] array)
                CreateFromArray(array);
            else
                CreateFromList(list);
        }

        public NativeMemory(ICollection<T> collection)
        {
            switch (collection)
            {
                case T[] array:
                    CreateFromArray(array);
                    break;
                case IList<T> list:
                    CreateFromList(list);
                    break;
                default:
                    CreateFromCollection(collection);
                    break;
            }
        }

        public NativeMemory(IEnumerable<T> enumerable)
        {
            switch (enumerable)
            {
                case T[] array:
                    CreateFromArray(array);
                    break;
                case IList<T> list:
                    CreateFromList(list);
                    break;
                case ICollection<T> collection:
                    CreateFromCollection(collection);
                    break;
                default:
                    CreateFromEnumerable(enumerable);
                    break;
            }
        }

        ~NativeMemory()
        {
            Dispose(false);
        }

        public sealed override Span<T> GetSpan()
        {
            return new Span<T>(_ptr, (int) _length);
        }

        public sealed override MemoryHandle Pin(int elementIndex = 0)
        {
            return Pin(elementIndex);
        }

        public MemoryHandle Pin(long elementIndex = 0)
        {
            lock (_syncLock)
            {
                if (_disposed && _references == 0)
                    throw new ObjectDisposedException("NativeMemory is already disposed");

                _references++;
            }

            // pinnable should be this as the memory is reference counted
            return new MemoryHandle(_ptr + elementIndex, pinnable: this);
        }

        public sealed override void Unpin()
        {
            lock (_syncLock)
            {
                var shouldDispose = _disposed && _references == 1;

                if (_references > 0)
                    _references--;

                if (shouldDispose)
                    DisposeInternal();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected sealed override void Dispose(bool disposing)
        {
            if (_disposed || _ptr == null)
                return;

            _disposed = true;

            if (disposing)
            {
                lock (_syncLock)
                {
                    DisposeInternal();
                }
            }
            else
            {
                try
                {
                    DisposeInternal();
                }
                catch
                {
                    // ignored
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisposeInternal()
        {
            if (_ptr == null || _references > 0)
                return;

            var ptr = _ptr;
            var size = _length * sizeof(T);

            _ptr = null;
            _length = 0;

            Marshal.FreeHGlobal((IntPtr) ptr);
            GC.RemoveMemoryPressure(size);
        }

        public static implicit operator Memory<T>(NativeMemory<T> manager)
        {
            return manager.Memory;
        }

        public static implicit operator Span<T>(NativeMemory<T> manager)
        {
            return manager.Span;
        }
    }
}
