using System;
using System.Linq;
using System.Text;
using OCL.Net.Native.Enums;

namespace OCL.Net.Internal
{
    public static class InfoLoader
    {
        public unsafe delegate ErrorCode LoadInfo<in TInfo>(TInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
            where TInfo : Enum;

        public static string LoadString<TInfo>(TInfo info, LoadInfo<TInfo> loadInfo)
            where TInfo : Enum
        {
            return Encoding.UTF8.GetString(LoadArray<byte, TInfo>(info, loadInfo)).TrimEnd('\0');
        }

        public static T LoadValue<T, TInfo>(TInfo info, LoadInfo<TInfo> loadInfo)
            where T : unmanaged
            where TInfo : Enum
        {
            return LoadArray<T, TInfo>(info, loadInfo).FirstOrDefault();
        }

        public static unsafe T[] LoadArray<T, TInfo>(TInfo info, LoadInfo<TInfo> loadInfo)
            where T : unmanaged
            where TInfo : Enum
        {
            UIntPtr size;

            loadInfo(info, UIntPtr.Zero, null, &size).HandleError();

            var buffer = new T[(long) size / sizeof(T)];

            fixed (T* ptr = buffer)
            {
                loadInfo(info, size, (byte*) ptr, null).HandleError();
            }

            return buffer;
        }
    }
}
