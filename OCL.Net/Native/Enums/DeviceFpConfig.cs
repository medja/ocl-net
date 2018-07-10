using System;

namespace OCL.Net.Native.Enums
{
    [Flags]
    public enum DeviceFpConfig : ulong
    {
        FpDenorm                     = 1 << 0,
        FpInfNan                     = 1 << 1,
        FpRoundToNearest             = 1 << 2,
        FpRountToZero                = 1 << 3,
        FpRountToInf                 = 1 << 4,
        FpFma                        = 1 << 5,
        FpSoftFloat                  = 1 << 6,
        FpCorrectlyRoundedDivideSqrt = 1 << 7
    }
}
