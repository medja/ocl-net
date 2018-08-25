using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OCL.Net.Internal;

namespace OCL.Net
{
    public unsafe partial class NativeMemory<T> : NativeMemoryManager<T>
        where T : unmanaged
    {
        public NativeMemory(long length) : this(CreateFromLength(length))
        { }

        public NativeMemory(T[] array) : this(CreateFromArray(array))
        { }

        public NativeMemory(IList<T> list) : this(CreateFromList(list))
        { }

        public NativeMemory(ICollection<T> collection) : this(CreateFromCollection(collection))
        { }

        public NativeMemory(IEnumerable<T> enumerable) : this(CreateFromEnumerable(enumerable))
        { }

        private NativeMemory(MemoryInfo info) : base(info.Pointer, info.Length)
        {
            GC.AddMemoryPressure(Size);
        }

        protected override void OnDispose()
        {
            Marshal.FreeHGlobal((IntPtr) Pointer);
            GC.RemoveMemoryPressure(Size);
        }

        private readonly struct MemoryInfo
        {
            public readonly T* Pointer;
            public readonly long Length;

            public MemoryInfo(T* ptr, long length)
            {
                Pointer = ptr;
                Length = length;
            }
        }
    }
}
