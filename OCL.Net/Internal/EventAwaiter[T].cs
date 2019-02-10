using System;
using System.Runtime.CompilerServices;

namespace OCL.Net.Internal
{
    public class EventAwaiter<T> : ICriticalNotifyCompletion
    {
        public bool IsCompleted => _event.IsCompleted;

        private readonly Event<T> _event;

        internal EventAwaiter(Event<T> @event)
        {
            _event = @event;
        }

        public T GetResult()
        {
            return _event.Result;
        }

        public void OnCompleted(Action continuation)
        {
            _event.OnComplete(continuation);
            _event.Flush();
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            _event.UnsafeOnComplete(continuation);
            _event.Flush();
        }
    }
}
