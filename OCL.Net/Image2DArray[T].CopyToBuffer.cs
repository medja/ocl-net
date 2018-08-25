using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2DArray<T>
    {
        #region CopyToBuffer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, 0, 0, 0, 0, Width, Height, Length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            int sourceIndex, int length = 1, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, (ulong) sourceIndex, 0, 0, 0, Width, Height, (ulong) length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong sourceIndex, ulong length = 1, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, sourceIndex, 0, 0, 0, Width, Height, length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            int sourceIndex, int sourceX, int sourceY, int destinationOffset,
            int width, int height, int length = 1, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer,
                (ulong) sourceIndex, (ulong) sourceX, (ulong) sourceY, (ulong) destinationOffset,
                (ulong) width, (ulong) height, (ulong) length, waitList);
        }

        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationOffset,
            ulong width, ulong height, ulong length = 1, params Event[] waitList)
        {
            var eventId = CopyToBufferInternal(queue, buffer,
                sourceIndex, sourceX, sourceY, destinationOffset, width, height, length, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, buffer);
        }

        #endregion

        #region CopyToBufferInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe EventId CopyToBufferInternal(CommandQueue queue, Buffer<T> buffer,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationOffset,
            ulong width, ulong height, ulong length, params Event[] waitList)
        {
            return base.CopyToBufferInternal(queue, buffer,
                sourceX, sourceY, sourceIndex, destinationOffset * (ulong) sizeof(T), width, height, length, waitList);
        }

        #endregion
    }
}
