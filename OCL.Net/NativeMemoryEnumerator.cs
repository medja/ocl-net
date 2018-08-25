using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace OCL.Net
{
    public unsafe struct NativeMemoryEnumerator<T> : IEnumerator<T>
        where T : unmanaged
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

        public NativeMemoryEnumerator(T* ptr, long length)
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
