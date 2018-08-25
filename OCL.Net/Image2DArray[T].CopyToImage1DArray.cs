using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2DArray<T>
    {
        #region CopyToImage1DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            int sourceIndex, int sourceY, int destinationIndex, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image,
                (ulong) sourceIndex, 0, (ulong) sourceY, (ulong) destinationIndex, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            ulong sourceIndex, ulong sourceY, ulong destinationIndex, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image, sourceIndex, 0, sourceY, destinationIndex, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            int sourceIndex, int sourceX, int sourceY, int destinationIndex, int destinationX,
            int width, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image,
                (ulong) sourceIndex, (ulong) sourceX, (ulong) sourceY, (ulong) destinationIndex, (ulong) destinationX,
                (ulong) width, waitList);
        }

        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationIndex, ulong destinationX,
            ulong width, params Event[] waitList)
        {
            var eventId = CopyToImage1DArrayInternal(queue, image,
                sourceIndex, sourceX, sourceY, destinationIndex, destinationX, width, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage1DArrayInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage1DArrayInternal(CommandQueue queue, Image1DArray<T> image,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationIndex, ulong destinationX,
            ulong width, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceY, sourceIndex, destinationX, destinationIndex, 0, width, 1, 1, waitList);
        }

        #endregion
    }
}
