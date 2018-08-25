using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image3D<T>
    {
        #region CopyToBuffer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer, 0, 0, 0, 0, Width, Height, Depth, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            int sourceX, int sourceY, int sourceZ, int destinationOffset,
            int width, int height, int depth, params Event[] waitList)
        {
            return CopyToBuffer(queue, buffer,
                (ulong) sourceX, (ulong) sourceY, (ulong) sourceZ, (ulong) destinationOffset,
                (ulong) width, (ulong) height, (ulong) depth, waitList);
        }

        public AutoDisposedEvent<Buffer<T>> CopyToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationOffset,
            ulong width, ulong height, ulong depth, params Event[] waitList)
        {
            var eventId = CopyToBufferInternal(queue, buffer,
                sourceX, sourceY, sourceZ, destinationOffset, width, height, depth, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, buffer);
        }

        #endregion

        #region CopyToBufferInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe EventId CopyToBufferInternal(CommandQueue queue, Buffer<T> buffer,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationOffset,
            ulong width, ulong height, ulong depth, params Event[] waitList)
        {
            return base.CopyToBufferInternal(queue, buffer,
                sourceX, sourceY, sourceZ, destinationOffset * (ulong) sizeof(T), width, height, depth, waitList);
        }

        #endregion
    }
}
