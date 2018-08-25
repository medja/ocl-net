using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1D<T>
    {
        #region CopyToImage2DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            int destinationIndex, int destinationY, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                0, (ulong) destinationIndex, 0, (ulong) destinationY, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            ulong destinationIndex, ulong destinationY, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image, 0, destinationIndex, 0, destinationY, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            int sourceX, int destinationIndex, int destinationX, int destinationY,
            int width, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                (ulong) sourceX, (ulong) destinationIndex, (ulong) destinationX, (ulong) destinationY,
                (ulong) width, waitList);
        }

        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            ulong sourceX, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, params Event[] waitList)
        {
            var eventId = CopyToImage2DArrayInternal(queue, image,
                sourceX, destinationIndex, destinationX, destinationY, width, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage2DArrayInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage2DArrayInternal(CommandQueue queue, Image2DArray<T> image,
            ulong sourceX, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, 0, 0, destinationX, destinationY, destinationIndex, width, 1, 1, waitList);
        }

        #endregion
    }
}
