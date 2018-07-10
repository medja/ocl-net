namespace OCL.Net.Native.Enums
{
    public enum KernelArgTypeQualifier : ulong
    {
        KernelArgTypeNone     = 0,
        KernelArgTypeConst    = 1 << 0,
        KernelArgTypeRestrict = 1 << 1,
        KernelArgTypeVolatile = 1 << 2
    }
}
