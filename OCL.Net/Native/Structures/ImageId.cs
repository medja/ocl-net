using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace OCL.Net.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    public struct ImageId
    {
        public IntPtr Handle => _handle;
        private readonly IntPtr _handle;

        private ImageId(IntPtr handle)
        {
            _handle = handle;
        }

        public static explicit operator ImageId(MemoryId id)
        {
            return new ImageId(id.Handle);
        }
    }
}
