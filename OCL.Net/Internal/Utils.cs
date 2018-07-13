using System;
using System.Threading;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static class Utils
    {
        private static int _nextContextHandle;

        public static Device CreateDevice(IOpenCl library, DeviceId deviceId)
        {
            return Device.FromId(library, deviceId);
        }

        public static Platform CreatePlatform(IOpenCl library, PlatformId platformId)
        {
            return Platform.FromId(library, platformId);
        }

        public static Context CreateContext(IOpenCl library, ContextId contextId, IntPtr contextHandle)
        {
            return Context.FromId(library, contextId, contextHandle);
        }

        public static IntPtr CreateContextHandle()
        {
            var nextHandle = Interlocked.Increment(ref _nextContextHandle);

            while (nextHandle == 0)
                nextHandle = Interlocked.Increment(ref _nextContextHandle);

            return (IntPtr) nextHandle;
        }

        public static CommandQueue CreateCommandQueue(IOpenCl library, CommandQueueId commandQueueId)
        {
            return CommandQueue.FromId(library, commandQueueId, false);
        }

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

        internal static EventId EnqueueEvents(CommandQueue commandQueue, bool blocking, EventId[] eventIds)
        {
            EventId eventId;

            var library = commandQueue.Library;
            var length = (uint) (eventIds?.Length ?? 0);

            if (blocking)
                library.clEnqueueBarrierWithWaitList(commandQueue, length, eventIds, out eventId).HandleError();
            else
                library.clEnqueueMarkerWithWaitList(commandQueue, length, eventIds, out eventId).HandleError();

            return eventId;
        }
    }
}
