using System;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image
    {
        public ulong ElementSize => LoadValue(ImageInfo.ImageElementSize, ref _elementSize).ToUInt64();
        public ImageFormat Format => LoadValue(ImageInfo.ImageFormat, ref _format);
        public uint NumMipLevels => LoadValue(ImageInfo.ImageNumMipLevels, ref _numMipLevels);
        public uint NumSamples => LoadValue(ImageInfo.ImageNumSamples, ref _numSamples);
        public ulong RowPitch => LoadValue(ImageInfo.ImageRowPitch, ref _rowPitch).ToUInt64();
        public ulong Width => LoadValue(ImageInfo.ImageWidth, ref _width).ToUInt64();

        private UIntPtr? _elementSize;
        private ImageFormat? _format;
        private uint? _numMipLevels;
        private uint? _numSamples;
        private UIntPtr? _rowPitch;
        private UIntPtr? _width;
    }
}
