using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image1D : Image
    {
        private protected Image1D(ImageId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib, handle, disposable)
        { }

        public static implicit operator Image1D(ImageId id)
        {
            return FromId(id) as Image1D;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Image1D<T> FromId<T>(IOpenCl lib, ImageId id,
            MemoryHandle handle = default, IDisposable disposable = null)
            where T : unmanaged
        {
            return Image1D<T>.FromId(lib, id, handle, disposable);
        }
    }
}
