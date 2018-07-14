using System;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Platform : OpenClType<PlatformId, PlatformInfo>
    {
        private Platform(PlatformId id, IOpenCl lib) : base(id, lib)
        { }

        public void UnloadCompiler()
        {
            Library.clUnloadPlatformCompiler(Id).HandleError();
        }

        protected override unsafe ErrorCode GetInfo(PlatformInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
        {
            return Library.clGetPlatformInfoUnsafe(Id, info, bufferSize, buffer, size);
        }

        public static implicit operator Platform(PlatformId id)
        {
            return FromId(id) as Platform;
        }

        public static implicit operator PlatformId(Platform platform)
        {
            return platform.Id;
        }

        internal static Platform FromId(IOpenCl lib, PlatformId id)
        {
            return FromId(id) as Platform ?? new Platform(id, lib);
        }
    }
}
