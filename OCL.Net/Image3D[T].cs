using System;
using System.Buffers;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image3D<T> : Image3D
        where T : unmanaged
    {
        private Image3D(ImageId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib, handle, disposable)
        { }

        public static implicit operator Image3D<T>(ImageId id)
        {
            return FromId(id) as Image3D<T>;
        }

        internal static Image3D<T> FromId(IOpenCl lib, ImageId id,
            MemoryHandle handle = default, IDisposable disposable = null)
        {
            return FromId(id) as Image3D<T> ?? new Image3D<T>(id, lib, handle, disposable);
        }
    }
}
