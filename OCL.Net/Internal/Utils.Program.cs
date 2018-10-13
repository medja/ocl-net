using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static Program CreateProgram(IOpenCl library, ProgramId programId)
        {
            return Program.FromId(library, programId);
        }
    }
}
