using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace OCL.Net.Internal
{
    public partial struct ColorValue
    {
        #region ToUnorm

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float ToUnorm(byte value)
        {
            return (float) (value / (double) byte.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float ToUnorm(double value)
        {
            return (float) value;
        }

        #endregion

        #region ToSnorm

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float ToSnorm(byte value)
        {
            return (float) (value * 2.0 / byte.MaxValue - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float ToSnorm(double value)
        {
            return (float) (value * 2 - 1);
        }

        #endregion

        #region ToUnsignedInt8

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte ToUnsignedInt8(byte value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        private static byte ToUnsignedInt8(double value)
        {
            return (byte) Math.Round(value * byte.MaxValue);
        }

        #endregion

        #region ToSignedInt8

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static sbyte ToSignedInt8(byte value)
        {
            return (sbyte) (value + sbyte.MinValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        private static sbyte ToSignedInt8(double value)
        {
            return (sbyte) Math.Round(value * byte.MaxValue + sbyte.MinValue);
        }

        #endregion

        #region ToUnsignedInt16

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ushort ToUnsignedInt16(byte value)
        {
            const uint ratio = ushort.MaxValue / byte.MaxValue;
            return (ushort) (value * ratio);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        private static ushort ToUnsignedInt16(double value)
        {
            return (ushort) Math.Round(value * ushort.MaxValue);
        }

        #endregion

        #region ToSignedInt16

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static short ToSignedInt16(byte value)
        {
            const uint ratio = ushort.MaxValue / byte.MaxValue;
            return (short) (value * ratio + short.MinValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        private static short ToSignedInt16(double value)
        {
            return (short) Math.Round(value * ushort.MaxValue + short.MinValue);
        }

        #endregion

        #region ToUnsignedInt32

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint ToUnsignedInt32(byte value)
        {
            const uint ratio = uint.MaxValue / byte.MaxValue;
            return value * ratio;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint ToUnsignedInt32(double value)
        {
            return (uint) Math.Round(value * uint.MaxValue);
        }

        #endregion

        #region ToSignedInt32

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ToSignedInt32(byte value)
        {
            const uint ratio = uint.MaxValue / byte.MaxValue;
            return (int) (value * ratio + int.MinValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ToSignedInt32(double value)
        {
            return (int) Math.Round(value * uint.MaxValue + int.MinValue);
        }

        #endregion
    }
}
