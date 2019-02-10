using System;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image1DArray
    {
        #region Create

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image1DArray<T> Create<T>(Context context, ImageChannelOrder channelOrder, bool normalized,
            int width, int length,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return Create<T>(context, channelOrder, normalized,
                (ulong) width, (ulong) length, deviceFlags, hostFlags, usePinnedMemory);
        }

        public static Image1DArray<T> Create<T>(Context context, ImageChannelOrder channelOrder, bool normalized,
            ulong width, ulong length,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            var format = new ImageFormatBuilder()
                .WithDataType(typeof(T))
                .WithNormalized(normalized)
                .WithChannelOrder(channelOrder)
                .Build();

            return Create<T>(context, format, width, length, deviceFlags, hostFlags, usePinnedMemory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image1DArray<T> Create<T>(Context context, ImageFormat format,
            int width, int length,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return Create<T>(context, format, (ulong) width, (ulong) length, deviceFlags, hostFlags, usePinnedMemory);
        }

        public static unsafe Image1DArray<T> Create<T>(Context context, ImageFormat format,
            ulong width, ulong length,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var lib = context.Library;
            var flags = deviceFlags.ToDeviceNativeFlags() | hostFlags.ToHostNativeFlags();

            if (usePinnedMemory ?? hostFlags != 0)
                flags |= MemFlags.MemAllocHostPtr;

            var description = ImageDescription.Create1DArray(width, length);

            ErrorCode errorCode;
            var id = lib.clCreateImageUnsafe(context, flags, &format, &description, null, &errorCode);
            errorCode.HandleError();

            return FromId<T>(lib, id);
        }

        #endregion
    }
}
