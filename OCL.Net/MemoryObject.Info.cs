using System;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class MemoryObject
    {
        public Context Context => Context.FromId(Library, LoadValue(MemInfo.MemContext, ref _context));
        public MemFlags Flags => LoadValue(MemInfo.MemFlags, ref _flags);
        public IntPtr HostPtr => LoadValue(MemInfo.MemHostPtr, ref _hostPtr);
        public uint MapCount => LoadValue(MemInfo.MemMapCount, ref _mapCount);
        public ulong Offset => LoadValue(MemInfo.MemOffset, ref _offset).ToUInt64();
        public override uint ReferenceCount => LoadValue<uint>(MemInfo.MemReferenceCount);
        public ulong Size => LoadValue(MemInfo.MemSize, ref _size).ToUInt64();
        public MemObjectType Type => LoadMemObjectType();

        public MemoryFlags DeviceFlags => Flags.ToDeviceFlags();
        public MemoryFlags HostFlags => Flags.ToHostFlags();

        private ContextId? _context;
        private MemFlags? _flags;
        private IntPtr? _hostPtr;
        private uint? _mapCount;
        private UIntPtr? _offset;
        private UIntPtr? _size;
        private MemObjectType? _type;

        private unsafe MemObjectType LoadMemObjectType()
        {
            if (_type.HasValue)
                return _type.Value;

            var memObjectType = LoadValue<MemObjectType>(MemInfo.MemType);

            if (memObjectType == MemObjectType.MemObjectImage1D)
            {
                var bufferId = InfoLoader.LoadValue<BufferId, ImageInfo>(ImageInfo.ImageBuffer, GetImageInfo);

                if (bufferId.Handle != IntPtr.Zero)
                    memObjectType = MemObjectType.MemObjectImage1DBuffer;
            }

            _type = memObjectType;
            return memObjectType;
        }
    }
}
