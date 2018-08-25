using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1DArray<T>
    {
        #region CopyToImage1DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image, 0, 0, 0, 0, Width, Length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            int sourceIndex, int destinationIndex, int length = 1, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image,
                (ulong) sourceIndex, 0, (ulong) destinationIndex, 0, Width, (ulong) length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            ulong sourceIndex, ulong destinationIndex, ulong length = 1, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image, sourceIndex, 0, destinationIndex, 0, Width, length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            int sourceIndex, int sourceX, int destinationIndex, int destinationX,
            int width, int length = 1, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image,
                (ulong) sourceIndex, (ulong) sourceX, (ulong) destinationIndex, (ulong) destinationX,
                (ulong) width, (ulong) length, waitList);
        }

        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            ulong sourceIndex, ulong sourceX, ulong destinationIndex, ulong destinationX,
            ulong width, ulong length = 1, params Event[] waitList)
        {
            var eventId = CopyToImage1DArrayInternal(queue, image,
                sourceIndex, sourceX, destinationIndex, destinationX, width, length, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage1DArrayInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage1DArrayInternal(CommandQueue queue, Image1DArray<T> image,
            ulong sourceIndex, ulong sourceX, ulong destinationIndex, ulong destinationX,
            ulong width, ulong length, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceIndex, 0, destinationX, destinationIndex, 0, width, length, 1, waitList);
        }

        #endregion
    }
}
