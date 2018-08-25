using System;
using System.Buffers;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2DArray<T> : Image2DArray
        where T : unmanaged
    {
        private Image2DArray(ImageId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib, handle, disposable)
        { }

        public static implicit operator Image2DArray<T>(ImageId id)
        {
            return FromId(id) as Image2DArray<T>;
        }

        internal static Image2DArray<T> FromId(IOpenCl lib, ImageId id,
            MemoryHandle handle = default, IDisposable disposable = null)
        {
            return FromId(id) as Image2DArray<T> ?? new Image2DArray<T>(id, lib, handle, disposable);
        }
    }
}
