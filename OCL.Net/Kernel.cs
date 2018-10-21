using System;
using System.Collections.Generic;
using System.Linq;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Kernel : RefCountedType<KernelId, KernelInfo>
    {
        public IReadOnlyList<KernelArgument> Arguments { get; }
        public IReadOnlyList<KernelWorkGroup> WorkGroups { get; }

        private Kernel(KernelId id, IOpenCl lib) : base(id, lib)
        {
            var arguments = new KernelArgument[NumArgs];

            for (var i = 0; i < arguments.Length; i++)
                arguments[i] = new KernelArgument(i, this);

            Arguments = arguments;
            WorkGroups = Program.Devices.Select(device => new KernelWorkGroup(device, this)).ToArray();
        }

        protected override unsafe ErrorCode GetInfo(KernelInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
        {
            return Library.clGetKernelInfoUnsafe(Id, info, bufferSize, buffer, size);
        }

        protected override ErrorCode RetainObject()
        {
            return Library.clRetainKernel(Id);
        }

        protected override ErrorCode ReleaseObject()
        {
            return Library.clReleaseKernel(Id);
        }

        public static implicit operator Kernel(KernelId id)
        {
            return FromId(id) as Kernel;
        }

        public static implicit operator KernelId(Kernel kernel)
        {
            return kernel.Id;
        }

        internal static Kernel FromId(IOpenCl lib, KernelId id)
        {
            return FromId(id) as Kernel ?? new Kernel(id, lib);
        }
    }
}
