using System;
using System.Buffers;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T> : Buffer
        where T : unmanaged
    {
        private Buffer(BufferId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib, handle, disposable)
        { }

        public static implicit operator Buffer<T>(BufferId id)
        {
            return FromId(id) as Buffer<T>;
        }

        internal static Buffer<T> FromId(IOpenCl lib, BufferId id,
            MemoryHandle handle = default, IDisposable disposable = null)
        {
            return FromId(id) as Buffer<T> ?? new Buffer<T>(id, lib, handle, disposable);
        }
    }
}
