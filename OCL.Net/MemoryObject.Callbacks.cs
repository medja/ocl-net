using System;
using System.Threading;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class MemoryObject
    {
        private static void DestructorCallback(MemoryId memoryId, IntPtr userData)
        {
            void HandleCallback(object state)
            {
                (FromId(memoryId) as MemoryObject)?.OnDestroy();
            }

            ThreadPool.UnsafeQueueUserWorkItem(HandleCallback, null);
        }
    }
}
