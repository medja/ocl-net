using System;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Write

        public AutoDisposedEvent Write(CommandQueue queue, Memory<T> memory, params Event[] waitList)
        {
            return Write(queue, memory, 0, LongLength, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Write(CommandQueue queue, Memory<T> memory, int offset, int length,
            params Event[] waitList)
        {
            return Write(queue, memory, (ulong) offset, (ulong) length, waitList);
        }

        public unsafe AutoDisposedEvent Write(CommandQueue queue, Memory<T> memory, ulong offset, ulong length,
            params Event[] waitList)
        {
            if ((ulong) memory.Length < length)
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot read over memory capacity");

            var handle = memory.Pin();

            var eventId = WriteInto(queue, (T*) handle.Pointer, offset, length, waitList);
            var @event = AutoDisposedEvent.FromId(Library, eventId);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region WriteInto

        internal unsafe EventId WriteInto(CommandQueue queue, T* ptr, ulong offset, ulong length,
            params Event[] waitList)
        {
            var byteOffset = (UIntPtr) (offset * (ulong) sizeof(T));
            var size = (UIntPtr) (length * (ulong) sizeof(T));

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* idsPtr = eventIds)
            {
                Library.clEnqueueWriteBufferUnsafe(queue, Id, false, byteOffset, size, ptr,
                    eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }

        #endregion
    }
}
