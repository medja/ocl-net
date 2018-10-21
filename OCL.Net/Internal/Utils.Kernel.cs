using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static Kernel CreateKernel(IOpenCl library, KernelId kernelId)
        {
            return Kernel.FromId(library, kernelId);
        }
    }
}
