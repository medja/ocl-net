namespace OCL.Net.Native.Enums
{
    public enum KernelWorkGroupInfo : uint
    {
        KernelWorkGroupSize                  = 0x11B0,
        KernelCompileWorkGroupSize           = 0x11B1,
        KernelLocalMemSize                   = 0x11B2,
        KernelPreferredWorkGroupSizeMultiple = 0x11B3,
        KernelPrivateMemSize                 = 0x11B4,
        KernelGlobalWorkSize                 = 0x11B5
    }
}
