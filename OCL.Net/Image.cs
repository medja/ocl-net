using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image : MemoryObject
    {
        public new ImageId Id { get; }

        private protected Image(ImageId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib, handle, disposable)
        {
            Id = id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected unsafe T LoadValue<T>(ImageInfo info, ref T? cache) where T : unmanaged
        {
            return InfoLoader.LoadValue(info, GetImageInfo, ref cache);
        }

        public static implicit operator Image(ImageId id)
        {
            return FromId(id) as Image;
        }

        public static implicit operator ImageId(Image image)
        {
            return image.Id;
        }
    }
}
