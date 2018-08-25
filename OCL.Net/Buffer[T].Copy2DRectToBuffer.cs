using System.Runtime.CompilerServices;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Copy2DRectToBuffer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy2DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            int x, int y, int width, int height, int rowPitch, params Event[] waitList)
        {
            return Copy2DRectToBuffer(queue, buffer, (ulong) x, (ulong) y, (ulong) x, (ulong) y,
                (ulong) width, (ulong) height, (ulong) rowPitch, (ulong) rowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy2DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong x, ulong y, ulong width, ulong height, ulong rowPitch, params Event[] waitList)
        {
            return Copy2DRectToBuffer(queue, buffer, x, y, x, y, width, height, rowPitch, rowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy2DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            int x, int y, int width, int height, int sourceRowPitch, int destinationRowPitch,
            params Event[] waitList)
        {
            return Copy2DRectToBuffer(queue, buffer, (ulong) x, (ulong) y, (ulong) x, (ulong) y,
                (ulong) width, (ulong) height, (ulong) sourceRowPitch,  (ulong) destinationRowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy2DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong x, ulong y, ulong width, ulong height, ulong sourceRowPitch, ulong destinationRowPitch,
            params Event[] waitList)
        {
            return Copy2DRectToBuffer(queue, buffer, x, y, x, y, width, height,
                sourceRowPitch, destinationRowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy2DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            int sourceX, int sourceY, int destinationX, int destinationY,
            int width, int height, int sourceRowPitch, int destinationRowPitch, params Event[] waitList)
        {
            return Copy2DRectToBuffer(queue, buffer,
                (ulong) sourceX, (ulong) sourceY, (ulong) destinationX, (ulong) destinationY,
                (ulong) width, (ulong) height, (ulong) sourceRowPitch, (ulong) destinationRowPitch, waitList);
        }

        public AutoDisposedEvent<Buffer<T>> Copy2DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong sourceX, ulong sourceY, ulong destinationX, ulong destinationY,
            ulong width, ulong height, ulong sourceRowPitch, ulong destinationRowPitch, params Event[] waitList)
        {
            var eventId = Copy3DRectToBufferInternal(queue, buffer,
                sourceX, sourceY, 0, destinationX, destinationY, 0, width, height, 1,
                sourceRowPitch, sourceRowPitch, destinationRowPitch, destinationRowPitch, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, buffer);
        }

        #endregion
    }
}
