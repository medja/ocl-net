using System;
using System.Runtime.CompilerServices;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1DArray<T>
    {
        public unsafe class Memory : Buffer<T>.Memory
        {
            public T this[long index, long x, int channel]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => base[x * _channelCount + index * _rowPitch + channel];
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                set => base[x * _channelCount + index * _rowPitch + channel] = value;
            }

            public Span<T> this[long index, long x]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => new Span<T>(Pointer + x * _channelCount + index * _rowPitch, _channelCount);
            }

            public long Width
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _width;
            }

            public long ImageCount
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _imageCount;
            }

            public byte ChannelCount
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _channelCount;
            }

            private readonly long _width;
            private readonly long _imageCount;
            private readonly byte _channelCount;
            private readonly long _rowPitch;

            public Memory(T* ptr, long width, long imageCount, byte channelCount, long rowPitch,
                ImageId image, CommandQueueId queue, IOpenCl library)
                : base(ptr, rowPitch * imageCount / sizeof(T), image, queue, library)
            {
                _width = width;
                _imageCount = imageCount;
                _channelCount = channelCount;
                _rowPitch = rowPitch / sizeof(T);
            }
        }
    }
}
