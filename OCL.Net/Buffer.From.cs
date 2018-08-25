using System;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public partial class Buffer
    {
        #region From

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Buffer<T> FromMemory<T>(Context context, Memory<T> memory,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return From(context, memory.Span, deviceFlags, hostFlags, usePinnedMemory);
        }

        public static unsafe Buffer<T> From<T>(Context context, Span<T> span,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (span == null)
                throw new ArgumentNullException(nameof(span));

            var lib = context.Library;

            var size = new UIntPtr((ulong) span.Length * (ulong) sizeof(T));
            var flags = deviceFlags.ToDeviceNativeFlags() | hostFlags.ToHostNativeFlags() | MemFlags.MemCopyHostPtr;

            if (usePinnedMemory ?? hostFlags != 0)
                flags |= MemFlags.MemAllocHostPtr;

            BufferId id;
            ErrorCode errorCode;

            fixed (T* ptr = span)
                id = lib.clCreateBufferUnsafe(context, flags, size, ptr, &errorCode);

            errorCode.HandleError();

            return FromId<T>(lib, id);
        }

        #endregion
    }
}
