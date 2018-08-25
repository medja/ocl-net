using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2D<T>
    {
        #region CopyToImage1DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            int sourceY, int destinationIndex, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image, 0, (ulong) sourceY, (ulong) destinationIndex, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            ulong sourceY, ulong destinationIndex, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image, 0, sourceY, destinationIndex, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            int sourceX, int sourceY, int destinationIndex, int destinationX,
            int width, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image,
                (ulong) sourceX, (ulong) sourceY, (ulong) destinationIndex, (ulong) destinationX,
                (ulong) width, waitList);
        }

        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            ulong sourceX, ulong sourceY, ulong destinationIndex, ulong destinationX,
            ulong width, params Event[] waitList)
        {
            var eventId = CopyToImage1DArrayInternal(queue, image,
                sourceX, sourceY, destinationIndex, destinationX, width, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage1DArrayInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage1DArrayInternal(CommandQueue queue, Image1DArray<T> image,
            ulong sourceX, ulong sourceY, ulong destinationIndex, ulong destinationX,
            ulong width, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceY, 0, destinationX, destinationIndex, 0, width, 1, 1, waitList);
        }

        #endregion
    }
}
