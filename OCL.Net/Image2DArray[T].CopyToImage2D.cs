using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2DArray<T>
    {
        #region CopyToImage2D

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2D<T>> CopyToImage2D(CommandQueue queue, Image2D<T> image,
            int sourceIndex, params Event[] waitList)
        {
            return CopyToImage2D(queue, image, (ulong) sourceIndex, 0, 0, 0, 0, Width, Height, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2D<T>> CopyToImage2D(CommandQueue queue, Image2D<T> image,
            ulong sourceIndex, params Event[] waitList)
        {
            return CopyToImage2D(queue, image, sourceIndex, 0, 0, 0, 0, Width, Height, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2D<T>> CopyToImage2D(CommandQueue queue, Image2D<T> image,
            int sourceIndex, int sourceX, int sourceY, int destinationX, int destinationY,
            int width, int height, params Event[] waitList)
        {
            return CopyToImage2D(queue, image,
                (ulong) sourceIndex, (ulong) sourceX, (ulong) sourceY, (ulong) destinationX, (ulong) destinationY,
                (ulong) width, (ulong) height, waitList);
        }

        public AutoDisposedEvent<Image2D<T>> CopyToImage2D(CommandQueue queue, Image2D<T> image,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationX, ulong destinationY,
            ulong width, ulong height, params Event[] waitList)
        {
            var eventId = CopyToImage2DInternal(queue, image,
                sourceIndex, sourceX, sourceY, destinationX, destinationY, width, height, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage2DInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage2DInternal(CommandQueue queue, Image2D<T> image,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationX, ulong destinationY,
            ulong width, ulong height, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceY, sourceIndex, destinationX, destinationY, 0, width, height, 1, waitList);
        }

        #endregion
    }
}
