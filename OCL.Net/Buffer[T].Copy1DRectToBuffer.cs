using System.Runtime.CompilerServices;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Copy1DRectTo

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy1DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            int x, int width, params Event[] waitList)
        {
            return Copy1DRectToBuffer(queue, buffer, (ulong) x, (ulong) x, (ulong) width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy1DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong x, ulong width, params Event[] waitList)
        {
            return Copy1DRectToBuffer(queue, buffer, x, x, width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy1DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            int sourceX, int destinationX, int width, params Event[] waitList)
        {
            return Copy1DRectToBuffer(queue, buffer, (ulong) sourceX, (ulong) destinationX, (ulong) width, waitList);
        }

        public unsafe AutoDisposedEvent<Buffer<T>> Copy1DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong sourceX, ulong destinationX, ulong width, params Event[] waitList)
        {
            var rowPitch = width * (ulong) sizeof(T);

            var eventId = Copy3DRectToBufferInternal(queue, buffer, sourceX, 0, 0, destinationX, 0, 0, width, 1, 1,
                rowPitch, rowPitch, rowPitch, rowPitch, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, buffer);
        }

        #endregion
    }
}
