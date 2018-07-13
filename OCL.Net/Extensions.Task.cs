using System;
using System.Threading.Tasks;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public static partial class Extensions
    {
        public static Event ToEvent(this Task task, Context context,
            ErrorCode faultErrorCode = ErrorCode.ExecStatusErrorForEventsInWaitList)
        {
            var id = CreateEventFromTask(task, context, faultErrorCode);

            return Event.FromId(context.Library, id, userEvent: true);
        }

        public static Event<T> ToEvent<T>(this Task<T> task, Context context,
            ErrorCode faultErrorCode = ErrorCode.ExecStatusErrorForEventsInWaitList)
        {
            var id = CreateEventFromTask(task, context, faultErrorCode);

            return Event.FromId(context.Library, id, () => task.Result, userEvent: true);
        }

        public static AutoDisposedEvent ToAutoDisposedEvent(this Task task, Context context,
            ErrorCode faultErrorCode = ErrorCode.ExecStatusErrorForEventsInWaitList)
        {
            var id = CreateEventFromTask(task, context, faultErrorCode);

            return AutoDisposedEvent.FromId(context.Library, id);
        }

        public static AutoDisposedEvent<T> ToAutoDisposedEvent<T>(this Task<T> task, Context context,
            ErrorCode faultErrorCode = ErrorCode.ExecStatusErrorForEventsInWaitList)
        {
            var id = CreateEventFromTask(task, context, faultErrorCode);

            return AutoDisposedEvent.FromId(context.Library, id, () => task.Result);
        }

        public static Event ToEvent(this Task task, CommandQueue commandQueue,
            bool blocking = true,
            ErrorCode faultErrorCode = ErrorCode.ExecStatusErrorForEventsInWaitList)
        {
            var userEvent = task.ToAutoDisposedEvent(commandQueue.Context, faultErrorCode);
            var id = Utils.EnqueueEvents(commandQueue, blocking, new[] {userEvent.Id});

            return Event.FromId(commandQueue.Library, id);
        }

        public static Event<T> ToEvent<T>(this Task<T> task, CommandQueue commandQueue,
            bool blocking = true,
            ErrorCode faultErrorCode = ErrorCode.ExecStatusErrorForEventsInWaitList)
        {
            var userEvent = task.ToAutoDisposedEvent(commandQueue.Context, faultErrorCode);
            var id = Utils.EnqueueEvents(commandQueue, blocking, new[] {userEvent.Id});

            return Event.FromId(commandQueue.Library, id, () => task.Result);
        }

        public static AutoDisposedEvent ToAutoDisposedEvent(this Task task, CommandQueue commandQueue,
            bool blocking = true,
            ErrorCode faultErrorCode = ErrorCode.ExecStatusErrorForEventsInWaitList)
        {
            var userEvent = task.ToAutoDisposedEvent(commandQueue.Context, faultErrorCode);
            var id = Utils.EnqueueEvents(commandQueue, blocking, new[] {userEvent.Id});

            return AutoDisposedEvent.FromId(commandQueue.Library, id);
        }

        public static AutoDisposedEvent<T> ToAutoDisposedEvent<T>(this Task<T> task, CommandQueue commandQueue,
            bool blocking = true,
            ErrorCode faultErrorCode = ErrorCode.ExecStatusErrorForEventsInWaitList)
        {
            var userEvent = task.ToAutoDisposedEvent(commandQueue.Context, faultErrorCode);
            var id = Utils.EnqueueEvents(commandQueue, blocking, new[] {userEvent.Id});

            return AutoDisposedEvent.FromId(commandQueue.Library, id, () => task.Result);
        }

        private static EventId CreateEventFromTask(Task task, Context context, ErrorCode faultErrorCode)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var lib = context.Library;

            var id = lib.clCreateUserEvent(context, out var errorCode);
            errorCode.HandleError();

            task.ContinueWith(t =>
            {
                if (t.IsFaulted && faultErrorCode < ErrorCode.Success)
                    lib.clSetUserEventStatus(id, (CommandExecutionStatus) faultErrorCode).HandleError();
                else
                    lib.clSetUserEventStatus(id, CommandExecutionStatus.Complete).HandleError();
            });

            return id;
        }
    }
}
