using System;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Copy3DRectToBuffer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy3DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            int x, int y, int z, int width, int height, int depth,
            int rowPitch, int slicePitch, params Event[] waitList)
        {
            return Copy3DRectToBuffer(queue, buffer,
                (ulong) x, (ulong) y, (ulong) z, (ulong) x, (ulong) y, (ulong) z,
                (ulong) width, (ulong) height, (ulong) depth,
                (ulong) rowPitch, (ulong) slicePitch, (ulong) rowPitch, (ulong) slicePitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy3DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong x, ulong y, ulong z, ulong width, ulong height, ulong depth,
            ulong rowPitch, ulong slicePitch, params Event[] waitList)
        {
            return Copy3DRectToBuffer(queue, buffer, x, y, z, x, y, z, width, height, depth,
                rowPitch, slicePitch, rowPitch, slicePitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy3DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            int x, int y, int z, int width, int height, int depth,
            int sourceRowPitch, int sourceSlicePitch, int destinationRowPitch, int destinationSlicePitch,
            params Event[] waitList)
        {
            return Copy3DRectToBuffer(queue, buffer,
                (ulong) x, (ulong) y, (ulong) z, (ulong) x, (ulong) y, (ulong) z,
                (ulong) width, (ulong) height, (ulong) depth,
                (ulong) sourceRowPitch, (ulong) sourceSlicePitch,
                (ulong) destinationRowPitch, (ulong) destinationSlicePitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy3DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong x, ulong y, ulong z, ulong width, ulong height, ulong depth,
            ulong sourceRowPitch, ulong sourceSlicePitch, ulong destinationRowPitch, ulong destinationSlicePitch,
            params Event[] waitList)
        {
            return Copy3DRectToBuffer(queue, buffer, x, y, z, x, y, z, width, height, depth,
                sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Buffer<T>> Copy3DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            int sourceX, int sourceY, int sourceZ, int destinationX, int destinationY, int destinationZ,
            int width, int height, int depth,
            int sourceRowPitch, int sourceSlicePitch, int destinationRowPitch, int destinationSlicePitch,
            params Event[] waitList)
        {
            return Copy3DRectToBuffer(queue, buffer,
                (ulong) sourceX, (ulong) sourceY, (ulong) sourceZ,
                (ulong) destinationX, (ulong) destinationY, (ulong) destinationZ,
                (ulong) width, (ulong) height, (ulong) depth,
                (ulong) sourceRowPitch, (ulong) sourceSlicePitch,
                (ulong) destinationRowPitch, (ulong) destinationSlicePitch, waitList);
        }

        public AutoDisposedEvent<Buffer<T>> Copy3DRectToBuffer(CommandQueue queue, Buffer<T> buffer,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, ulong depth,
            ulong sourceRowPitch, ulong sourceSlicePitch, ulong destinationRowPitch, ulong destinationSlicePitch,
            params Event[] waitList)
        {
            var eventId = Copy3DRectToBufferInternal(queue, buffer,
                sourceX, sourceY, sourceZ, destinationX, destinationY, destinationZ, width, height, depth,
                sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, buffer);
        }

        #endregion

        #region Copy3DRectToBufferInternal

        internal unsafe EventId Copy3DRectToBufferInternal(CommandQueue queue, Buffer<T> buffer,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, ulong depth,
            ulong sourceRowPitch, ulong sourceSlicePitch, ulong destinationRowPitch, ulong destinationSlicePitch,
            params Event[] waitList)
        {
            var sourceOrigin = stackalloc UIntPtr[3];

            sourceOrigin[0] = (UIntPtr) (sourceX * (ulong) sizeof(T));
            sourceOrigin[1] = (UIntPtr) sourceY;
            sourceOrigin[2] = (UIntPtr) sourceZ;

            var destinationOrigin = stackalloc UIntPtr[3];

            destinationOrigin[0] = (UIntPtr) (destinationX * (ulong) sizeof(T));
            destinationOrigin[1] = (UIntPtr) destinationY;
            destinationOrigin[2] = (UIntPtr) destinationZ;

            var region = stackalloc UIntPtr[3];

            region[0] = (UIntPtr) (width * (ulong) sizeof(T));
            region[1] = (UIntPtr) height;
            region[2] = (UIntPtr) depth;

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* idsPtr = eventIds)
            {
                Library.clEnqueueCopyBufferRectUnsafe(queue, Id, buffer, sourceOrigin, destinationOrigin, region,
                    (UIntPtr) sourceRowPitch, (UIntPtr) sourceSlicePitch,
                    (UIntPtr) destinationRowPitch, (UIntPtr) destinationSlicePitch,
                    eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }

        #endregion
    }
}
