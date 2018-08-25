using System;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public abstract partial class Image3D
    {
        public ulong Depth => LoadValue(ImageInfo.ImageDepth, ref _depth).ToUInt64();
        public ulong Height => LoadValue(ImageInfo.ImageHeight, ref _height).ToUInt64();
        public ulong SlicePitch => LoadValue(ImageInfo.ImageSlicePitch, ref _slicePitch).ToUInt64();

        private UIntPtr? _depth;
        private UIntPtr? _slicePitch;
        private UIntPtr? _height;
    }
}
