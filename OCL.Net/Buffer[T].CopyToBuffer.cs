using System;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region CopyToBuffer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, 0, 0, LongLength, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer, int offset, int length,
            params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, (ulong) offset, (ulong) offset, (ulong) length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer, ulong offset, ulong length,
            params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, offset, offset, length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            int sourceOffset, int destinationOffset, int length, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer,
                (ulong) sourceOffset, (ulong) destinationOffset, (ulong) length, waitList);
        }

        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong sourceOffset, ulong destinationOffset, ulong length, params Event[] waitList)
        {
            var eventId = CopyToBufferInternal(queue, buffer, sourceOffset, destinationOffset, length, waitList);
            return AutoDisposedEvent.FromId(Library, eventId, buffer);
        }

        #endregion

        #region CopyToBufferInternal

        internal unsafe EventId CopyToBufferInternal(CommandQueue queue, Buffer<T> buffer,
            ulong sourceOffset, ulong destinationOffset, ulong length, params Event[] waitList)
        {
            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* idsPtr = eventIds)
            {
                Library.clEnqueueCopyBufferUnsafe(queue, Id, buffer,
                    (UIntPtr) (sourceOffset * (ulong) sizeof(T)),
                    (UIntPtr) (destinationOffset * (ulong) sizeof(T)),
                    (UIntPtr) (length * (ulong) sizeof(T)),
                    eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }

        #endregion
    }
}
