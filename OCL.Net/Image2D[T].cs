using System;
using System.Buffers;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2D<T> : Image2D
        where T : unmanaged
    {
        private Image2D(ImageId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib, handle, disposable)
        { }

        public static implicit operator Image2D<T>(ImageId id)
        {
            return FromId(id) as Image2D<T>;
        }

        internal static Image2D<T> FromId(IOpenCl lib, ImageId id,
            MemoryHandle handle = default, IDisposable disposable = null)
        {
            return FromId(id) as Image2D<T> ?? new Image2D<T>(id, lib, handle, disposable);
        }
    }
}
