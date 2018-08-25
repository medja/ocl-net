using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using OCL.Net.Native.Enums;

namespace OCL.Net.Internal
{
    public partial struct ColorValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorValue FromColor(Color color, ImageChannelType channelType)
        {
            return FromRgba(color.R, color.G, color.B, color.A, channelType);
        }

        public static ColorValue FromRgba(byte r, byte g, byte b, byte a, ImageChannelType channelType)
        {
            var format = new ColorValue();

            switch (channelType)
            {
                case ImageChannelType.SnormInt8:
                case ImageChannelType.SnormInt16:
                    format.Float.Red = ToSnorm(r);
                    format.Float.Green = ToSnorm(g);
                    format.Float.Blue = ToSnorm(b);
                    format.Float.Alpha = ToSnorm(a);
                    break;

                case ImageChannelType.UnormInt8:
                case ImageChannelType.UnormInt16:
                case ImageChannelType.UnormShort565:
                case ImageChannelType.UnormShort555:
                case ImageChannelType.UnormInt101010:
                    format.Float.Red = ToUnorm(r);
                    format.Float.Green = ToUnorm(g);
                    format.Float.Blue = ToUnorm(b);
                    format.Float.Alpha = ToUnorm(a);
                    break;

                case ImageChannelType.HalfFloat:
                case ImageChannelType.Float:
                    format.Float.Red = ToUnorm(r);
                    format.Float.Green = ToUnorm(g);
                    format.Float.Blue = ToUnorm(b);
                    format.Float.Alpha = ToUnorm(a);
                    break;

                case ImageChannelType.SignedInt8:
                    format.Int.Red = ToSignedInt8(r);
                    format.Int.Green = ToSignedInt8(g);
                    format.Int.Blue = ToSignedInt8(b);
                    format.Int.Alpha = ToSignedInt8(a);
                    break;

                case ImageChannelType.SignedInt16:
                    format.Int.Red = ToSignedInt16(r);
                    format.Int.Green = ToSignedInt16(g);
                    format.Int.Blue = ToSignedInt16(b);
                    format.Int.Alpha = ToSignedInt16(a);
                    break;

                case ImageChannelType.SignedInt32:
                    format.Int.Red = ToSignedInt32(r);
                    format.Int.Green = ToSignedInt32(g);
                    format.Int.Blue = ToSignedInt32(b);
                    format.Int.Alpha = ToSignedInt32(a);
                    break;

                case ImageChannelType.UnsignedInt8:
                    format.UInt.Red = ToUnsignedInt8(r);
                    format.UInt.Green = ToUnsignedInt8(g);
                    format.UInt.Blue = ToUnsignedInt8(b);
                    format.UInt.Alpha = ToUnsignedInt8(a);
                    break;

                case ImageChannelType.UnsignedInt16:
                    format.UInt.Red = ToUnsignedInt16(r);
                    format.UInt.Green = ToUnsignedInt16(g);
                    format.UInt.Blue = ToUnsignedInt16(b);
                    format.UInt.Alpha = ToUnsignedInt16(a);
                    break;

                case ImageChannelType.UnsignedInt32:
                    format.UInt.Red = ToUnsignedInt32(r);
                    format.UInt.Green = ToUnsignedInt32(g);
                    format.UInt.Blue = ToUnsignedInt32(b);
                    format.UInt.Alpha = ToUnsignedInt32(a);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(channelType), channelType, null);
            }

            return format;
        }

        public static ColorValue FromRgba(double r, double g, double b, double a, ImageChannelType channelType)
        {
            var format = new ColorValue();

            switch (channelType)
            {
                case ImageChannelType.SnormInt8:
                case ImageChannelType.SnormInt16:
                    format.Float.Red = ToSnorm(r);
                    format.Float.Green = ToSnorm(g);
                    format.Float.Blue = ToSnorm(b);
                    format.Float.Alpha = ToSnorm(a);
                    break;

                case ImageChannelType.UnormInt8:
                case ImageChannelType.UnormInt16:
                case ImageChannelType.UnormShort565:
                case ImageChannelType.UnormShort555:
                case ImageChannelType.UnormInt101010:
                    format.Float.Red = ToUnorm(r);
                    format.Float.Green = ToUnorm(g);
                    format.Float.Blue = ToUnorm(b);
                    format.Float.Alpha = ToUnorm(a);
                    break;

                case ImageChannelType.HalfFloat:
                case ImageChannelType.Float:
                    format.Float.Red = ToUnorm(r);
                    format.Float.Green = ToUnorm(g);
                    format.Float.Blue = ToUnorm(b);
                    format.Float.Alpha = ToUnorm(a);
                    break;

                case ImageChannelType.SignedInt8:
                    format.Int.Red = ToSignedInt8(r);
                    format.Int.Green = ToSignedInt8(g);
                    format.Int.Blue = ToSignedInt8(b);
                    format.Int.Alpha = ToSignedInt8(a);
                    break;

                case ImageChannelType.SignedInt16:
                    format.Int.Red = ToSignedInt16(r);
                    format.Int.Green = ToSignedInt16(g);
                    format.Int.Blue = ToSignedInt16(b);
                    format.Int.Alpha = ToSignedInt16(a);
                    break;

                case ImageChannelType.SignedInt32:
                    format.Int.Red = ToSignedInt32(r);
                    format.Int.Green = ToSignedInt32(g);
                    format.Int.Blue = ToSignedInt32(b);
                    format.Int.Alpha = ToSignedInt32(a);
                    break;

                case ImageChannelType.UnsignedInt8:
                    format.UInt.Red = ToUnsignedInt8(r);
                    format.UInt.Green = ToUnsignedInt8(g);
                    format.UInt.Blue = ToUnsignedInt8(b);
                    format.UInt.Alpha = ToUnsignedInt8(a);
                    break;

                case ImageChannelType.UnsignedInt16:
                    format.UInt.Red = ToUnsignedInt16(r);
                    format.UInt.Green = ToUnsignedInt16(g);
                    format.UInt.Blue = ToUnsignedInt16(b);
                    format.UInt.Alpha = ToUnsignedInt16(a);
                    break;

                case ImageChannelType.UnsignedInt32:
                    format.UInt.Red = ToUnsignedInt32(r);
                    format.UInt.Green = ToUnsignedInt32(g);
                    format.UInt.Blue = ToUnsignedInt32(b);
                    format.UInt.Alpha = ToUnsignedInt32(a);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(channelType), channelType, null);
            }

            return format;
        }
    }
}
