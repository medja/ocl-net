using System;
using System.Collections.Generic;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Program
    {
        public Dictionary<Device, byte[]> Binaries => LoadBinaries();
        public ulong[] BinarySizes => LoadArray<UIntPtr>(ProgramInfo.ProgramBinarySizes).Cast<ulong>();
        public Context Context => Context.FromId(Library, LoadValue(ProgramInfo.ProgramContext, ref _context));
        public Device[] Devices => LoadArray(ProgramInfo.ProgramDevices, ref _devices).ToDevices(Library);
        public uint NumDevices => LoadValue(ProgramInfo.ProgramNumDevices, ref _numDevices);
        public ulong NumKernels => LoadValue(ProgramInfo.ProgramNumKernels, ref _numKernels).ToUInt64();
        public string[] KernelNames => LoadString(ProgramInfo.ProgramKernelNames, ref _kernelNames).Split(';');
        public override uint ReferenceCount => LoadValue<uint>(ProgramInfo.ProgramReferenceCount);
        public string Source => LoadString(ProgramInfo.ProgramSource, ref _source);

        private ContextId? _context;
        private DeviceId[] _devices;
        private uint? _numDevices;
        private UIntPtr? _numKernels;
        private string _kernelNames;
        private string _source;

        private unsafe Dictionary<Device, byte[]> LoadBinaries()
        {
            var binarySizes = BinarySizes;

            var buffers = new PinnedBuffer[binarySizes.Length];
            var pointers = new byte*[binarySizes.Length];

            for (var i = 0; i < binarySizes.Length; i++)
            {
                var buffer = new PinnedBuffer(binarySizes[i]);

                buffers[i] = buffer;
                pointers[i] = (byte*) buffer.Handle.Pointer;
            }

            var size = (UIntPtr) (IntPtr.Size * buffers.Length);
            ErrorCode errorCode;

            fixed (byte** ptr = pointers)
                errorCode = GetInfo(ProgramInfo.ProgramBinaries, size, (byte*) ptr, null);

            foreach (var buffer in buffers)
                buffer.Dispose();

            errorCode.HandleError();

            var result = new Dictionary<Device, byte[]>();
            var devices = Devices;

            for (var i = 0; i < buffers.Length && i < devices.Length; i++)
            {
                if (buffers.Length > 0)
                    result[devices[i]] = buffers[i].Buffer;
            }

            return result;
        }
    }
}
