using System;
using System.Threading;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static class Utils
    {
        private static int _nextContextHandle;

        public static Device CreateDevice(IOpenCl library, DeviceId deviceId)
        {
            return Device.FromId(library, deviceId);
        }

        public static Platform CreatePlatform(IOpenCl library, PlatformId platformId)
        {
            return Platform.FromId(library, platformId);
        }

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

        public static CommandQueue CreateCommandQueue(IOpenCl library, CommandQueueId commandQueueId)
        {
            return CommandQueue.FromId(library, commandQueueId, false);
        }
    }
}
