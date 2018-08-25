using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1DArray<T>
    {
        #region CopyToBuffer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, 0, 0, 0, Width, Length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            int sourceIndex, int length = 1, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, (ulong) sourceIndex, 0, 0, Width, (ulong) length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong sourceIndex, ulong length = 1, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, sourceIndex, 0, 0, Width, length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            int sourceIndex, int sourceX, int destinationOffset, int width, int length = 1, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer,
                (ulong) sourceIndex, (ulong) sourceX, (ulong) destinationOffset,
                (ulong) width, (ulong) length, waitList);
        }

        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong sourceIndex, ulong sourceX, ulong destinationOffset,
            ulong width, ulong length = 1, params Event[] waitList)
        {
            var eventId = CopyToBufferInternal(queue, buffer,
                sourceIndex, sourceX, destinationOffset, width, length, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, buffer);
        }

        #endregion

        #region CopyToBufferInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe EventId CopyToBufferInternal(CommandQueue queue, Buffer<T> buffer,
            ulong sourceIndex, ulong sourceX, ulong destinationOffset,
            ulong width, ulong length, params Event[] waitList)
        {
            return base.CopyToBufferInternal(queue, buffer,
                sourceX, sourceIndex, 0, destinationOffset * (ulong) sizeof(T), width, length, 1, waitList);
        }

        #endregion
    }
}
