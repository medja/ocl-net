using System;

namespace OCL.Net.Native.Enums
{
    [Flags]
    public enum MemFlags : ulong
    {
        MemReadWrite     = 1 << 0,
        MemWriteOnly     = 1 << 1,
        MemReadOnly      = 1 << 2,
        MemUseHostPtr    = 1 << 3,
        MemAllocHostPtr  = 1 << 4,
        MemCopyHostPtr   = 1 << 5,
        MemHostWriteOnly = 1 << 7,
        MemHostReadOnly  = 1 << 8,
        MemHostNoAccess  = 1 << 9
    }
}
