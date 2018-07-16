using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace OCL.Net
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public partial class NativeMemory<T> : IEnumerable<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Enumerator GetEnumerator()
        {
            return new Enumerator(_ptr, _length);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public unsafe struct Enumerator : IEnumerator<T>
        {
            public T Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _current;
            }

            object IEnumerator.Current => _current;

            private long _index;
            private T _current;

            private readonly T* _ptr;
            private readonly long _length;

            public Enumerator(T* ptr, long length)
            {
                _index = 0;
                _current = default;
                _ptr = ptr;
                _length = length;
            }

            public bool MoveNext()
            {
                if (_index < _length)
                {
                    _current = _ptr[_index];
                    _index++;
                    return true;
                }

                _current = default;
                return false;
            }

            public void Reset()
            {
                _index = 0;
                _current = default;
            }

            public void Dispose()
            { }
        }
    }
}
