using System;
using System.Runtime.CompilerServices;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Write2DRect

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write2DRect(CommandQueue queue, Memory<T> memory,
            int bufferX, int bufferY, int width, int height, int bufferRowPitch, params Event[] waitList)
        {
            return Write2DRect(queue, memory, (ulong) bufferX, (ulong) bufferY,
                (ulong) width, (ulong) height, (ulong) bufferRowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe AutoDisposedEvent<Memory<T>> Write2DRect(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong bufferY, ulong width, ulong height, ulong bufferRowPitch, params Event[] waitList)
        {
            var hostRowPitch = width * (ulong) sizeof(T);

            return Write2DRect(queue, memory, bufferX, bufferY, 0, 0, width, height, bufferRowPitch, hostRowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write2DRect(CommandQueue queue, Memory<T> memory,
            int bufferX, int bufferY, int hostX, int hostY, int width, int height,
            int bufferRowPitch, int hostRowPitch, params Event[] waitList)
        {
            return Write2DRect(queue, memory, (ulong) bufferX, (ulong) bufferY, (ulong) hostX, (ulong) hostY,
                (ulong) width, (ulong) height, (ulong) bufferRowPitch, (ulong) hostRowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe AutoDisposedEvent<Memory<T>> Write2DRect(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong bufferY, ulong hostX, ulong hostY, ulong width, ulong height,
            ulong bufferRowPitch, ulong hostRowPitch, params Event[] waitList)
        {
            if (hostRowPitch * height > (ulong) memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot read over memory capacity");

            var handle = memory.Pin();

            var eventId = Write3DRectInto(queue, (T*) handle.Pointer,
                bufferX, bufferY, 0, hostX, hostY, 0, width, height, 1,
                bufferRowPitch, bufferRowPitch, hostRowPitch, hostRowPitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion
    }
}
