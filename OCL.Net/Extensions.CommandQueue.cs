using System.Collections.Generic;

namespace OCL.Net
{
    public static partial class Extensions
    {
        public static Event WhenAll(this CommandQueue queue, bool blocking)
        {
            return Event.WhenAll(queue, blocking);
        }

        public static Event WhenAll(this CommandQueue queue, bool blocking, params Event[] events)
        {
            return Event.WhenAll(queue, blocking, events);
        }

        public static Event WhenAll(this CommandQueue queue, bool blocking, IEnumerable<Event> events)
        {
            return Event.WhenAll(queue, blocking, events);
        }
    }
}
