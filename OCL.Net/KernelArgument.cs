using System;
using System.Runtime.CompilerServices;
using System.Threading;
using OCL.Net.Native;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public sealed partial class KernelArgument
    {
        public uint Index { get; }
        public Kernel Kernel { get; }

        public IOpenCl Library
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Kernel.Library;
        }

        public KernelArgument(int index, Kernel kernel) : this((uint) index, kernel)
        { }

        public KernelArgument(uint index, Kernel kernel)
        {
            Index = index;
            Kernel = kernel;

            _type = new Lazy<Type>(FindType, LazyThreadSafetyMode.None);
        }

        private unsafe ErrorCode GetInfo(KernelArgInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
        {
            return Library.clGetKernelArgInfoUnsafe(Kernel, Index, info, bufferSize, buffer, size);
        }
    }
}
