using System;

namespace OCL.Net.Native.Enums
{
    [Flags]
    public enum MapFlags : ulong
    {
        MapRead                  = 1 << 0,
        MapWrite                 = 1 << 1,
        MapWriteInvalidateRegion = 1 << 2
    }
}
