using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1DArray<T>
    {
        #region CopyToImage2DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            int sourceIndex, int destinationIndex, int destinationY, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                (ulong) sourceIndex, 0, (ulong) destinationIndex, (ulong) destinationY, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            ulong sourceIndex, ulong destinationIndex, ulong destinationY, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                sourceIndex, 0, destinationIndex, destinationY, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            int sourceIndex, int sourceX, int destinationIndex, int destinationX, int destinationY,
            int width, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                (ulong) sourceIndex, (ulong) sourceX,
                (ulong) destinationIndex, (ulong) destinationX, (ulong) destinationY,
                (ulong) width, waitList);
        }

        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            ulong sourceIndex, ulong sourceX, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, params Event[] waitList)
        {
            var eventId = CopyToImage2DArrayInternal(queue, image,
                sourceIndex, sourceX, destinationIndex, destinationX, destinationY, width, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage2DArrayInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage2DArrayInternal(CommandQueue queue, Image2DArray<T> image,
            ulong sourceIndex, ulong sourceX, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceIndex, 0, destinationX, destinationY, destinationIndex, width, 1, 1, waitList);
        }

        #endregion
    }
}
