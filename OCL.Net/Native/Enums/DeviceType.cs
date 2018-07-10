using System;

namespace OCL.Net.Native.Enums
{
    [Flags]
    public enum DeviceType : ulong
    {
        Default     = 1 << 0,
        Cpu         = 1 << 1,
        Gpu         = 1 << 2,
        Accelerator = 1 << 3,
        Custom      = 1 << 4,
        All         = 0xFFFFFFFF
    }
}
