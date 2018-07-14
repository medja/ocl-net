using System;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Event<T>
    {
        public new AutoDisposedEvent Enqueue(CommandQueue commandQueue, bool blocking = true)
        {
            Span<EventId> ids = stackalloc EventId[1];
            ids[0] = Id;

            var eventId = Utils.EnqueueEvents(commandQueue, blocking, ids);

            return AutoDisposedEvent<T>.FromId(Library, eventId, () => _result.Value);
        }
    }
}
