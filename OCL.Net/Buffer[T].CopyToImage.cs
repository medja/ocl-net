using System;
using System.Linq;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region CopyToImageInternal

        private unsafe EventId CopyToImageInternal(CommandQueue queue, ImageId image,
            ulong sourceOffset, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, ulong depth, params Event[] waitList)
        {
            var destinationOrigin = stackalloc UIntPtr[3];

            destinationOrigin[0] = (UIntPtr) destinationX;
            destinationOrigin[1] = (UIntPtr) destinationY;
            destinationOrigin[2] = (UIntPtr) destinationZ;

            var region = stackalloc UIntPtr[3];

            region[0] = (UIntPtr) width;
            region[1] = (UIntPtr) height;
            region[2] = (UIntPtr) depth;

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* idsPtr = eventIds)
            {
                Library.clEnqueueCopyBufferToImageUnsafe(queue, Id, image,
                    (UIntPtr) (sourceOffset * (ulong) sizeof(T)), destinationOrigin,
                    region, eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }

        #endregion
    }
}
