using System;
using System.Collections.Generic;
using System.Linq;
using OCL.Net.Native.Enums;

namespace OCL.Net.Internal
{
    public sealed class ChannelOrderInfo
    {
        public int ChannelCount { get; }
        public ImageChannelOrder ChannelOrder { get; }

        public IReadOnlyCollection<ImageChannelType> SupportedChannelOrders => _supportedChannelTypes;

        private readonly HashSet<ImageChannelType> _supportedChannelTypes;

        public ChannelOrderInfo(int channelCount, ImageChannelOrder channelOrder)
            : this(channelCount, channelOrder, AllChannelTypes)
        { }

        public ChannelOrderInfo(int channelCount, ImageChannelOrder channelOrder,
            params ImageChannelType[] supportedChannelTypes)
        {
            ChannelCount = channelCount;
            ChannelOrder = channelOrder;

            _supportedChannelTypes = new HashSet<ImageChannelType>(supportedChannelTypes);
        }

        public bool SupportsChannelType(ImageChannelType channelType)
        {
            return _supportedChannelTypes.Contains(channelType);
        }

        private static readonly ImageChannelType[] AllChannelTypes =
            Enum.GetValues(typeof(ImageChannelType)).Cast<ImageChannelType>().ToArray();

        private static readonly ChannelOrderInfo R =
            new ChannelOrderInfo(1, ImageChannelOrder.R);

        private static readonly ChannelOrderInfo A =
            new ChannelOrderInfo(1, ImageChannelOrder.A);

        private static readonly ChannelOrderInfo Rg =
            new ChannelOrderInfo(2, ImageChannelOrder.Rg);

        private static readonly ChannelOrderInfo Ra =
            new ChannelOrderInfo(2, ImageChannelOrder.Ra);

        private static readonly ChannelOrderInfo Rgb =
            new ChannelOrderInfo(3, ImageChannelOrder.Rgb,
                ImageChannelType.UnormShort565, ImageChannelType.UnormShort565,
                ImageChannelType.UnormInt101010);

        private static readonly ChannelOrderInfo Rgba =
            new ChannelOrderInfo(4, ImageChannelOrder.Rgba);

        private static readonly ChannelOrderInfo Bgra =
            new ChannelOrderInfo(4, ImageChannelOrder.Bgra,
                ImageChannelType.UnormInt8, ImageChannelType.SnormInt8,
                ImageChannelType.SignedInt8, ImageChannelType.UnsignedInt8);

        private static readonly ChannelOrderInfo Argb =
            new ChannelOrderInfo(4, ImageChannelOrder.Argb,
                ImageChannelType.UnormInt8, ImageChannelType.SnormInt8,
                ImageChannelType.SignedInt8, ImageChannelType.UnsignedInt8);

        private static readonly ChannelOrderInfo Intensity =
            new ChannelOrderInfo(1, ImageChannelOrder.Intensity,
                ImageChannelType.UnormInt8, ImageChannelType.UnormInt16,
                ImageChannelType.SnormInt8, ImageChannelType.SnormInt16,
                ImageChannelType.HalfFloat, ImageChannelType.Float);

        private static readonly ChannelOrderInfo Luminance =
            new ChannelOrderInfo(1, ImageChannelOrder.Luminance,
                ImageChannelType.UnormInt8, ImageChannelType.UnormInt16,
                ImageChannelType.SnormInt8, ImageChannelType.SnormInt16,
                ImageChannelType.HalfFloat, ImageChannelType.Float);

        private static readonly ChannelOrderInfo Rx =
            new ChannelOrderInfo(1, ImageChannelOrder.Rx);

        private static readonly ChannelOrderInfo Rgx =
            new ChannelOrderInfo(2, ImageChannelOrder.Rgx);

        private static readonly ChannelOrderInfo Rgbx =
            new ChannelOrderInfo(3, ImageChannelOrder.Rgbx,
                ImageChannelType.UnormShort565, ImageChannelType.UnormShort565,
                ImageChannelType.UnormInt101010);

        public static readonly IReadOnlyList<ChannelOrderInfo> All = new List<ChannelOrderInfo>
        {
            R,
            A,
            Rg,
            Ra,
            Rgb,
            Rgba,
            Bgra,
            Argb,
            Intensity,
            Luminance,
            Rx,
            Rgx,
            Rgbx
        };

        public static ChannelOrderInfo For(ImageChannelOrder channelOrder)
        {
            switch (channelOrder)
            {
                case ImageChannelOrder.R:
                    return R;
                case ImageChannelOrder.A:
                    return A;
                case ImageChannelOrder.Rg:
                    return Rg;
                case ImageChannelOrder.Ra:
                    return Ra;
                case ImageChannelOrder.Rgb:
                    return Rgb;
                case ImageChannelOrder.Rgba:
                    return Rgba;
                case ImageChannelOrder.Bgra:
                    return Bgra;
                case ImageChannelOrder.Argb:
                    return Argb;
                case ImageChannelOrder.Intensity:
                    return Intensity;
                case ImageChannelOrder.Luminance:
                    return Luminance;
                case ImageChannelOrder.Rx:
                    return Rx;
                case ImageChannelOrder.Rgx:
                    return Rgx;
                case ImageChannelOrder.Rgbx:
                    return Rgbx;
                default:
                    throw new ArgumentOutOfRangeException(nameof(channelOrder), channelOrder, null);
            }
        }

        public static implicit operator ImageChannelOrder(ChannelOrderInfo info)
        {
            return info.ChannelOrder;
        }
    }
}
