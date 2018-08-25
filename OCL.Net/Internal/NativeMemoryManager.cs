using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace OCL.Net.Internal
{
    public unsafe class NativeMemoryManager<T> : MemoryManager<T>, IEnumerable<T>
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

        public uint ReferenceCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _references;
        }

        private T* _ptr;
        private long _length;

        private uint _references;
        private bool _disposed;

        protected readonly object SyncLock = new object();

        protected NativeMemoryManager(T* ptr, long length)
        {
            _ptr = ptr;
            _length = length;
        }

        ~NativeMemoryManager()
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
            lock (SyncLock)
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
            lock (SyncLock)
            {
                var shouldDispose = _disposed && _references == 1;

                if (_references > 0)
                    _references--;

                if (shouldDispose)
                    DisposeInternal();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NativeMemoryEnumerator<T> GetEnumerator()
        {
            return new NativeMemoryEnumerator<T>(_ptr, _length);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
                lock (SyncLock)
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

            OnDispose();

            _ptr = null;
            _length = 0;
        }

        protected virtual void OnDispose()
        { }

        public static implicit operator Memory<T>(NativeMemoryManager<T> manager)
        {
            return manager.Memory;
        }

        public static implicit operator Span<T>(NativeMemoryManager<T> manager)
        {
            return manager.Span;
        }
    }
}
