using System;
using System.Runtime.CompilerServices;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image3D<T>
    {
        public unsafe class Memory : Buffer<T>.Memory
        {
            public T this[long x, long y, long z, int channel]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => base[x * _channelCount + y * _rowPitch + z * _slicePitch + channel];
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                set => base[x * _channelCount + y * _rowPitch + z * _slicePitch + channel] = value;
            }

            public Span<T> this[long x, long y, long z]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => new Span<T>(Pointer + x * _channelCount + y * _rowPitch + z * _slicePitch, _channelCount);
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

            public long Depth
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _depth;
            }

            public byte ChannelCount
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _channelCount;
            }

            private readonly long _width;
            private readonly long _height;
            private readonly long _depth;
            private readonly byte _channelCount;
            private readonly long _rowPitch;
            private readonly long _slicePitch;

            public Memory(T* ptr, long width, long height, long depth,
                byte channelCount, long rowPitch, long slicePitch, ImageId image, CommandQueueId queue, IOpenCl library)
                : base(ptr, slicePitch * depth / sizeof(T), image, queue, library)
            {
                _width = width;
                _height = height;
                _depth = depth;
                _channelCount = channelCount;
                _rowPitch = rowPitch / sizeof(T);
                _slicePitch = slicePitch / sizeof(T);
            }
        }
    }
}
