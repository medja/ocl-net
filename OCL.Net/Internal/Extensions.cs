using System;
using System.Diagnostics.CodeAnalysis;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static class Extensions
    {
        public static void HandleError(this ErrorCode errorCode)
        {
            if (errorCode < ErrorCode.Success)
                throw new OpenClException(errorCode);
        }

        public static ErrorCode GetErrorCode(this CommandExecutionStatus status)
        {
            if (status < 0)
                return (ErrorCode) status;

            return ErrorCode.Success;
        }

        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
        public static MemoryFlags ToDeviceFlags(this MemFlags memFlags)
        {
            const MemFlags deviceFlags = MemFlags.MemReadOnly | MemFlags.MemWriteOnly;

            switch (memFlags & deviceFlags)
            {
                case MemFlags.MemReadWrite:
                    return MemoryFlags.Read;
                case MemFlags.MemWriteOnly:
                    return MemoryFlags.Write;
                default:
                    return MemoryFlags.Read | MemoryFlags.Write;
            }
        }

        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
        public static MemoryFlags ToHostFlags(this MemFlags memFlags)
        {
            const MemFlags hostFlags = MemFlags.MemHostReadOnly | MemFlags.MemHostWriteOnly | MemFlags.MemHostNoAccess;

            switch (memFlags & hostFlags)
            {
                case MemFlags.MemHostWriteOnly:
                    return MemoryFlags.Read;
                case MemFlags.MemHostReadOnly:
                    return MemoryFlags.Write;
                case MemFlags.MemHostNoAccess:
                    return 0;
                default:
                    return MemoryFlags.Read | MemoryFlags.Write;
            }
        }

        public static MemFlags ToDeviceNativeFlags(this MemoryFlags memoryFlags)
        {
            switch (memoryFlags)
            {
                case MemoryFlags.Read:
                    return MemFlags.MemReadOnly;
                case MemoryFlags.Write:
                    return MemFlags.MemWriteOnly;
                case 0:
                    return 0;
                default:
                    return MemFlags.MemReadWrite;
            }
        }

        public static MemFlags ToHostNativeFlags(this MemoryFlags memoryFlags)
        {
            switch (memoryFlags)
            {
                case MemoryFlags.Read:
                    return MemFlags.MemHostReadOnly;
                case MemoryFlags.Write:
                    return MemFlags.MemHostWriteOnly;
                case 0:
                    return MemFlags.MemHostNoAccess;
                default:
                    return 0;
            }
        }

        public static Device[] ToDevices(this DeviceId[] ids, IOpenCl library)
        {
            var devices = new Device[ids.Length];

            for (var i = 0; i < devices.Length; i++)
                devices[i] = Device.FromId(library, ids[i]);

            return devices;
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
