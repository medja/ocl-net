using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static Sampler CreateSampler(IOpenCl library, SamplerId samplerId)
        {
            return Sampler.FromId(library, samplerId);
        }
    }
}
