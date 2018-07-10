using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using OCL.Net.Native.Enums;

namespace OCL.Net.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    public struct ImageFormat
    {
        public ImageChannelOrder ImageChannelOrder => _imageChannelOrder;
        public ImageChannelType ImageChannelDataType => _imageChannelDataType;

        private readonly ImageChannelOrder _imageChannelOrder;
        private readonly ImageChannelType _imageChannelDataType;

        public ImageFormat(ImageChannelOrder imageChannelOrder, ImageChannelType imageChannelDataType)
        {
            _imageChannelOrder = imageChannelOrder;
            _imageChannelDataType = imageChannelDataType;
        }
    }
}
