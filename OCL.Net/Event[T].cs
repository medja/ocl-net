using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Event<T> : Event
    {
        public T Result => GetResult();

        private readonly Lazy<T> _result;
        private readonly Lazy<TaskCompletionSource<T>> _taskCompletionSource;

        private Event(EventId id, IOpenCl lib, Func<T> resultFactory, bool autoDispose = false, bool userEvent = false)
            : base(id, lib, autoDispose, userEvent)
        {
            _result = new Lazy<T>(resultFactory);
            _taskCompletionSource = new Lazy<TaskCompletionSource<T>>(CreateTaskCompletionSource);
        }

        public static implicit operator Event<T>(EventId id)
        {
            return FromId(id) as Event<T>;
        }

        public static implicit operator EventId(Event<T> @event)
        {
            return @event.Id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Event<T> FromId(IOpenCl lib, EventId id, T result,
            bool autoDispose = false, bool userEvent = false)
        {
            return FromId(lib, id, () => result, autoDispose, userEvent);
        }

        internal static Event<T> FromId(IOpenCl lib, EventId id, Func<T> resultFactory,
            bool autoDispose = false, bool userEvent = false)
        {
            return FromId(id) as Event<T> ?? new Event<T>(id, lib, resultFactory, autoDispose, userEvent);
        }
    }
}
