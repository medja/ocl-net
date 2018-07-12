using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace OCL.Net.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    public struct BufferId : IHandle
    {
        public IntPtr Handle => _handle;
        private readonly IntPtr _handle;

        private BufferId(IntPtr handle)
        {
            _handle = handle;
        }

        public static explicit operator BufferId(MemoryId id)
        {
            return new BufferId(id.Handle);
        }
    }
}
