using System.Collections.Generic;

namespace OCL.Net
{
    public static partial class Extensions
    {
        #region WhenAll

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

        #endregion

        #region Migrate

        public static AutoDisposedEvent MigrateToHost(this CommandQueue queue,
            IEnumerable<MemoryObject> memoryObjects, params Event[] waitList)
        {
            return MemoryObject.MigrateToHost(queue, memoryObjects, waitList);
        }

        public static AutoDisposedEvent MigrateToHost(this CommandQueue queue,
            IEnumerable<MemoryObject> memoryObjects, IEnumerable<Event> waitList)
        {
            return MemoryObject.MigrateToHost(queue, memoryObjects, waitList);
        }

        public static AutoDisposedEvent MigrateToDeviceAsUndefined(this CommandQueue queue,
            IEnumerable<MemoryObject> memoryObjects, params Event[] waitList)
        {
            return MemoryObject.MigrateToDeviceAsUndefined(queue, memoryObjects, waitList);
        }

        public static AutoDisposedEvent MigrateToDeviceAsUndefined(this CommandQueue queue,
            IEnumerable<MemoryObject> memoryObjects, IEnumerable<Event> waitList)
        {
            return MemoryObject.MigrateToDeviceAsUndefined(queue, memoryObjects, waitList);
        }

        #endregion
    }
}
