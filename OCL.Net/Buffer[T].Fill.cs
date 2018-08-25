using System;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Fill

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Fill(CommandQueue queue, Span<T> pattern, params Event[] waitList)
        {
            return Fill(queue, pattern, 0, LongLength, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Fill(CommandQueue queue, Span<T> pattern, int offset, int length,
            params Event[] waitList)
        {
            return Fill(queue, pattern, (ulong) offset, (ulong) length, waitList);
        }

        public AutoDisposedEvent Fill(CommandQueue queue, Span<T> pattern, ulong offset, ulong length,
            params Event[] waitList)
        {
            var eventId = FillInternal(queue, pattern, offset, length, waitList);
            return AutoDisposedEvent.FromId(Library, eventId);
        }

        #endregion

        #region FillInternal

        internal unsafe EventId FillInternal(CommandQueue queue, Span<T> pattern, ulong offset, ulong length,
            params Event[] waitList)
        {
            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (T* ptr = pattern)
            fixed (EventId* idsPtr = eventIds)
            {
                Library.clEnqueueFillBufferUnsafe(queue, Id,
                    ptr, (UIntPtr) pattern.Length, (UIntPtr) offset, (UIntPtr) length,
                    eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }

        #endregion
    }
}
