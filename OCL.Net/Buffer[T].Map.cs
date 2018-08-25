using System;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Map

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory> Map(CommandQueue queue,
            MapFlags flags = MapFlags.MapRead | MapFlags.MapWrite, params Event[] waitList)
        {
            return Map(queue, 0, LongLength, flags, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory> Map(CommandQueue queue, int offset, int length,
            MapFlags flags = MapFlags.MapRead | MapFlags.MapWrite, params Event[] waitList)
        {
            return Map(queue, (ulong) offset, (ulong) length, flags, waitList);
        }

        public unsafe AutoDisposedEvent<Memory> Map(CommandQueue queue, ulong offset, ulong length,
            MapFlags flags = MapFlags.MapRead | MapFlags.MapWrite, params Event[] waitList)
        {
            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            void* ptr;
            EventId eventId;
            ErrorCode errorCode;

            fixed (EventId* idsPtr = eventIds)
            {
                ptr = Library.clEnqueueMapBufferUnsafe(queue, Id, false, flags,
                    (UIntPtr) (offset * (ulong) sizeof(T)),
                    (UIntPtr) (length * (ulong) sizeof(T)),
                    eventCount, idsPtr, &eventId, &errorCode);
            }

            errorCode.HandleError();

            var memory = new Memory((T*) ptr, (long) length, Id, queue, Library);
            return AutoDisposedEvent.FromId(Library, eventId, memory);
        }

        #endregion

        #region Unmap

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Unmap(CommandQueue queue, Memory memory, params Event[] waitList)
        {
            return memory.Unmap(queue, waitList);
        }

        #endregion
    }
}
