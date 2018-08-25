using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1D<T>
    {
        #region CopyToImage2D

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2D<T>> CopyToImage2D(CommandQueue queue, Image2D<T> image,
            int destinationY, params Event[] waitList)
        {
            return CopyToImage2D(queue, image, 0, 0, (ulong) destinationY, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2D<T>> CopyToImage2D(CommandQueue queue, Image2D<T> image,
            ulong destinationY, params Event[] waitList)
        {
            return CopyToImage2D(queue, image, 0, 0, destinationY, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2D<T>> CopyToImage2D(CommandQueue queue, Image2D<T> image,
            int sourceX, int destinationX, int destinationY, int width, params Event[] waitList)
        {
            return CopyToImage2D(queue, image,
                (ulong) sourceX, (ulong) destinationX, (ulong) destinationY, (ulong) width, waitList);
        }

        public AutoDisposedEvent<Image2D<T>> CopyToImage2D(CommandQueue queue, Image2D<T> image,
            ulong sourceX, ulong destinationX, ulong destinationY, ulong width, params Event[] waitList)
        {
            var eventId = CopyToImage2DInternal(queue, image, sourceX, destinationX, destinationY, width, waitList);
            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage2DInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage2DInternal(CommandQueue queue, Image2D<T> image,
            ulong sourceX, ulong destinationX, ulong destinationY, ulong width, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, 0, 0, destinationX, destinationY, 0, width, 1, 1, waitList);
        }

        #endregion
    }
}
