using System;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1DArray<T>
    {
        #region Write

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write(CommandQueue queue, Memory<T> memory, params Event[] waitList)
        {
            return Write(queue, memory, 0, 0, Width, Length, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write(CommandQueue queue, Memory<T> memory, int index, int length = 1,
            params Event[] waitList)
        {
            return Write(queue, memory, (ulong) index, 0, Width, (ulong) length, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write(CommandQueue queue, Memory<T> memory, ulong index, ulong length = 1,
            params Event[] waitList)
        {
            return Write(queue, memory, index, 0, Width, length, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write(CommandQueue queue, Memory<T> memory,
            int index, int x, int width, int length = 1, int rowPitch = 0, params Event[] waitList)
        {
            return Write(queue, memory,
                (ulong) index, (ulong) x, (ulong) width, (ulong) length, (ulong) rowPitch, waitList);
        }

        public unsafe AutoDisposedEvent<Memory<T>> Write(CommandQueue queue, Memory<T> memory,
            ulong index, ulong x, ulong width, ulong length = 1, ulong rowPitch = 0, params Event[] waitList)
        {
            if (rowPitch == 0)
                rowPitch = width * (ulong) Format.ElementSize;

            if (rowPitch * length > (ulong) memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot read over memory capacity");

            var handle = memory.Pin();

            var eventId = WriteInto(queue, (T*) handle.Pointer, index, x, width, length, rowPitch, waitList);
            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region WriteInto

        internal unsafe EventId WriteInto(CommandQueue queue, T* ptr,
            ulong index, ulong x, ulong width, ulong length, ulong rowPitch, params Event[] waitList)
        {
            var origin = stackalloc UIntPtr[3];

            origin[0] = (UIntPtr) x;
            origin[1] = (UIntPtr) index;
            origin[2] = UIntPtr.Zero;

            var region = stackalloc UIntPtr[3];

            region[0] = (UIntPtr) width;
            region[1] = (UIntPtr) length;
            region[2] = (UIntPtr) 1;

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* idsPtr = eventIds)
            {
                Library.clEnqueueWriteImageUnsafe(queue, Id, false, origin, region,
                    (UIntPtr) rowPitch, (UIntPtr) rowPitch, ptr, eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }

        #endregion
    }
}
