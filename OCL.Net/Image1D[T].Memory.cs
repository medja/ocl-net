using System;
using System.Runtime.CompilerServices;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1D<T>
    {
        public unsafe class Memory : Buffer<T>.Memory
        {
            public T this[long x, int channel]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => base[x * _channelCount + channel];
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                set => base[x * _channelCount + channel] = value;
            }

            public new Span<T> this[long x]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => new Span<T>(Pointer + x * _channelCount, _channelCount);
            }

            public long Width
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _width;
            }

            public byte ChannelCount
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _channelCount;
            }

            private readonly long _width;
            private readonly byte _channelCount;

            public Memory(T* ptr, long width, byte channelCount, ImageId image, CommandQueueId queue, IOpenCl library)
                : base(ptr, width * channelCount, image, queue, library)
            {
                _width = width;
                _channelCount = channelCount;
            }
        }
    }
}
