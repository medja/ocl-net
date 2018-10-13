using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OCL.Net.Native.Enums;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        private static int _nextProgramBuildEventHandle;

        public static IntPtr CreateProgramBuildEventHandle()
        {
            var nextHandle = Interlocked.Increment(ref _nextProgramBuildEventHandle);

            while (nextHandle == 0)
                nextHandle = Interlocked.Increment(ref _nextProgramBuildEventHandle);

            return (IntPtr) nextHandle;
        }

        public static ProgramBuildEvent CreateProgramBuildEvent(ProgramBuildType buildType,
            Program program, IEnumerable<Device> devices, IntPtr eventHandle)
        {
            return ProgramBuildEvent.Create(buildType, program, devices, eventHandle);
        }

        public static string GetProgramBuildLog(Program program, Device device)
        {
            return LoadProgramBuildString(program, device, ProgramBuildInfo.ProgramBuildLog);
        }

        public static string GetProgramBuildOptions(Program program, Device device)
        {
            return LoadProgramBuildString(program, device, ProgramBuildInfo.ProgramBuildOptions);
        }

        private static unsafe string LoadProgramBuildString(Program program, Device device, ProgramBuildInfo info)
        {
            UIntPtr size;

            program.Library.clGetProgramBuildInfoUnsafe(program, device, info, UIntPtr.Zero, null, &size).HandleError();

            var buffer = new byte[(int) size];

            fixed (byte* ptr = buffer)
                program.Library.clGetProgramBuildInfoUnsafe(program, device, info, size, ptr, null).HandleError();

            return Encoding.UTF8.GetString(buffer).TrimEnd('\0');
        }
    }
}
