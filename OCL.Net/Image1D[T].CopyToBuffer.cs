using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1D<T>
    {
        #region CopyToBuffer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, 0, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            int sourceX, int destinationOffset, int width, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, (ulong) sourceX, (ulong) destinationOffset, (ulong) width, waitList);
        }

        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong sourceX, ulong destinationOffset, ulong width, params Event[] waitList)
        {
            var eventId = CopyToBufferInternal(queue, buffer, sourceX, destinationOffset, width, waitList);
            return AutoDisposedEvent.FromId(Library, eventId, buffer);
        }

        #endregion

        #region CopyToBufferInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe EventId CopyToBufferInternal(CommandQueue queue, Buffer<T> buffer,
            ulong sourceX, ulong destinationOffset, ulong width, params Event[] waitList)
        {
            return base.CopyToBufferInternal(queue, buffer,
                sourceX, 0, 0, destinationOffset * (ulong) sizeof(T), width, 1, 1, waitList);
        }

        #endregion
    }
}
