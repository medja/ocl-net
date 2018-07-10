using System;

namespace OCL.Net.Native.Enums
{
    [Flags]
    public enum CommandQueueProperties : ulong
    {
        QueueOutOfOrderExecModeEnable = 1 << 0,
        QueueProfilingEnable          = 1 << 1
    }
}
