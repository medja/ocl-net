using System;
using System.Threading.Tasks;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public partial class Event
    {
        public EventAwaiter GetAwaiter()
        {
            return new EventAwaiter(this);
        }

        public void ContinueWith(Action<Event> continuation)
        {
            OnComplete(() => continuation(this));
        }

        public Task ToTask()
        {
            return _taskCompletionSource.Value.Task;
        }

        private TaskCompletionSource<object> CreateTaskCompletionSource()
        {
            var completionSource = new TaskCompletionSource<object>();

            OnComplete(() =>
            {
                if (Status == ErrorCode.Success)
                    completionSource.SetResult(null);
                else
                    completionSource.SetException(new OpenClException(Status));
            });

            return completionSource;
        }
    }
}
