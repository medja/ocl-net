using System;
using System.Runtime.CompilerServices;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2DArray<T>
    {
        public unsafe class Memory : Buffer<T>.Memory
        {
            public T this[long index, long x, long y, int channel]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => base[x * _channelCount + y * _rowPitch + index * _slicePitch + channel];
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                set => base[x * _channelCount + y * _rowPitch + index * _slicePitch + channel] = value;
            }

            public Span<T> this[long index, long x, long y]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => new Span<T>(Pointer + x * _channelCount + y * _rowPitch + index * _slicePitch, _channelCount);
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
            private readonly long _height;
            private readonly long _imageCount;
            private readonly byte _channelCount;
            private readonly long _rowPitch;
            private readonly long _slicePitch;

            public Memory(T* ptr, long width, long height, long imageCount,
                byte channelCount, long rowPitch, long slicePitch,
                ImageId image, CommandQueueId queue, IOpenCl library)
                : base(ptr, slicePitch * imageCount / sizeof(T), image, queue, library)
            {
                _width = width;
                _height = height;
                _imageCount = imageCount;
                _channelCount = channelCount;
                _rowPitch = rowPitch / sizeof(T);
                _slicePitch = slicePitch / sizeof(T);
            }
        }
    }
}
