using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OCL.Net
{
    public unsafe partial class NativeMemory<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MemoryInfo CreateFromLength(long length)
        {
            if (length == 0)
                return new MemoryInfo(null, length);

            var size = length * sizeof(T);
            var ptr = (T*) Marshal.AllocHGlobal(new IntPtr(size)).ToPointer();

            return new MemoryInfo(ptr, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MemoryInfo CreateFromArray(T[] array)
        {
            var length = array?.Length ?? 0;
            var info = CreateFromLength(length);

            if (length == 0)
                return info;

            var size = length * sizeof(T);

            fixed (T* src = array)
                System.Buffer.MemoryCopy(src, info.Pointer, size, size);

            return info;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static MemoryInfo CreateFromList(IList<T> list)
        {
            if (list is T[] array)
                return CreateFromArray(array);

            var length = list?.Count ?? 0;
            var info = CreateFromLength(length);

            if (length == 0)
                return info;

            for (var i = 0; i < length; i++)
                info.Pointer[i] = list[i];

            return info;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static MemoryInfo CreateFromCollection(ICollection<T> collection)
        {
            switch (collection)
            {
                case T[] array:
                    return CreateFromArray(array);

                case IList<T> list:
                    return CreateFromList(list);
            }

            var length = collection?.Count ?? 0;
            var info = CreateFromLength(length);

            if (length == 0)
                return info;

            using (var enumerator = collection.GetEnumerator())
                for (var i = 0; i < length && enumerator.MoveNext(); i++)
                    info.Pointer[i] = enumerator.Current;

            return info;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MemoryInfo CreateFromEnumerable(IEnumerable<T> enumerable)
        {
            switch (enumerable)
            {
                case T[] array:
                    return CreateFromArray(array);

                case IList<T> list:
                    return CreateFromList(list);

                case ICollection<T> collection:
                    return CreateFromCollection(collection);
            }

            var buffer = Internal.Buffer<T>.Create();

            foreach (var value in enumerable)
                buffer.Add(value);

            var info = CreateFromLength(buffer.Count);

            if (buffer.Count > 0)
                buffer.CopyTo(info.Pointer);

            return info;
        }
    }
}
