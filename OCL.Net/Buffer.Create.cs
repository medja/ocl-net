using System;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public partial class Buffer
    {
        #region Create

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Buffer<T> Create<T>(Context context, int length,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            return Create<T>(context, (ulong) length, deviceFlags, hostFlags, usePinnedMemory);
        }

        public static unsafe Buffer<T> Create<T>(Context context, ulong length,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags,
            bool? usePinnedMemory = null)
            where T : unmanaged
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var lib = context.Library;

            var size = new UIntPtr(length * (ulong) sizeof(T));
            var flags = deviceFlags.ToDeviceNativeFlags() | hostFlags.ToHostNativeFlags();

            if (usePinnedMemory ?? hostFlags != 0)
                flags |= MemFlags.MemAllocHostPtr;

            ErrorCode errorCode;
            var id = lib.clCreateBufferUnsafe(context, flags, size, null, &errorCode);
            errorCode.HandleError();

            return FromId<T>(lib, id);
        }

        #endregion
    }
}
