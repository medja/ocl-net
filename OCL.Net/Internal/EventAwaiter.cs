using System;
using System.Runtime.CompilerServices;

namespace OCL.Net.Internal
{
    public struct EventAwaiter : ICriticalNotifyCompletion
    {
        public bool IsCompleted => _event.IsCompleted;

        private readonly Event _event;

        internal EventAwaiter(Event @event)
        {
            _event = @event;
        }

        public void GetResult()
        {
            _event.Wait();
        }

        public void OnCompleted(Action continuation)
        {
            _event.OnComplete(continuation);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            _event.OnComplete(continuation);
        }
    }
}
