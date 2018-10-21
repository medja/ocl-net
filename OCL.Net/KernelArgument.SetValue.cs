using System;
using OCL.Net.Internal;

namespace OCL.Net
{
    public sealed partial class KernelArgument
    {
        private unsafe bool SetSByte<T>(T value) where T : unmanaged
        {
            sbyte convertedValue;

            switch (value)
            {
                case sbyte sbyteValue:
                    convertedValue = sbyteValue;
                    break;

                case byte byteValue when byteValue <= sbyte.MaxValue:
                    convertedValue = (sbyte) byteValue;
                    break;

                case short shortValue when shortValue >= 0 && shortValue <= sbyte.MaxValue:
                    convertedValue = (sbyte) shortValue;
                    break;

                case ushort ushortValue when ushortValue <= sbyte.MaxValue:
                    convertedValue = (sbyte) ushortValue;
                    break;

                case int intValue when intValue >= 0 && intValue <= sbyte.MaxValue:
                    convertedValue = (sbyte) intValue;
                    break;

                case uint uintValue when uintValue <= sbyte.MaxValue:
                    convertedValue = (sbyte) uintValue;
                    break;

                case long longValue when longValue >= 0 && longValue <= sbyte.MaxValue:
                    convertedValue = (sbyte) longValue;
                    break;

                case ulong ulongValue when ulongValue <= (int) sbyte.MaxValue:
                    convertedValue = (sbyte) ulongValue;
                    break;

                case IntPtr intPtrValue:
                    var nativeIntValue = intPtrValue.ToInt64();

                    if (nativeIntValue >= 0 && nativeIntValue <= sbyte.MaxValue)
                        convertedValue = (sbyte) nativeIntValue;
                    else
                        return false;

                    break;

                case UIntPtr uIntPtrValue:
                    var nativeUIntValue = uIntPtrValue.ToUInt64();

                    if (nativeUIntValue <= (int) sbyte.MaxValue)
                        convertedValue = (sbyte) nativeUIntValue;
                    else
                        return false;

                    break;

                default:
                    try { convertedValue = Convert.ToSByte(value); }
                    catch { return false; }
                    break;
            }

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(sbyte), &convertedValue).HandleError();
            return true;
        }

        private unsafe bool SetByte<T>(T value) where T : unmanaged
        {
            byte convertedValue;

            switch (value)
            {
                case sbyte sbyteValue when sbyteValue >= 0:
                    convertedValue = (byte) sbyteValue;
                    break;

                case byte byteValue:
                    convertedValue = byteValue;
                    break;

                case short shortValue when shortValue >= 0 && shortValue <= byte.MaxValue:
                    convertedValue = (byte) shortValue;
                    break;

                case ushort ushortValue when ushortValue <= byte.MaxValue:
                    convertedValue = (byte) ushortValue;
                    break;

                case int intValue when intValue >= 0 && intValue <= byte.MaxValue:
                    convertedValue = (byte) intValue;
                    break;

                case uint uintValue when uintValue <= byte.MaxValue:
                    convertedValue = (byte) uintValue;
                    break;

                case long longValue when longValue >= 0 && longValue <= byte.MaxValue:
                    convertedValue = (byte) longValue;
                    break;

                case ulong ulongValue when ulongValue <= byte.MaxValue:
                    convertedValue = (byte) ulongValue;
                    break;

                case IntPtr intPtrValue:
                    var nativeIntValue = intPtrValue.ToInt64();

                    if (nativeIntValue >= 0 && nativeIntValue <= byte.MaxValue)
                        convertedValue = (byte) nativeIntValue;
                    else
                        return false;

                    break;

                case UIntPtr uIntPtrValue:
                    var nativeUIntValue = uIntPtrValue.ToUInt64();

                    if (nativeUIntValue <= byte.MaxValue)
                        convertedValue = (byte) nativeUIntValue;
                    else
                        return false;

                    break;

                default:
                    try { convertedValue = Convert.ToByte(value); }
                    catch { return false; }
                    break;
            }

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(byte), &convertedValue).HandleError();
            return true;
        }

        private unsafe bool SetShort<T>(T value) where T : unmanaged
        {
            short convertedValue;

            switch (value)
            {
                case sbyte sbyteValue:
                    convertedValue = sbyteValue;
                    break;

                case byte byteValue:
                    convertedValue = byteValue;
                    break;

                case short shortValue:
                    convertedValue = shortValue;
                    break;

                case ushort ushortValue when ushortValue <= short.MaxValue:
                    convertedValue = (short) ushortValue;
                    break;

                case int intValue when intValue >= 0 && intValue <= short.MaxValue:
                    convertedValue = (short) intValue;
                    break;

                case uint uintValue when uintValue <= short.MaxValue:
                    convertedValue = (short) uintValue;
                    break;

                case long longValue when longValue >= 0 && longValue <= short.MaxValue:
                    convertedValue = (short) longValue;
                    break;

                case ulong ulongValue when ulongValue <= (int) short.MaxValue:
                    convertedValue = (short) ulongValue;
                    break;

                case IntPtr intPtrValue:
                    var nativeIntValue = intPtrValue.ToInt64();

                    if (nativeIntValue >= 0 && nativeIntValue <= byte.MaxValue)
                        convertedValue = (short) nativeIntValue;
                    else
                        return false;

                    break;

                case UIntPtr uIntPtrValue:
                    var nativeUIntValue = uIntPtrValue.ToUInt64();

                    if (nativeUIntValue <= (int) short.MaxValue)
                        convertedValue = (short) nativeUIntValue;
                    else
                        return false;

                    break;

                default:
                    try { convertedValue = Convert.ToInt16(value); }
                    catch { return false; }
                    break;
            }

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(short), &convertedValue).HandleError();
            return true;
        }

        private unsafe bool SetUShort<T>(T value) where T : unmanaged
        {
            ushort convertedValue;

            switch (value)
            {
                case sbyte sbyteValue when sbyteValue >= 0:
                    convertedValue = (ushort) sbyteValue;
                    break;

                case byte byteValue:
                    convertedValue = byteValue;
                    break;

                case short shortValue when shortValue >= 0:
                    convertedValue = (ushort) shortValue;
                    break;

                case ushort ushortValue:
                    convertedValue = ushortValue;
                    break;

                case int intValue when intValue >= 0 && intValue <= ushort.MaxValue:
                    convertedValue = (ushort) intValue;
                    break;

                case uint uintValue when uintValue <= ushort.MaxValue:
                    convertedValue = (ushort) uintValue;
                    break;

                case long longValue when longValue >= 0 && longValue <= ushort.MaxValue:
                    convertedValue = (ushort) longValue;
                    break;

                case ulong ulongValue when ulongValue <= ushort.MaxValue:
                    convertedValue = (ushort) ulongValue;
                    break;

                case IntPtr intPtrValue:
                    var nativeIntValue = intPtrValue.ToInt64();

                    if (nativeIntValue >= 0 && nativeIntValue <= byte.MaxValue)
                        convertedValue = (ushort) nativeIntValue;
                    else
                        return false;

                    break;

                case UIntPtr uIntPtrValue:
                    var nativeUIntValue = uIntPtrValue.ToUInt64();

                    if (nativeUIntValue <= ushort.MaxValue)
                        convertedValue = (ushort) nativeUIntValue;
                    else
                        return false;

                    break;

                default:
                    try { convertedValue = Convert.ToUInt16(value); }
                    catch { return false; }
                    break;
            }

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(ushort), &convertedValue).HandleError();
            return true;
        }

        private unsafe bool SetInt<T>(T value) where T : unmanaged
        {
            int convertedValue;

            switch (value)
            {
                case byte byteValue:
                    convertedValue = byteValue;
                    break;

                case sbyte sbyteValue:
                    convertedValue = sbyteValue;
                    break;

                case short shortValue:
                    convertedValue = shortValue;
                    break;

                case ushort ushortValue:
                    convertedValue = ushortValue;
                    break;

                case int intValue:
                    convertedValue = intValue;
                    break;

                case uint uintValue when uintValue <= int.MaxValue:
                    convertedValue = (int) uintValue;
                    break;

                case long longValue when longValue >= 0 && longValue <= int.MaxValue:
                    convertedValue = (int) longValue;
                    break;

                case ulong ulongValue when ulongValue <= int.MaxValue:
                    convertedValue = (int) ulongValue;
                    break;

                case IntPtr intPtrValue:
                    var nativeIntValue = intPtrValue.ToInt64();

                    if (nativeIntValue >= 0 && nativeIntValue <= byte.MaxValue)
                        convertedValue = (int) nativeIntValue;
                    else
                        return false;

                    break;

                case UIntPtr uIntPtrValue:
                    var nativeUIntValue = uIntPtrValue.ToUInt64();

                    if (nativeUIntValue <= int.MaxValue)
                        convertedValue = (int) nativeUIntValue;
                    else
                        return false;

                    break;

                default:
                    try { convertedValue = Convert.ToInt32(value); }
                    catch { return false; }
                    break;
            }

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(int), &convertedValue).HandleError();
            return true;
        }

        private unsafe bool SetUInt<T>(T value) where T : unmanaged
        {
            uint convertedValue;

            switch (value)
            {
                case sbyte sbyteValue when sbyteValue >= 0:
                    convertedValue = (uint) sbyteValue;
                    break;

                case byte byteValue:
                    convertedValue = byteValue;
                    break;

                case short shortValue when shortValue >= 0:
                    convertedValue = (uint) shortValue;
                    break;

                case ushort ushortValue:
                    convertedValue = ushortValue;
                    break;

                case int intValue when intValue >= 0:
                    convertedValue = (uint) intValue;
                    break;

                case uint uintValue:
                    convertedValue = uintValue;
                    break;

                case long longValue when longValue >= 0 && longValue <= uint.MaxValue:
                    convertedValue = (uint) longValue;
                    break;

                case ulong ulongValue when ulongValue <= uint.MaxValue:
                    convertedValue = (uint) ulongValue;
                    break;

                case IntPtr intPtrValue:
                    var nativeIntValue = intPtrValue.ToInt64();

                    if (nativeIntValue >= 0 && nativeIntValue <= byte.MaxValue)
                        convertedValue = (uint) nativeIntValue;
                    else
                        return false;

                    break;

                case UIntPtr uIntPtrValue:
                    var nativeUIntValue = uIntPtrValue.ToUInt64();

                    if (nativeUIntValue <= uint.MaxValue)
                        convertedValue = (uint) nativeUIntValue;
                    else
                        return false;

                    break;

                default:
                    try { convertedValue = Convert.ToUInt32(value); }
                    catch { return false; }
                    break;
            }

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(uint), &convertedValue).HandleError();
            return true;
        }

        private unsafe bool SetLong<T>(T value) where T : unmanaged
        {
            long convertedValue;

            switch (value)
            {
                case byte byteValue:
                    convertedValue = byteValue;
                    break;

                case sbyte sbyteValue:
                    convertedValue = sbyteValue;
                    break;

                case short shortValue:
                    convertedValue = shortValue;
                    break;

                case ushort ushortValue:
                    convertedValue = ushortValue;
                    break;

                case int intValue:
                    convertedValue = intValue;
                    break;

                case uint uintValue:
                    convertedValue = uintValue;
                    break;

                case long longValue:
                    convertedValue = longValue;
                    break;

                case ulong ulongValue when ulongValue <= long.MaxValue:
                    convertedValue = (long) ulongValue;
                    break;

                case IntPtr intPtrValue:
                    convertedValue = intPtrValue.ToInt64();
                    break;

                case UIntPtr uIntPtrValue:
                    var nativeUIntValue = uIntPtrValue.ToUInt64();

                    if (nativeUIntValue <= long.MaxValue)
                        convertedValue = (long) nativeUIntValue;
                    else
                        return false;

                    break;

                default:
                    try { convertedValue = Convert.ToInt64(value); }
                    catch { return false; }
                    break;
            }

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(long), &convertedValue).HandleError();
            return true;
        }

        private unsafe bool SetULong<T>(T value) where T : unmanaged
        {
            ulong convertedValue;

            switch (value)
            {
                case sbyte sbyteValue when sbyteValue >= 0:
                    convertedValue = (ulong) sbyteValue;
                    break;

                case byte byteValue:
                    convertedValue = byteValue;
                    break;

                case short shortValue when shortValue >= 0:
                    convertedValue = (ulong) shortValue;
                    break;

                case ushort ushortValue:
                    convertedValue = ushortValue;
                    break;

                case int intValue when intValue >= 0:
                    convertedValue = (ulong) intValue;
                    break;

                case uint uintValue:
                    convertedValue = uintValue;
                    break;

                case long longValue when longValue >= 0:
                    convertedValue = (ulong) longValue;
                    break;

                case ulong ulongValue:
                    convertedValue = ulongValue;
                    break;

                case IntPtr intPtrValue:
                    var nativeIntValue = intPtrValue.ToInt64();

                    if (nativeIntValue >= 0)
                        convertedValue = (ulong) nativeIntValue;
                    else
                        return false;

                    break;

                case UIntPtr uIntPtrValue:
                    convertedValue = uIntPtrValue.ToUInt64();
                    break;

                default:
                    try { convertedValue = Convert.ToUInt64(value); }
                    catch { return false; }
                    break;
            }

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(ulong), &convertedValue).HandleError();
            return true;
        }

        private unsafe bool SetFloat<T>(T value) where T : unmanaged
        {
            float convertedValue;

            switch (value)
            {
                case sbyte sbyteValue:
                    convertedValue = sbyteValue;
                    break;

                case byte byteValue:
                    convertedValue = byteValue;
                    break;

                case short shortValue:
                    convertedValue = shortValue;
                    break;

                case ushort ushortValue:
                    convertedValue = ushortValue;
                    break;

                case int intValue:
                    convertedValue = intValue;
                    break;

                case uint uintValue:
                    convertedValue = uintValue;
                    break;

                case long longValue:
                    convertedValue = longValue;
                    break;

                case ulong ulongValue:
                    convertedValue = ulongValue;
                    break;

                case IntPtr intPtrValue:
                    convertedValue = intPtrValue.ToInt64();
                    break;

                case UIntPtr uIntPtrValue:
                    convertedValue = uIntPtrValue.ToUInt64();
                    break;

                default:
                    try { convertedValue = Convert.ToSingle(value); }
                    catch { return false; }
                    break;
            }

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(float), &convertedValue).HandleError();
            return true;
        }

        private unsafe bool SetDouble<T>(T value) where T : unmanaged
        {
            double convertedValue;

            switch (value)
            {
                case sbyte sbyteValue:
                    convertedValue = sbyteValue;
                    break;

                case byte byteValue:
                    convertedValue = byteValue;
                    break;

                case short shortValue:
                    convertedValue = shortValue;
                    break;

                case ushort ushortValue:
                    convertedValue = ushortValue;
                    break;

                case int intValue:
                    convertedValue = intValue;
                    break;

                case uint uintValue:
                    convertedValue = uintValue;
                    break;

                case long longValue:
                    convertedValue = longValue;
                    break;

                case ulong ulongValue:
                    convertedValue = ulongValue;
                    break;

                case IntPtr intPtrValue:
                    convertedValue = intPtrValue.ToInt64();
                    break;

                case UIntPtr uIntPtrValue:
                    convertedValue = uIntPtrValue.ToUInt64();
                    break;

                default:
                    try { convertedValue = Convert.ToDouble(value); }
                    catch { return false; }
                    break;
            }

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(double), &convertedValue).HandleError();
            return true;
        }
    }
}
