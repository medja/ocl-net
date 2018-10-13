using System;
using System.Buffers;

namespace OCL.Net.Internal
{
    public struct PinnedBuffer : IDisposable
    {
        public readonly byte[] Buffer;
        public MemoryHandle Handle;

        public PinnedBuffer(ulong length)
        {
            Buffer = new byte[length];
            Handle = new Memory<byte>(Buffer).Pin();
        }

        public PinnedBuffer(byte[] buffer)
        {
            Buffer = buffer;
            Handle = new Memory<byte>(Buffer).Pin();
        }

        public void Dispose()
        {
            Handle.Dispose();
        }
    }
}
