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
    public sealed partial class Platform
    {
        public IEnumerable<Device> QueryDevices(DeviceType type = DeviceType.Default)
        {
            return LoadDevices(Library, Id, type).Select(id => Device.FromId(Library, id));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Platform> QueryPlatforms()
        {
            return QueryPlatforms(OpenCl.Libraries);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Platform> QueryPlatforms(IOpenCl[] libraries)
        {
            return QueryPlatforms((IEnumerable<IOpenCl>) libraries);
        }

        public static IEnumerable<Platform> QueryPlatforms(IEnumerable<IOpenCl> libraries)
        {
            if (libraries == null)
                throw new ArgumentNullException(nameof(libraries));

            if (!(libraries is ICollection<IOpenCl> collection))
                collection = libraries.ToList();

            if (collection.Any(library => library == null))
                throw new ArgumentException("Libraries cannot be null", nameof(libraries));

            return collection.SelectMany(LoadPlatforms, FromId);
        }

        private static IEnumerable<PlatformId> LoadPlatforms(IOpenCl library)
        {
            library.clGetPlatformIDs(0, null, out var count).HandleError();
            var platforms = new PlatformId[count];
            library.clGetPlatformIDs(count, platforms, out _).HandleError();

            return platforms;
        }

        private static IEnumerable<DeviceId> LoadDevices(IOpenCl library, PlatformId platform, DeviceType type)
        {
            library.clGetDeviceIDs(platform, type, 0, null, out var count).HandleError();
            var devices = new DeviceId[count];
            library.clGetDeviceIDs(platform, type, count, devices, out _).HandleError();

            return devices;
        }
    }
}
