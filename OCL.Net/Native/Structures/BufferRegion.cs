using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace OCL.Net.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    public struct BufferRegion
    {
        public UIntPtr Origin => _origin;
        public UIntPtr Size => _size;

        private readonly UIntPtr _origin;
        private readonly UIntPtr _size;

        public BufferRegion(UIntPtr origin, UIntPtr size)
        {
            _origin = origin;
            _size = size;
        }

        public static BufferRegion Create(int origin, int size)
        {
            return new BufferRegion((UIntPtr) origin, (UIntPtr) size);
        }

        public static BufferRegion Create(ulong origin, ulong size)
        {
            return new BufferRegion((UIntPtr) origin, (UIntPtr) size);
        }
    }
}
