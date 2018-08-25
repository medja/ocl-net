using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using OCL.Net.Native.Enums;

namespace OCL.Net.Internal
{
    public sealed class ChannelTypeInfo
    {
        public bool Normalized { get; }
        public Type DataType { get; }
        public int DataSize { get; }
        public ImageChannelType ChannelType { get; }

        public IReadOnlyCollection<ImageChannelOrder> SupportedChannelOrders => _supportedChannelOrders;

        private readonly HashSet<ImageChannelOrder> _supportedChannelOrders;

        private ChannelTypeInfo(bool normalized, Type dataType, ImageChannelType channelType)
            : this(normalized, dataType, channelType, AllChannelOrders)
        { }

        private ChannelTypeInfo(bool normalized, Type dataType, ImageChannelType channelType,
            params ImageChannelOrder[] supportedChannelOrders)
        {
            Normalized = normalized;
            DataType = dataType;
            DataSize = dataType == null ? 1 :Marshal.SizeOf(dataType);
            ChannelType = channelType;

            _supportedChannelOrders = new HashSet<ImageChannelOrder>(supportedChannelOrders);
        }

        public bool SupportsChannelOrder(ImageChannelOrder channelOrder)
        {
            return _supportedChannelOrders.Contains(channelOrder);
        }

        private static readonly ImageChannelOrder[] AllChannelOrders =
            Enum.GetValues(typeof(ImageChannelOrder)).Cast<ImageChannelOrder>().ToArray();

        private static readonly ChannelTypeInfo SnormInt8 =
            new ChannelTypeInfo(true, typeof(sbyte), ImageChannelType.SnormInt8);

        private static readonly ChannelTypeInfo SnormInt16 =
            new ChannelTypeInfo(true, typeof(short), ImageChannelType.SnormInt16);

        private static readonly ChannelTypeInfo UnormInt8 =
            new ChannelTypeInfo(true, typeof(byte), ImageChannelType.UnormInt8);

        private static readonly ChannelTypeInfo UnormInt16 =
            new ChannelTypeInfo(true, typeof(ushort), ImageChannelType.UnormInt16);

        private static readonly ChannelTypeInfo UnormShort565 =
            new ChannelTypeInfo(true, null, ImageChannelType.UnormShort565, ImageChannelOrder.Rgb, ImageChannelOrder.Rgbx);

        private static readonly ChannelTypeInfo UnormShort555 =
            new ChannelTypeInfo(true, null, ImageChannelType.UnormShort555, ImageChannelOrder.Rgb, ImageChannelOrder.Rgbx);

        private static readonly ChannelTypeInfo UnormInt101010 =
            new ChannelTypeInfo(true, null, ImageChannelType.UnormInt101010, ImageChannelOrder.Rgb, ImageChannelOrder.Rgbx);

        private static readonly ChannelTypeInfo SignedInt8 =
            new ChannelTypeInfo(false, typeof(sbyte), ImageChannelType.SignedInt8);

        private static readonly ChannelTypeInfo SignedInt16 =
            new ChannelTypeInfo(false, typeof(short), ImageChannelType.SignedInt16);

        private static readonly ChannelTypeInfo SignedInt32 =
            new ChannelTypeInfo(false, typeof(int), ImageChannelType.SignedInt32);

        private static readonly ChannelTypeInfo UnsignedInt8 =
            new ChannelTypeInfo(false, typeof(byte), ImageChannelType.UnsignedInt8);

        private static readonly ChannelTypeInfo UnsignedInt16 =
            new ChannelTypeInfo(false, typeof(ushort), ImageChannelType.UnsignedInt16);

        private static readonly ChannelTypeInfo UnsignedInt32 =
            new ChannelTypeInfo(false, typeof(uint), ImageChannelType.UnsignedInt32);

        private static readonly ChannelTypeInfo HalfFloat =
            new ChannelTypeInfo(true, null, ImageChannelType.HalfFloat);

        private static readonly ChannelTypeInfo Float =
            new ChannelTypeInfo(true, typeof(float), ImageChannelType.Float);

        public static readonly IReadOnlyList<ChannelTypeInfo> All = new List<ChannelTypeInfo>
        {
            SnormInt8,
            SnormInt16,
            UnormInt8,
            UnormInt16,
            UnormShort565,
            UnormShort555,
            UnormInt101010,
            SignedInt8,
            SignedInt16,
            SignedInt32,
            UnsignedInt8,
            UnsignedInt16,
            UnsignedInt32,
            HalfFloat,
            Float
        };

        public static ChannelTypeInfo For(ImageChannelType channelType)
        {
            switch (channelType)
            {
                case ImageChannelType.SnormInt8:
                    return SnormInt8;
                case ImageChannelType.SnormInt16:
                    return SnormInt16;
                case ImageChannelType.UnormInt8:
                    return UnormInt8;
                case ImageChannelType.UnormInt16:
                    return UnormInt16;
                case ImageChannelType.UnormShort565:
                    return UnormShort565;
                case ImageChannelType.UnormShort555:
                    return UnormShort555;
                case ImageChannelType.UnormInt101010:
                    return UnormInt101010;
                case ImageChannelType.SignedInt8:
                    return SignedInt8;
                case ImageChannelType.SignedInt16:
                    return SignedInt16;
                case ImageChannelType.SignedInt32:
                    return SignedInt32;
                case ImageChannelType.UnsignedInt8:
                    return UnsignedInt8;
                case ImageChannelType.UnsignedInt16:
                    return UnsignedInt16;
                case ImageChannelType.UnsignedInt32:
                    return UnsignedInt32;
                case ImageChannelType.HalfFloat:
                    return HalfFloat;
                case ImageChannelType.Float:
                    return Float;
                default:
                    throw new ArgumentOutOfRangeException(nameof(channelType), channelType, null);
            }
        }

        public static implicit operator ImageChannelType(ChannelTypeInfo info)
        {
            return info.ChannelType;
        }
    }
}
