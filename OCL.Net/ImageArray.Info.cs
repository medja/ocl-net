using System;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public abstract partial class ImageArray
    {
        public ulong Length => LoadValue(ImageInfo.ImageArraySize, ref _length).ToUInt64();

        private UIntPtr? _length;
    }
}
