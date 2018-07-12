using System;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Device : RefCountedType<DeviceId, DeviceInfo>
    {
        private Device(DeviceId id, IOpenCl lib) : base(id, lib)
        { }

        protected override ErrorCode GetInfo(DeviceInfo info, UIntPtr bufferSize, IntPtr buffer, out UIntPtr size)
        {
            return Library.clGetDeviceInfo(Id, info, bufferSize, buffer, out size);
        }

        protected override ErrorCode RetainObject()
        {
            return Library.clRetainDevice(Id);
        }

        protected override ErrorCode ReleaseObject()
        {
            return Library.clReleaseDevice(Id);
        }

        public static implicit operator Device(DeviceId id)
        {
            return FromId(id) as Device;
        }

        public static implicit operator DeviceId(Device device)
        {
            return device.Id;
        }

        internal static Device FromId(IOpenCl lib, DeviceId id)
        {
            return FromId(id) as Device ?? new Device(id, lib);
        }
    }
}
