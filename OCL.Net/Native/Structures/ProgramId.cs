using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace OCL.Net.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    public struct ProgramId : IHandle
    {
        public IntPtr Handle => _handle;
        private readonly IntPtr _handle;
    }
}
