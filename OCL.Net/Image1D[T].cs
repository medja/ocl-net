using System;
using System.Buffers;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1D<T> : Image1D
        where T : unmanaged
    {
        private Image1D(ImageId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib, handle, disposable)
        { }

        public static implicit operator Image1D<T>(ImageId id)
        {
            return FromId(id) as Image1D<T>;
        }

        internal static Image1D<T> FromId(IOpenCl lib, ImageId id,
            MemoryHandle handle = default, IDisposable disposable = null)
        {
            return FromId(id) as Image1D<T> ?? new Image1D<T>(id, lib, handle, disposable);
        }
    }
}
