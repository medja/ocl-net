using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Buffer : MemoryObject
    {
        public new BufferId Id { get; }

        private protected Buffer(BufferId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib, handle, disposable)
        {
            Id = id;
        }

        public static implicit operator Buffer(BufferId id)
        {
            return FromId(id) as Buffer;
        }

        public static implicit operator BufferId(Buffer buffer)
        {
            return buffer.Id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Buffer<T> FromId<T>(IOpenCl lib, BufferId id,
            MemoryHandle handle = default, IDisposable disposable = null)
            where T : unmanaged
        {
            return Buffer<T>.FromId(lib, id, handle, disposable);
        }
    }
}
