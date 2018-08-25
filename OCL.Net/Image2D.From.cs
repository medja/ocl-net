using System;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image2D
    {
        #region FromMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image2D<T> FromMemory<T>(Context context, Memory<T> memory,
            ImageChannelOrder channelOrder, bool normalized,
            int width, int height = 0, int rowPitch = 0, int slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return FromMemory(context, memory, channelOrder, normalized,
                (ulong) width, (ulong) height, (ulong) rowPitch, (ulong) slicePitch,
                deviceFlags, hostFlags, usePinnedMemory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image2D<T> FromMemory<T>(Context context, Memory<T> memory,
            ImageChannelOrder channelOrder, bool normalized,
            ulong width, ulong height = 0, ulong rowPitch = 0, ulong slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return From(context, memory.Span, channelOrder, normalized,
                width, height, rowPitch, slicePitch, deviceFlags, hostFlags, usePinnedMemory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image2D<T> FromMemory<T>(Context context, Memory<T> memory, ImageFormat format,
            int width, int height = 0, int rowPitch = 0, int slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return FromMemory(context, memory, format,
                (ulong) width, (ulong) height, (ulong) rowPitch, (ulong) slicePitch,
                deviceFlags, hostFlags, usePinnedMemory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image2D<T> FromMemory<T>(Context context, Memory<T> memory, ImageFormat format,
            ulong width, ulong height = 0, ulong rowPitch = 0, ulong slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return From(context, memory.Span, format,
                width, height, rowPitch, slicePitch, deviceFlags, hostFlags, usePinnedMemory);
        }

        #endregion

        #region From

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image2D<T> From<T>(Context context, Span<T> span,
            ImageChannelOrder channelOrder, bool normalized,
            int width, int height = 0, int rowPitch = 0, int slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return From(context, span, channelOrder, normalized,
                (ulong) width, (ulong) height, (ulong) rowPitch, (ulong) slicePitch,
                deviceFlags, hostFlags, usePinnedMemory);
        }

        public static Image2D<T> From<T>(Context context, Span<T> span,
            ImageChannelOrder channelOrder, bool normalized,
            ulong width, ulong height = 0, ulong rowPitch = 0, ulong slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            var format = new ImageFormatBuilder()
                .WithDataType(typeof(T))
                .WithNormalized(normalized)
                .WithChannelOrder(channelOrder)
                .Build();

            return From(context, span, format,
                width, height, rowPitch, slicePitch, deviceFlags, hostFlags, usePinnedMemory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image2D<T> From<T>(Context context, Span<T> span, ImageFormat format,
            int width, int height = 0, int rowPitch = 0, int slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return From(context, span, format,
                (ulong) width, (ulong) height, (ulong) rowPitch, (ulong) slicePitch,
                deviceFlags, hostFlags, usePinnedMemory);
        }

        public static unsafe Image2D<T> From<T>(Context context, Span<T> span, ImageFormat format,
            ulong width, ulong height = 0, ulong rowPitch = 0, ulong slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (rowPitch == 0)
                rowPitch = width * (ulong) format.ElementSize;

            if (slicePitch == 0)
                slicePitch = (ulong) span.Length * (ulong) sizeof(T);

            if (height == 0)
                height = slicePitch / rowPitch;

            if (slicePitch > (ulong) span.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(span), "Span capacity is too small");

            var lib = context.Library;
            var flags = deviceFlags.ToDeviceNativeFlags() | hostFlags.ToHostNativeFlags() | MemFlags.MemCopyHostPtr;

            if (usePinnedMemory ?? hostFlags != 0)
                flags |= MemFlags.MemAllocHostPtr;

            var description = ImageDescription.Create2D(width, height, rowPitch, slicePitch);

            ImageId id;
            ErrorCode errorCode;

            fixed (T* ptr = span)
                id = lib.clCreateImageUnsafe(context, flags, &format, &description, ptr, &errorCode);

            errorCode.HandleError();

            return FromId<T>(lib, id);
        }

        #endregion
    }
}
