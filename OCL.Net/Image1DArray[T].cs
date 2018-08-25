using System;
using System.Buffers;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1DArray<T> : Image1DArray
        where T : unmanaged
    {
        private Image1DArray(ImageId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib, handle, disposable)
        { }

        public static implicit operator Image1DArray<T>(ImageId id)
        {
            return FromId(id) as Image1DArray<T>;
        }

        internal static Image1DArray<T> FromId(IOpenCl lib, ImageId id,
            MemoryHandle handle = default, IDisposable disposable = null)
        {
            return FromId(id) as Image1DArray<T> ?? new Image1DArray<T>(id, lib, handle, disposable);
        }
    }
}
