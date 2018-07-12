using System;
using System.Diagnostics.CodeAnalysis;
using OCL.Net.Native.Enums;

namespace OCL.Net.Internal
{
    public static class Extensions
    {
        public static void HandleError(this ErrorCode errorCode)
        {
            if (errorCode < ErrorCode.Success)
                throw new OpenClException(errorCode);
        }

        [SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Global")]
        public static T[] Cast<T>(this IntPtr[] values)
        {
            var type = typeof(T);
            var result = new T[values.Length];

            if (type.IsEnum)
            {
                for (var i = 0; i < values.Length; i++)
                    result[i] = (T) Enum.ToObject(type, values[i].ToInt64());
            }
            else
            {
                for (var i = 0; i < values.Length; i++)
                    result[i] = (T) Convert.ChangeType(values[i].ToInt64(), typeof(T));
            }

            return result;
        }

        [SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Global")]
        public static T[] Cast<T>(this UIntPtr[] values)
        {
            var type = typeof(T);
            var result = new T[values.Length];

            if (type.IsEnum)
            {
                for (var i = 0; i < values.Length; i++)
                    result[i] = (T) Enum.ToObject(type, values[i].ToUInt64());
            }
            else
            {
                for (var i = 0; i < values.Length; i++)
                    result[i] = (T) Convert.ChangeType(values[i].ToUInt64(), typeof(T));
            }

            return result;
        }
    }
}
