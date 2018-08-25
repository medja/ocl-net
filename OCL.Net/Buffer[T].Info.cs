using System.Runtime.CompilerServices;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        public int Length => (int) LongLength;
        public ulong LongLength => GetLength();
        public new Buffer<T> AssociatedMemory => FromId(Library, LoadValue(MemInfo.MemAssociatedMemObject, ref AssociatedMemoryId));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe ulong GetLength()
        {
            return Size / (ulong) sizeof(T);
        }
    }
}
