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
        private void CreateFromLength(long length)
        {
            _length = length;

            if (length == 0)
            {
                _ptr = null;
                return;
            }

            var size = length * sizeof(T);

            _ptr = (T*) Marshal.AllocHGlobal(new IntPtr(size)).ToPointer();
            GC.AddMemoryPressure(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreateFromArray(T[] array)
        {
            var length = array?.Length ?? 0;
            CreateFromLength(length);

            if (length == 0)
                return;

            var size = length * sizeof(T);

            fixed (T* src = array)
                Buffer.MemoryCopy(src, _ptr, size, size);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void CreateFromList(IList<T> list)
        {
            var length = list?.Count ?? 0;
            CreateFromLength(length);

            if (length == 0)
                return;

            for (var i = 0; i < _length; i++)
                _ptr[i] = list[i];
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void CreateFromCollection(ICollection<T> collection)
        {
            var length = collection?.Count ?? 0;
            CreateFromLength(length);

            if (length == 0)
                return;

            using (var enumerator = collection.GetEnumerator())
                for (var i = 0; i < _length && enumerator.MoveNext(); i++)
                    _ptr[i] = enumerator.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreateFromEnumerable(IEnumerable<T> enumerable)
        {
            var buffer = Internal.Buffer<T>.Create();

            foreach (var value in enumerable)
                buffer.Add(value);

            CreateFromLength(buffer.Count);

            if (buffer.Count > 0)
                buffer.CopyTo(_ptr);
        }
    }
}
