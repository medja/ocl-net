using System;
using System.Buffers;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public partial class Buffer
    {
        #region For

        public static Buffer<T> For<T>(Context context, MemoryManager<T> memoryManager,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            if (memoryManager == null)
                throw new ArgumentNullException(nameof(memoryManager));

            return ForInternal(context, memoryManager.Memory, memoryManager, deviceFlags, hostFlags);
        }

        public static Buffer<T> ForMemory<T>(Context context, Memory<T> memory,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return ForInternal(context, memory, null, deviceFlags, hostFlags);
        }

        private static unsafe Buffer<T> ForInternal<T>(Context context, Memory<T> memory, IDisposable disposable,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var lib = context.Library;

            var size = new UIntPtr((ulong) memory.Length * (ulong) sizeof(T));
            var flags = deviceFlags.ToDeviceNativeFlags() | hostFlags.ToHostNativeFlags() | MemFlags.MemUseHostPtr;

            var handle = memory.Pin();

            ErrorCode errorCode;
            var id = lib.clCreateBufferUnsafe(context, flags, size, handle.Pointer, &errorCode);
            errorCode.HandleError();

            return FromId<T>(lib, id, handle, disposable);
        }

        #endregion
    }
}
