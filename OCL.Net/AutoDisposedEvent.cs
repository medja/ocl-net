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
    public class AutoDisposedEvent
    {
        public EventId Id => _event.Id;

        private readonly Event _event;

        private protected AutoDisposedEvent(Event @event)
        {
            _event = @event;
        }

        public void Flush()
        {
            _event.Flush();
        }

        public void Wait()
        {
            _event.Wait();
        }

        public EventAwaiter GetAwaiter()
        {
            return new EventAwaiter(_event);
        }

        public void ContinueWith(Action<Event> continuation)
        {
            _event.DisableAutoDispose();
            _event.OnComplete(() => continuation(_event));
        }

        public Event ToEvent()
        {
            _event.DisableAutoDispose();
            return _event;
        }

        public Task ToTask()
        {
            return ToEvent().ToTask();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OnComplete(Action continuation)
        {
            _event.OnComplete(continuation);
        }

        public static implicit operator Event(AutoDisposedEvent @event)
        {
            return @event.ToEvent();
        }

        public static implicit operator EventId(AutoDisposedEvent @event)
        {
            return @event._event.Id;
        }

        internal static AutoDisposedEvent FromId(IOpenCl lib, EventId id)
        {
            return new AutoDisposedEvent(Event.FromId(lib, id, autoDispose: true));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static AutoDisposedEvent<T> FromId<T>(IOpenCl lib, EventId id, T result)
        {
            return AutoDisposedEvent<T>.FromId(lib, id, result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static AutoDisposedEvent<T> FromId<T>(IOpenCl lib, EventId id, Func<T> resultFactory)
        {
            return AutoDisposedEvent<T>.FromId(lib, id, resultFactory);
        }
    }
}
