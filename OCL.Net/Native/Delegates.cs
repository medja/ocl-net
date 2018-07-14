using System;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net.Native
{
    public delegate void ContextNotify(string errinfo, byte[] privateInfo, UIntPtr cb, IntPtr userData);
    public unsafe delegate void ContextNotifyUnsafe(string errinfo, byte* privateInfo, UIntPtr cb, void* userData);

    public delegate void EventNotify(EventId @event, CommandExecutionStatus eventCommandExecStatus, IntPtr userData);
    public unsafe delegate void EventNotifyUnsafe(EventId @event, CommandExecutionStatus eventCommandExecStatus, void* userData);

    public delegate void MemoryDestructorNotify(MemoryId memObj, IntPtr userData);
    public unsafe delegate void MemoryDestructorNotifyUnsafe(MemoryId memObj, void* userData);

    public delegate void ProgramBuildNotify(ProgramId program, IntPtr userData);
    public unsafe delegate void ProgramBuildNotifyUnsafe(ProgramId program, void* userData);

    public delegate void NativeKernel(IntPtr args);
    public unsafe delegate void NativeKernelUnsafe(void* args);
}
