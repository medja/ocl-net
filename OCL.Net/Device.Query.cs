using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public sealed partial class Device
    {
        public static IEnumerable<Device> QueryDevices(DeviceType type = DeviceType.Default)
        {
            return QueryDevices(type, OpenCl.Libraries);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Device> QueryDevices(DeviceType type, params IOpenCl[] libraries)

        {
            return QueryDevices(type, (IEnumerable<IOpenCl>) libraries);
        }

        public static IEnumerable<Device> QueryDevices(DeviceType type, IEnumerable<IOpenCl> libraries)
        {
            if (libraries == null)
                throw new ArgumentNullException(nameof(libraries));

            if (!(libraries is ICollection<IOpenCl> collection))
                collection = libraries.ToList();

            if (collection.Any(library => library == null))
                throw new ArgumentException("Libraries cannot be null", nameof(libraries));

            return collection.SelectMany(library => LoadDevices(library, type), FromId);
        }

        private static unsafe IEnumerable<DeviceId> LoadDevices(IOpenCl library, DeviceType type)
        {
            uint count;
            library.clGetDeviceIDsUnsafe(default, type, 0, null, &count).HandleError();

            var devices = new DeviceId[count];

            fixed (DeviceId* devicesPtr = devices)
                library.clGetDeviceIDsUnsafe(default, type, count, devicesPtr, null).HandleError();

            return devices;
        }
    }
}
