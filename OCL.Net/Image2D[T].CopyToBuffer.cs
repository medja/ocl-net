using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2D<T>
    {
        #region CopyToBuffer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, 0, 0, 0, Width, Height, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            int sourceX, int sourceY, int destinationOffset, int width, int height, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer,
                (ulong) sourceX, (ulong) sourceY, (ulong) destinationOffset, (ulong) width, (ulong) height, waitList);
        }

        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong sourceX, ulong sourceY, ulong destinationOffset, ulong width, ulong height, params Event[] waitList)
        {
            var eventId = CopyToBufferInternal(queue, buffer,
                sourceX, sourceY, destinationOffset, width, height, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, buffer);
        }

        #endregion

        #region CopyToBufferInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe EventId CopyToBufferInternal(CommandQueue queue, Buffer<T> buffer,
            ulong sourceX, ulong sourceY, ulong destinationOffset, ulong width, ulong height, params Event[] waitList)
        {
            return base.CopyToBufferInternal(queue, buffer,
                sourceX, sourceY, 0, destinationOffset * (ulong) sizeof(T), width, height, 1, waitList);
        }

        #endregion
    }
}
