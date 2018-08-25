using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Buffer<T> CreateSubBuffer(int origin, int length,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
        {
            return CreateSubBuffer((ulong) origin, (ulong) length, deviceFlags, hostFlags);
        }

        public unsafe Buffer<T> CreateSubBuffer(ulong origin, ulong length,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
        {
            const BufferCreateType type = BufferCreateType.BufferCreateTypeRegion;

            var flags = (deviceFlags & DeviceFlags).ToDeviceNativeFlags() | (hostFlags & HostFlags).ToHostNativeFlags();
            var region = BufferRegion.Create(origin * (ulong) sizeof(T), length * (ulong) sizeof(T));

            ErrorCode errorCode;
            var id = Library.clCreateSubBufferUnsafe(Id, flags, type, &region, &errorCode);
            errorCode.HandleError();

            return FromId(Library, id);
        }
    }
}
