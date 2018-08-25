using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;

namespace OCL.Net.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    public struct ImageFormat
    {
        public ImageChannelOrder ImageChannelOrder => _imageChannelOrder;
        public ImageChannelType ImageChannelDataType => _imageChannelDataType;

        public bool Normalized => ChannelTypeInfo.For(_imageChannelDataType).Normalized;
        public int ElementSize => GetElementSize();
        public int ChannelCount => ChannelOrderInfo.For(_imageChannelOrder).ChannelCount;

        private readonly ImageChannelOrder _imageChannelOrder;
        private readonly ImageChannelType _imageChannelDataType;

        public ImageFormat(ImageChannelOrder imageChannelOrder, ImageChannelType imageChannelDataType)
        {
            _imageChannelOrder = imageChannelOrder;
            _imageChannelDataType = imageChannelDataType;
        }

        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
        private int GetElementSize()
        {
            switch (_imageChannelDataType)
            {
                case ImageChannelType.UnormShort565:
                case ImageChannelType.UnormShort555:
                    return 2;

                case ImageChannelType.UnormInt101010:
                    return 4;

                default:
                    var channelCount = ChannelOrderInfo.For(_imageChannelOrder).ChannelCount;
                    var dataSize = ChannelTypeInfo.For(_imageChannelDataType).DataSize;

                    return channelCount * dataSize;
            }
        }
    }
}
