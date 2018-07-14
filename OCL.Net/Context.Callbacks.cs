using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace OCL.Net
{
    public sealed partial class Context
    {
        private static readonly Dictionary<IntPtr, WeakReference<Context>> References =
            new Dictionary<IntPtr, WeakReference<Context>>();

        private static void RegisterHandle(IntPtr handle, Context context)
        {
            lock (References)
            {
                References[handle] = new WeakReference<Context>(context);
            }
        }

        private static void DisposeHandle(IntPtr handle)
        {
            lock (References)
            {
                References.Remove(handle);
            }
        }

        [SuppressMessage("ReSharper", "IdentifierTypo")]
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static unsafe void ContextCallback(string errinfo, byte* privateInfo, UIntPtr cb, void* userData)
        {
            void HandleCallback(object state)
            {
                Context context;
                var handle = (IntPtr) userData;

                lock (References)
                {
                    if (!References.TryGetValue(handle, out var reference))
                        return;

                    if (!reference.TryGetTarget(out context))
                    {
                        References.Remove(handle);
                        return;
                    }
                }

                context.OnError(new RuntimeExceptionArgs(new RuntimeException(errinfo)));
            }

            ThreadPool.UnsafeQueueUserWorkItem(HandleCallback, null);
        }
    }
}
