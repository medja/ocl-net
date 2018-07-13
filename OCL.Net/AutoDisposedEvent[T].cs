using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public sealed class AutoDisposedEvent<T> : AutoDisposedEvent
    {
        private readonly Event<T> _event;

        private AutoDisposedEvent(Event<T> @event) : base(@event)
        {
            _event = @event;
        }

        public new EventAwaiter<T> GetAwaiter()
        {
            return new EventAwaiter<T>(_event);
        }

        public void ContinueWith(Action<Event<T>> continuation)
        {
            _event.DisableAutoDispose();
            _event.OnComplete(() => continuation(_event));
        }

        public new Event<T> ToEvent()
        {
            _event.DisableAutoDispose();
            return _event;
        }

        public new Task<T> ToTask()
        {
            return ToEvent().ToTask();
        }

        public static implicit operator Event(AutoDisposedEvent<T> @event)
        {
            return @event.ToEvent();
        }

        public static implicit operator Event<T>(AutoDisposedEvent<T> @event)
        {
            return @event.ToEvent();
        }

        public static implicit operator EventId(AutoDisposedEvent<T> @event)
        {
            return @event._event.Id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static AutoDisposedEvent<T> FromId(IOpenCl lib, EventId id, T result)
        {
            return FromId(lib, id, () => result);
        }

        internal static AutoDisposedEvent<T> FromId(IOpenCl lib, EventId id, Func<T> resultFactory)
        {
            return new AutoDisposedEvent<T>(Event.FromId(lib, id, resultFactory, true));
        }
    }
}
