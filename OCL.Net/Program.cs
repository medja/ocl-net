using System;
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
