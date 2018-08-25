using System;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2D<T>
    {
        #region Write

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write(CommandQueue queue, Memory<T> memory,
            params Event[] waitList)
        {
            return Write(queue, memory, 0, 0, Width, Height, 0, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write(CommandQueue queue, Memory<T> memory,
            int x, int y, int width, int height, int rowPitch = 0, int slicePitch = 0, params Event[] waitList)
        {
            return Write(queue, memory, (ulong) x, (ulong) y,
                (ulong) width, (ulong) height, (ulong) rowPitch, (ulong) slicePitch, waitList);
        }

        public unsafe AutoDisposedEvent<Memory<T>> Write(CommandQueue queue, Memory<T> memory,
            ulong x, ulong y, ulong width, ulong height, ulong rowPitch = 0, ulong slicePitch = 0,
            params Event[] waitList)
        {
            if (rowPitch == 0)
                rowPitch = width * (ulong) Format.ElementSize;

            if (slicePitch == 0)
                slicePitch = height * rowPitch;

            if (slicePitch > (ulong) memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot read over memory capacity");

            var handle = memory.Pin();

            var eventId = WriteInto(queue, (T*) handle.Pointer, x, y, width, height, rowPitch, slicePitch, waitList);
            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region WriteInto

        internal unsafe EventId WriteInto(CommandQueue queue, T* ptr,
            ulong x, ulong y, ulong width, ulong height, ulong rowPitch, ulong slicePitch, params Event[] waitList)
        {
            var origin = stackalloc UIntPtr[3];

            origin[0] = (UIntPtr) x;
            origin[1] = (UIntPtr) y;
            origin[2] = UIntPtr.Zero;

            var region = stackalloc UIntPtr[3];

            region[0] = (UIntPtr) width;
            region[1] = (UIntPtr) height;
            region[2] = (UIntPtr) 1;

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* idsPtr = eventIds)
            {
                Library.clEnqueueWriteImageUnsafe(queue, Id, false, origin, region,
                    (UIntPtr) rowPitch, (UIntPtr) slicePitch, ptr, eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }

        #endregion
    }
}
