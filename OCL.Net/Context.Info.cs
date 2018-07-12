using System;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Context
    {
        public Device[] Devices => LoadArray(ContextInfo.ContextDevices, ref _devices).ToDevices(Library);
        public uint NumDevices => LoadValue(ContextInfo.ContextNumDevices, ref _numDevices);
        public ContextProperties[] Properties => LoadArray(ContextInfo.ContextProperties, ref _properties).Cast<ContextProperties>();
        public override uint ReferenceCount => LoadValue<uint>(ContextInfo.ContextReferenceCount);

        private DeviceId[] _devices;
        private uint? _numDevices;
        private IntPtr[] _properties;
    }
}
