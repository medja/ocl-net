using OCL.Net.Internal;

namespace OCL.Net
{
    public sealed partial class Event<T>
    {
        public new AutoDisposedEvent Enqueue(CommandQueue commandQueue, bool blocking = true)
        {
            var eventId = Utils.EnqueueEvents(commandQueue, blocking, new[] {Id});

            return AutoDisposedEvent<T>.FromId(Library, eventId, () => _result.Value);
        }
    }
}
