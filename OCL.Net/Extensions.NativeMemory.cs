using System.Collections.Generic;

namespace OCL.Net
{
    public static partial class Extensions
    {
        public static NativeMemory<T> ToNativeMemory<T>(this T[] array) where T : unmanaged
        {
            return new NativeMemory<T>(array);
        }

        public static NativeMemory<T> ToNativeMemory<T>(this IList<T> list) where T : unmanaged
        {
            return new NativeMemory<T>(list);
        }

        public static NativeMemory<T> ToNativeMemory<T>(this ICollection<T> collection) where T : unmanaged
        {
            return new NativeMemory<T>(collection);
        }

        public static NativeMemory<T> ToNativeMemory<T>(this IEnumerable<T> enumerable) where T : unmanaged
        {
            return new NativeMemory<T>(enumerable);
        }
    }
}
