using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static CommandQueue CreateCommandQueue(IOpenCl library, CommandQueueId commandQueueId)
        {
            return CommandQueue.FromId(library, commandQueueId, false);
        }
    }
}
