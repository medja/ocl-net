using System;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image3D
    {
        #region FromMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image3D<T> FromMemory<T>(Context context, Memory<T> memory,
            ImageChannelOrder channelOrder, bool normalized,
            int width, int height, int depth = 0, int rowPitch = 0, int slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return FromMemory(context, memory, channelOrder, normalized,
                (ulong) width, (ulong) height, (ulong) depth, (ulong) rowPitch, (ulong) slicePitch,
                deviceFlags, hostFlags, usePinnedMemory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image3D<T> FromMemory<T>(Context context, Memory<T> memory,
            ImageChannelOrder channelOrder, bool normalized,
            ulong width, ulong height, ulong depth = 0, ulong rowPitch = 0, ulong slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return From(context, memory.Span, channelOrder, normalized,
                width, height, depth, rowPitch, slicePitch, deviceFlags, hostFlags, usePinnedMemory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image3D<T> FromMemory<T>(Context context, Memory<T> memory, ImageFormat format,
            int width, int height, int depth = 0, int rowPitch = 0, int slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return FromMemory(context, memory, format,
                (ulong) width, (ulong) height, (ulong) depth, (ulong) rowPitch, (ulong) slicePitch,
                deviceFlags, hostFlags, usePinnedMemory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image3D<T> FromMemory<T>(Context context, Memory<T> memory, ImageFormat format,
            ulong width, ulong height, ulong depth = 0, ulong rowPitch = 0, ulong slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return From(context, memory.Span, format,
                width, height, depth, rowPitch, slicePitch, deviceFlags, hostFlags, usePinnedMemory);
        }

        #endregion

        #region From

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image3D<T> From<T>(Context context, Span<T> span,
            ImageChannelOrder channelOrder, bool normalized,
            int width, int height, int depth = 0, int rowPitch = 0, int slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return From(context, span, channelOrder, normalized,
                (ulong) width, (ulong) height, (ulong) depth, (ulong) rowPitch, (ulong) slicePitch,
                deviceFlags, hostFlags, usePinnedMemory);
        }

        public static Image3D<T> From<T>(Context context, Span<T> span,
            ImageChannelOrder channelOrder, bool normalized,
            ulong width, ulong height, ulong depth = 0, ulong rowPitch = 0, ulong slicePitch = 0,
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
                width, height, depth, rowPitch, slicePitch, deviceFlags, hostFlags, usePinnedMemory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image3D<T> From<T>(Context context, Span<T> span, ImageFormat format,
            int width, int height, int depth = 0, int rowPitch = 0, int slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return From(context, span, format,
                (ulong) width, (ulong) height, (ulong) depth, (ulong) rowPitch, (ulong) slicePitch,
                deviceFlags, hostFlags, usePinnedMemory);
        }

        public static unsafe Image3D<T> From<T>(Context context, Span<T> span, ImageFormat format,
            ulong width, ulong height, ulong depth = 0, ulong rowPitch = 0, ulong slicePitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (rowPitch == 0)
                rowPitch = width * (ulong) format.ElementSize;

            if (slicePitch == 0)
                slicePitch = height * rowPitch;

            if (depth == 0)
                depth = (ulong) span.Length * (ulong) sizeof(T) / slicePitch;

            if (slicePitch * depth > (ulong) span.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(span), "Span capacity is too small");

            var lib = context.Library;
            var flags = deviceFlags.ToDeviceNativeFlags() | hostFlags.ToHostNativeFlags() | MemFlags.MemCopyHostPtr;

            if (usePinnedMemory ?? hostFlags != 0)
                flags |= MemFlags.MemAllocHostPtr;

            var description = ImageDescription.Create3D(width, height, depth, rowPitch, slicePitch);

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
