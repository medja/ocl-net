using System;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Sampler : RefCountedType<SamplerId, SamplerInfo>
    {
        private Sampler(SamplerId id, IOpenCl lib) : base(id, lib)
        { }

        protected override unsafe ErrorCode GetInfo(SamplerInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
        {
            return Library.clGetSamplerInfoUnsafe(Id, info, bufferSize, buffer, size);
        }

        protected override ErrorCode RetainObject()
        {
            return Library.clRetainSampler(Id);
        }

        protected override ErrorCode ReleaseObject()
        {
            return Library.clReleaseSampler(Id);
        }

        public static implicit operator Sampler(SamplerId id)
        {
            return FromId(id) as Sampler;
        }

        public static implicit operator SamplerId(Sampler sampler)
        {
            return sampler.Id;
        }

        internal static Sampler FromId(IOpenCl lib, SamplerId id)
        {
            return FromId(id) as Sampler ?? new Sampler(id, lib);
        }
    }
}
