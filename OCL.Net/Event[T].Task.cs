using System;
using System.Threading.Tasks;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public sealed partial class Event<T>
    {
        public new EventAwaiter<T> GetAwaiter()
        {
            return new EventAwaiter<T>(this);
        }

        private T GetResult()
        {
            Wait();
            return _result.Value;
        }

        public void ContinueWith(Action<Event<T>> continuation)
        {
            OnComplete(() => continuation(this));
        }

        public new Task<T> ToTask()
        {
            return _taskCompletionSource.Value.Task;
        }

        private TaskCompletionSource<T> CreateTaskCompletionSource()
        {
            var completionSource = new TaskCompletionSource<T>();

            OnComplete(() =>
            {
                if (Status == ErrorCode.Success)
                    completionSource.SetResult(_result.Value);
                else
                    completionSource.SetException(new OpenClException(Status));
            });

            return completionSource;
        }
    }
}
