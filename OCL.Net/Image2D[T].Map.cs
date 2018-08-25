using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2D<T>
    {
        #region Map

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory> Map(CommandQueue queue,
            MapFlags flags = MapFlags.MapRead | MapFlags.MapWrite, params Event[] waitList)
        {
            return Map(queue, 0, 0, Width, Height, flags, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory> Map(CommandQueue queue, int x, int y, int width, int height,
            MapFlags flags = MapFlags.MapRead | MapFlags.MapWrite, params Event[] waitList)
        {
            return Map(queue, (ulong) x, (ulong) y, (ulong) width, (ulong) height, flags, waitList);
        }

        [SuppressMessage("ReSharper", "TooWideLocalVariableScope")]
        public unsafe AutoDisposedEvent<Memory> Map(CommandQueue queue, ulong x, ulong y, ulong width, ulong height,
            MapFlags flags = MapFlags.MapRead | MapFlags.MapWrite, params Event[] waitList)
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

            void* ptr;
            UIntPtr rowPitch;
            UIntPtr slicePitch;
            EventId eventId;
            ErrorCode errorCode;

            fixed (EventId* idsPtr = eventIds)
            {
                ptr = Library.clEnqueueMapImageUnsafe(queue, Id, false,
                    flags, origin, region, &rowPitch, &slicePitch, eventCount, idsPtr, &eventId, &errorCode);
            }

            errorCode.HandleError();

            var memory = new Memory((T*) ptr, (long) width, (long) height,
                (byte) Format.ChannelCount, (long) rowPitch, Id, queue, Library);

            return AutoDisposedEvent.FromId(Library, eventId, memory);
        }

        #endregion

        #region Unmap

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Unmap(CommandQueue queue, Memory memory, params Event[] waitList)
        {
            return memory.Unmap(queue, waitList);
        }

        #endregion
    }
}
