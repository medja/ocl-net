using System;

namespace OCL.Net
{
    [Flags]
    public enum MemoryFlags
    {
        Read  = 1 << 0,
        Write = 1 << 1
    }
}
