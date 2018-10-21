using System;
using System.Runtime.CompilerServices;
using OCL.Net.Native;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public sealed partial class KernelWorkGroup
    {
        public Device Device { get; }
        public Kernel Kernel { get; }

        public IOpenCl Library
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Kernel.Library;
        }

        public KernelWorkGroup(Device device, Kernel kernel)
        {
            Device = device;
            Kernel = kernel;
        }

        private unsafe ErrorCode GetInfo(KernelWorkGroupInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
        {
            return Library.clGetKernelWorkGroupInfoUnsafe(Kernel, Device, info, bufferSize, buffer, size);
        }
    }
}
