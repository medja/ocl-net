using System;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static Event CreateEvent(IOpenCl library, EventId eventId)
        {
            return Event.FromId(library, eventId);
        }

        public static Event<T> CreateEvent<T>(IOpenCl library, EventId eventId, T result)
        {
            return Event.FromId(library, eventId, result);
        }

        public static AutoDisposedEvent CreateAutoDisposedEvent(IOpenCl library, EventId eventId)
        {
            return AutoDisposedEvent.FromId(library, eventId);
        }

        public static AutoDisposedEvent<T> CreateAutoDisposedEvent<T>(IOpenCl library, EventId eventId, T result)
        {
            return AutoDisposedEvent.FromId(library, eventId, result);
        }

        public static AutoDisposedEvent<T> CreateAutoDisposedEvent<T>(IOpenCl library, EventId eventId,
            Func<T> resultFactory)
        {
            return AutoDisposedEvent.FromId(library, eventId, resultFactory);
        }

        public static UserEvent CreateUserEvent(IOpenCl library, EventId eventId)
        {
            return UserEvent.FromId(library, eventId);
        }

        internal static unsafe EventId EnqueueEvents(CommandQueue commandQueue, bool blocking, Span<EventId> eventIds)
        {
            EventId eventId;

            var library = commandQueue.Library;
            var length = (uint) eventIds.Length;

            fixed (EventId* idsPtr = eventIds)
            {
                if (blocking)
                    library.clEnqueueBarrierWithWaitListUnsafe(commandQueue, length, idsPtr, &eventId).HandleError();
                else
                    library.clEnqueueMarkerWithWaitListUnsafe(commandQueue, length, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }
    }
}
