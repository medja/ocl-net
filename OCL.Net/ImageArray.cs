using System;
using System.Buffers;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class ImageArray : Image
    {
        private protected ImageArray(ImageId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib, handle, disposable)
        { }
    }
}
