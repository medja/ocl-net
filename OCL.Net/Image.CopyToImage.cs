using System;
using System.Linq;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image
    {
        protected unsafe EventId CopyToImageInternal(CommandQueue queue, ImageId image,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, ulong depth, params Event[] waitList)
        {
            var sourceOrigin = stackalloc UIntPtr[3];

            sourceOrigin[0] = (UIntPtr) sourceX;
            sourceOrigin[1] = (UIntPtr) sourceY;
            sourceOrigin[2] = (UIntPtr) sourceZ;

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
                Library.clEnqueueCopyImageUnsafe(
                    queue, Id, image, sourceOrigin, destinationOrigin, region,
                    eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }
    }
}
