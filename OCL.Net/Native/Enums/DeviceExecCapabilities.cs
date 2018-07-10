using System;

namespace OCL.Net.Native.Enums
{
    [Flags]
    public enum DeviceExecCapabilities : ulong
    {
        ExecKernel       = 1 << 0,
        ExecNativeKernel = 1 << 1
    }
}
