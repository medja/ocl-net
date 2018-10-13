using System;
using System.Collections.Generic;
using System.Threading;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class ProgramBuildEvent
    {
        private static readonly Dictionary<IntPtr, WeakReference<ProgramBuildEvent>> References =
            new Dictionary<IntPtr, WeakReference<ProgramBuildEvent>>();

        private static void RegisterHandle(IntPtr handle, ProgramBuildEvent @event)
        {
            lock (References)
            {
                References[handle] = new WeakReference<ProgramBuildEvent>(@event);
            }
        }

        private static void DisposeHandle(IntPtr handle)
        {
            lock (References)
            {
                References.Remove(handle);
            }
        }

        internal static unsafe void ProgramBuildCallback(ProgramId programId, void* userData)
        {
            void HandleCallback(object state)
            {
                ProgramBuildEvent @event;
                var handle = (IntPtr) userData;

                lock (References)
                {
                    if (!References.TryGetValue(handle, out var reference))
                        return;

                    if (!reference.TryGetTarget(out @event))
                    {
                        References.Remove(handle);
                        return;
                    }
                }

                @event.Update();
            }

            ThreadPool.UnsafeQueueUserWorkItem(HandleCallback, null);
        }
    }
}
