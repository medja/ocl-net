using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image2D : Image
    {
        private protected Image2D(ImageId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib, handle, disposable)
        { }

        public static implicit operator Image2D(ImageId id)
        {
            return FromId(id) as Image2D;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Image2D<T> FromId<T>(IOpenCl lib, ImageId id,
            MemoryHandle handle = default, IDisposable disposable = null)
            where T : unmanaged
        {
            return Image2D<T>.FromId(lib, id, handle, disposable);
        }
    }
}
