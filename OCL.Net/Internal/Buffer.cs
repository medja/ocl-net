using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace OCL.Net.Internal
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public struct Buffer<T> where T : unmanaged
    {
        public const int DefaultCapacity = 64;
        private const uint MaxArraySize = 0x7fefffff;

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (int) _count;
        }

        private uint _count;
        private uint _capacity;

        private uint _bufferCount;
        private T[][] _buffers;

        private uint _offset;
        private T[] _lastBuffer;

        public Buffer(int capacity = DefaultCapacity)
        {
            _offset = 0;
            _lastBuffer = new T[capacity];

            _count = 0;
            _capacity = (uint) capacity;

            _buffers = new T[4][];
            _buffers[0] = _lastBuffer;
            _bufferCount = 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T value)
        {
            if (_count == _capacity)
                IncreaseCapacity();

            _lastBuffer[_offset] = value;

            _offset++;
            _count++;
        }

        public unsafe void CopyTo(T* destination)
        {
            if (_bufferCount == 0)
                return;

            long size;
            var offset = 0L;
            var limit = _bufferCount - 1;

            for (var i = 0; i < limit; i++)
            {
                var length = _buffers[i].Length;
                size = length * sizeof(T);

                fixed (T* source = _buffers[i])
                    Buffer.MemoryCopy(source, destination + offset, size, size);

                offset += length;
            }

            size = _offset * sizeof(T);

            fixed (T* source = _lastBuffer)
                Buffer.MemoryCopy(source, destination + offset, size, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void CopyTo(ref T destination)
        {
            fixed (T* ptr = &destination)
            {
                CopyTo(ptr);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void CopyTo(T[] destination)
        {
            fixed (T* ptr = destination)
            {
                CopyTo(ptr);
            }
        }

        private void IncreaseCapacity()
        {
            var capacity = (uint) _lastBuffer.Length * 2;

            if (capacity > MaxArraySize)
                capacity = MaxArraySize;

            _capacity += capacity;

            _offset = 0;
            _lastBuffer = new T[capacity];

            if (_bufferCount == _buffers.Length)
                Array.Resize(ref _buffers, _buffers.Length * 2);

            _buffers[_bufferCount++] = _lastBuffer;
        }

        public static Buffer<T> Create(int capacity = DefaultCapacity)
        {
            return new Buffer<T>(capacity);
        }
    }
}
