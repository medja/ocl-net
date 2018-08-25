using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image2D
    {
        #region Fill

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Fill(CommandQueue queue, double r, double g, double b, double a,
            params Event[] waitList)
        {
            return Fill(queue, r, g, b, a, 0, 0, Width, Height, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Fill(CommandQueue queue, double r, double g, double b, double a,
            int x, int y, int width, int height, params Event[] waitList)
        {
            return Fill(queue, r, g, b, a, (ulong) x, (ulong) y, (ulong) width, (ulong) height, waitList);
        }

        public unsafe AutoDisposedEvent Fill(CommandQueue queue, double r, double g, double b, double a,
            ulong x, ulong y, ulong width, ulong height, params Event[] waitList)
        {
            var colorValue = ColorValue.FromRgba(r, g, b, a, Format.ImageChannelDataType);
            var eventId = FillInternal(queue, &colorValue, x, y, width, height, waitList);

            return AutoDisposedEvent.FromId(Library, eventId);
        }

        #endregion

        #region FillWithColor

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent FillWithColor(CommandQueue queue, Color color, params Event[] waitList)
        {
            return FillWithColor(queue, color, 0, 0, Width, Height, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent FillWithColor(CommandQueue queue, Color color,
            int x, int y, int width, int height, params Event[] waitList)
        {
            return FillWithColor(queue, color, (ulong) x, (ulong) y, (ulong) width, (ulong) height, waitList);
        }

        public unsafe AutoDisposedEvent FillWithColor(CommandQueue queue, Color color,
            ulong x, ulong y, ulong width, ulong height, params Event[] waitList)
        {
            var colorValue = ColorValue.FromColor(color, Format.ImageChannelDataType);
            var eventId = FillInternal(queue, &colorValue, x, y, width, height, waitList);

            return AutoDisposedEvent.FromId(Library, eventId);
        }

        #endregion

        #region FillInternal

        internal unsafe EventId FillInternal(CommandQueue queue, void* color,
            ulong x, ulong y, ulong width, ulong height, params Event[] waitList)
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
                Library.clEnqueueFillImageUnsafe(
                    queue, Id, color, origin, region, eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }

        #endregion
    }
}
