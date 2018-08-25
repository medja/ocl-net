using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace OCL.Net
{
    public unsafe class SafeNativeMemory<T> : MemoryManager<T>, IEnumerable<T>
        where T : unmanaged
    {
        public T this[long i]
        {
            get
            {
                if (i < 0 || i >= _memory.Length)
                    throw new IndexOutOfRangeException();

                return _memory[i];
            }
            set
            {
                if (i < 0 || i >= _memory.Length)
                    throw new IndexOutOfRangeException();

                _memory[i] = value;
            }
        }

        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _memory.Length;
        }

        public long LongLength
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _memory.LongLength;
        }

        public long Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _memory.Size;
        }

        public Span<T> Span
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _memory.Span;
        }

        public T* Pointer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _memory.Pointer;
        }

        public IntPtr Handle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _memory.Handle;
        }

        private readonly NativeMemory<T> _memory;

        public SafeNativeMemory(long length) : this(new NativeMemory<T>(length))
        { }

        public SafeNativeMemory(T[] array) : this(new NativeMemory<T>(array))
        { }

        public SafeNativeMemory(IList<T> list) : this(new NativeMemory<T>(list))
        { }

        public SafeNativeMemory(ICollection<T> collection) : this(new NativeMemory<T>(collection))
        { }

        public SafeNativeMemory(IEnumerable<T> enumerable) : this(new NativeMemory<T>(enumerable))
        { }

        public SafeNativeMemory(NativeMemory<T> memory)
        {
            _memory = memory;
        }

        public sealed override Span<T> GetSpan()
        {
            return _memory.GetSpan();
        }

        public sealed override MemoryHandle Pin(int elementIndex = 0)
        {
            return Pin(elementIndex);
        }

        public MemoryHandle Pin(long elementIndex = 0)
        {
            return _memory.Pin(elementIndex);
        }

        public sealed override void Unpin()
        {
            _memory.Unpin();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NativeMemoryEnumerator<T> GetEnumerator()
        {
            return _memory.GetEnumerator();
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
            _memory.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _memory.Dispose();
        }

        public static implicit operator SafeNativeMemory<T>(NativeMemory<T> nativeMemory)
        {
            return new SafeNativeMemory<T>(nativeMemory);
        }

        public static implicit operator NativeMemory<T>(SafeNativeMemory<T> safeNativeMemory)
        {
            return safeNativeMemory._memory;
        }
    }
}
