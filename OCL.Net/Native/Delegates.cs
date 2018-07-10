using System;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net.Native
{
    public delegate void ContextNotify(string errinfo, byte[] privateInfo, UIntPtr cb, IntPtr userData);
    public delegate void EventNotify(EventId @event, CommandExecutionStatus eventCommandExecStatus, IntPtr userData);
    public delegate void MemoryDestructorNotify(MemoryId memObj, IntPtr userData);
    public delegate void ProgramBuildNotify(ProgramId program, IntPtr userData);
    public delegate void NativeKernel(IntPtr args);
}
