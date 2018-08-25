using System;
using System.Threading;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        private static int _nextContextHandle;

        public static Context CreateContext(IOpenCl library, ContextId contextId, IntPtr contextHandle)
        {
            return Context.FromId(library, contextId, contextHandle);
        }

        public static IntPtr CreateContextHandle()
        {
            var nextHandle = Interlocked.Increment(ref _nextContextHandle);

            while (nextHandle == 0)
                nextHandle = Interlocked.Increment(ref _nextContextHandle);

            return (IntPtr) nextHandle;
        }
    }
}
