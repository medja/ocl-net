using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace OCL.Net.Internal
{
    [StructLayout(LayoutKind.Explicit)]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public partial struct ColorValue
    {
        [FieldOffset(0)] public IntColor Int;
        [FieldOffset(0)] public UIntColor UInt;
        [FieldOffset(0)] public FloatColor Float;

        [StructLayout(LayoutKind.Sequential)]
        public struct IntColor
        {
            public int Red;
            public int Green;
            public int Blue;
            public int Alpha;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct UIntColor
        {
            public uint Red;
            public uint Green;
            public uint Blue;
            public uint Alpha;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FloatColor
        {
            public float Red;
            public float Green;
            public float Blue;
            public float Alpha;
        }
    }
}
