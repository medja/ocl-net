using System;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Write3DRect

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write3DRect(CommandQueue queue, Memory<T> memory,
            int bufferX, int bufferY, int bufferZ, int width, int height, int depth,
            int bufferRowPitch, int bufferSlicePitch, params Event[] waitList)
        {
            return Write3DRect(queue, memory,
                (ulong) bufferX, (ulong) bufferY, (ulong) bufferZ, (ulong) width, (ulong) height, (ulong) depth,
                (ulong) bufferRowPitch, (ulong) bufferSlicePitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe AutoDisposedEvent<Memory<T>> Write3DRect(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, params Event[] waitList)
        {
            var hostRowPitch = width * (ulong) sizeof(T);
            var hostSlicePitch = height * hostRowPitch;

            return Write3DRect(queue, memory, bufferX, bufferY, bufferZ, 0, 0, 0, width, height, depth,
                bufferRowPitch, bufferSlicePitch, hostRowPitch, hostSlicePitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Write3DRect(CommandQueue queue, Memory<T> memory,
            int bufferX, int bufferY, int bufferZ, int hostX, int hostY, int hostZ,
            int width, int height, int depth,
            int bufferRowPitch, int bufferSlicePitch, int hostRowPitch, int hostSlicePitch, params Event[] waitList)
        {
            return Write3DRect(queue, memory,
                (ulong) bufferX, (ulong) bufferY, (ulong) bufferZ, (ulong) hostX, (ulong) hostY, (ulong) hostZ,
                (ulong) width, (ulong) height, (ulong) depth,
                (ulong) bufferRowPitch, (ulong) bufferSlicePitch, (ulong) hostRowPitch, (ulong) hostSlicePitch,
                waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe AutoDisposedEvent<Memory<T>> Write3DRect(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong hostX, ulong hostY, ulong hostZ,
            ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, ulong hostRowPitch, ulong hostSlicePitch,
            params Event[] waitList)
        {
            if (hostSlicePitch * depth > (ulong) memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot read over memory capacity");

            var handle = memory.Pin();

            var eventId = Write3DRectInto(queue, (T*) handle.Pointer,
                bufferX, bufferY, bufferZ, hostX, hostY, hostZ, width, height, depth,
                bufferRowPitch, bufferSlicePitch, hostRowPitch, hostSlicePitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Write3DRectInto

        internal unsafe EventId Write3DRectInto(CommandQueue queue, T* ptr,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong hostX, ulong hostY, ulong hostZ,
            ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, ulong hostRowPitch, ulong hostSlicePitch,
            params Event[] waitList)
        {
            var bufferOrigin = stackalloc UIntPtr[3];

            bufferOrigin[0] = (UIntPtr) (bufferX * (ulong) sizeof(T));
            bufferOrigin[1] = (UIntPtr) bufferY;
            bufferOrigin[2] = (UIntPtr) bufferZ;

            var hostOrigin = stackalloc UIntPtr[3];

            hostOrigin[0] = (UIntPtr) (hostX * (ulong) sizeof(T));
            hostOrigin[1] = (UIntPtr) hostY;
            hostOrigin[2] = (UIntPtr) hostZ;

            var region = stackalloc UIntPtr[3];

            region[0] = (UIntPtr) (width * (ulong) sizeof(T));
            region[1] = (UIntPtr) height;
            region[2] = (UIntPtr) depth;

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* idsPtr = eventIds)
            {
                Library.clEnqueueWriteBufferRectUnsafe(queue, Id, false,
                    bufferOrigin, hostOrigin, region,
                    (UIntPtr) bufferRowPitch, (UIntPtr) bufferSlicePitch,
                    (UIntPtr) hostRowPitch, (UIntPtr) hostSlicePitch,
                    ptr, eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }

        #endregion
    }
}
