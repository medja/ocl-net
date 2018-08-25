using System;
using System.Runtime.CompilerServices;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2D<T>
    {
        public unsafe class Memory : Buffer<T>.Memory
        {
            public T this[long x, long y, int channel]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => base[x * _channelCount + y * _rowPitch + channel];
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                set => base[x * _channelCount + y * _rowPitch + channel] = value;
            }

            public Span<T> this[long x, long y]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => new Span<T>(Pointer + x * _channelCount + y * _rowPitch, _channelCount);
            }

            public long Width
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _width;
            }

            public long Height
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _height;
            }

            public byte ChannelCount
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _channelCount;
            }

            private readonly long _width;
            private readonly long _height;
            private readonly byte _channelCount;
            private readonly long _rowPitch;

            public Memory(T* ptr, long width, long height, byte channelCount, long rowPitch,
                ImageId image, CommandQueueId queue, IOpenCl library)
                : base(ptr, rowPitch * height / sizeof(T), image, queue, library)
            {
                _width = width;
                _height = height;
                _channelCount = channelCount;
                _rowPitch = rowPitch / sizeof(T);
            }
        }
    }
}
