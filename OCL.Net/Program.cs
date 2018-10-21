using System;
using System.Collections.Generic;
using System.Linq;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Program : RefCountedType<ProgramId, ProgramInfo>
    {
        private Program(ProgramId id, IOpenCl lib) : base(id, lib)
        { }

        public Kernel CreateKernel(string kernelName)
        {
            var kernelId = Library.clCreateKernel(Id, kernelName, out var errorCode);
            errorCode.HandleError();

            return Kernel.FromId(Library, kernelId);
        }

        public unsafe Dictionary<string, Kernel> CreateKernels()
        {
            var kernelIds = new KernelId[NumKernels];

            fixed (KernelId* kernelIdsPtr = kernelIds)
                Library.clCreateKernelsInProgramUnsafe(Id, (uint) kernelIds.Length, kernelIdsPtr, null).HandleError();

            return kernelIds
                .Select(kernelId => Kernel.FromId(Library, kernelId))
                .ToDictionary(kernel => kernel.FunctionName);
        }

        protected override unsafe ErrorCode GetInfo(ProgramInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
        {
            return Library.clGetProgramInfoUnsafe(Id, info, bufferSize, buffer, size);
        }

        protected override ErrorCode RetainObject()
        {
            return Library.clRetainProgram(Id);
        }

        protected override ErrorCode ReleaseObject()
        {
            return Library.clReleaseProgram(Id);
        }

        public static implicit operator Program(ProgramId id)
        {
            return FromId(id) as Program;
        }

        public static implicit operator ProgramId(Program program)
        {
            return program.Id;
        }

        internal static Program FromId(IOpenCl lib, ProgramId id)
        {
            return FromId(id) as Program ?? new Program(id, lib);
        }
    }
}
