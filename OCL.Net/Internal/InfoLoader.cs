using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using OCL.Net.Native.Enums;

namespace OCL.Net.Internal
{
    public static class InfoLoader
    {
        public delegate ErrorCode LoadInfo<in T>(T info, UIntPtr bufferSize, IntPtr buffer, out UIntPtr size)
            where T : struct;

        public static string LoadString<TInfo>(TInfo info, LoadInfo<TInfo> loadInfo)
            where TInfo : struct
        {
            return Encoding.UTF8.GetString(LoadArray<byte, TInfo>(info, loadInfo)).TrimEnd('\0');
        }

        public static T LoadValue<T, TInfo>(TInfo info, LoadInfo<TInfo> loadInfo)
            where T : struct
            where TInfo : struct
        {
            return LoadArray<T, TInfo>(info, loadInfo).FirstOrDefault();
        }

        public static unsafe T[] LoadArray<T, TInfo>(TInfo info, LoadInfo<TInfo> loadInfo)
            where T : struct
            where TInfo : struct
        {
            loadInfo(info, UIntPtr.Zero, IntPtr.Zero, out var size).HandleError();

            var buffer = new T[(int) size / Unsafe.SizeOf<T>()];
            var span = MemoryMarshal.AsBytes(new Span<T>(buffer));

            fixed (byte* ptr = &span.GetPinnableReference())
            {
                loadInfo(info, size, (IntPtr) ptr, out _).HandleError();
            }

            return buffer;
        }
    }
}
