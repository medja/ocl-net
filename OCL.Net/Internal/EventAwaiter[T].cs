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
            _event.Flush();
            _event.OnComplete(continuation);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            _event.Flush();
            _event.UnsafeOnComplete(continuation);
        }
    }
}
