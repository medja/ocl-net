using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image2DArray
    {
        #region Fill

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Fill(CommandQueue queue, double r, double g, double b, double a,
            params Event[] waitList)
        {
            return Fill(queue, r, g, b, a, 0, 0, 0, Width, Height, Length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Fill(CommandQueue queue, double r, double g, double b, double a,
            int index, int length = 1,  params Event[] waitList)
        {
            return Fill(queue, r, g, b, a, (ulong) index, 0, 0, Width, Height, (ulong) length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Fill(CommandQueue queue, double r, double g, double b, double a,
            ulong index, ulong length = 1, params Event[] waitList)
        {
            return Fill(queue, r, g, b, a, index, 0, 0, Width, Height, length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Fill(CommandQueue queue, double r, double g, double b, double a,
            int index, int x, int y, int width, int height, int length = 1, params Event[] waitList)
        {
            return Fill(queue, r, g, b, a,
                (ulong) index, (ulong) x, (ulong) y, (ulong) width, (ulong) height, (ulong) length, waitList);
        }

        public unsafe AutoDisposedEvent Fill(CommandQueue queue, double r, double g, double b, double a,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length = 1, params Event[] waitList)
        {
            var colorValue = ColorValue.FromRgba(r, g, b, a, Format.ImageChannelDataType);
            var eventId = FillInternal(queue, &colorValue, index, x, y, width, height, length, waitList);

            return AutoDisposedEvent.FromId(Library, eventId);
        }

        #endregion

        #region FillWithColor

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent FillWithColor(CommandQueue queue, Color color, params Event[] waitList)
        {
            return FillWithColor(queue, color, 0, 0, 0, Width, Height, Length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent FillWithColor(CommandQueue queue, Color color, int index, int length = 1,
            params Event[] waitList)
        {
            return FillWithColor(queue, color, (ulong) index, 0, 0, Width, Height, (ulong) length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent FillWithColor(CommandQueue queue, Color color, ulong index, ulong length = 1,
            params Event[] waitList)
        {
            return FillWithColor(queue, color, index, 0, 0, Width, Height, length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent FillWithColor(CommandQueue queue, Color color,
            int index, int x, int y, int width, int height, int length = 1, params Event[] waitList)
        {
            return FillWithColor(queue, color,
                (ulong) index, (ulong) x, (ulong) y, (ulong) width, (ulong) height, (ulong) length, waitList);
        }

        public unsafe AutoDisposedEvent FillWithColor(CommandQueue queue, Color color,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length = 1, params Event[] waitList)
        {
            var colorValue = ColorValue.FromColor(color, Format.ImageChannelDataType);
            var eventId = FillInternal(queue, &colorValue, index, x, y, width, height, length, waitList);

            return AutoDisposedEvent.FromId(Library, eventId);
        }

        #endregion

        #region FillInternal

        internal unsafe EventId FillInternal(CommandQueue queue, void* color,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length, params Event[] waitList)
        {
            var origin = stackalloc UIntPtr[3];

            origin[0] = (UIntPtr) x;
            origin[1] = (UIntPtr) y;
            origin[2] = (UIntPtr) index;

            var region = stackalloc UIntPtr[3];

            region[0] = (UIntPtr) width;
            region[1] = (UIntPtr) height;
            region[2] = (UIntPtr) length;

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
