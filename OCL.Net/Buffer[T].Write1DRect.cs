using System;
using System.Runtime.CompilerServices;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Write1DRect

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write1DRect(CommandQueue queue, Memory<T> memory,
            int bufferX, int width, params Event[] waitList)
        {
            return Write1DRect(queue, memory, (ulong) bufferX, 0, (ulong) width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write1DRect(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong width, params Event[] waitList)
        {
            return Write1DRect(queue, memory, bufferX, 0, width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write1DRect(CommandQueue queue, Memory<T> memory,
            int bufferX, int hostX, int width, params Event[] waitList)
        {
            return Write1DRect(queue, memory, (ulong) bufferX, (ulong) hostX, (ulong) width,waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe AutoDisposedEvent<Memory<T>> Write1DRect(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong hostX, ulong width, params Event[] waitList)
        {
            if (width > (ulong) memory.Length)
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot read over memory capacity");

            var rowPitch = width * (ulong) sizeof(T);
            var handle = memory.Pin();

            var eventId = Write3DRectInto(queue, (T*) handle.Pointer, bufferX, 0, 0, hostX, 0, 0, width, 1, 1,
                rowPitch, rowPitch, rowPitch, rowPitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion
    }
}
