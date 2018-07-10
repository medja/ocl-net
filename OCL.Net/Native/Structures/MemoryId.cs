using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace OCL.Net.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    public struct MemoryId
    {
        public IntPtr Handle => _handle;
        private readonly IntPtr _handle;

        private MemoryId(IntPtr handle)
        {
            _handle = handle;
        }

        public static implicit operator MemoryId(BufferId id)
        {
            return new MemoryId(id.Handle);
        }

        public static implicit operator MemoryId(ImageId id)
        {
            return new MemoryId(id.Handle);
        }
    }
}
