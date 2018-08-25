using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region CopyToImage1DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image, 0, 0, 0, image.Width, image.Length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            int destinationIndex, int length = 1, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image,
                (ulong) destinationIndex, 0, 0, image.Width, (ulong) length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            ulong destinationIndex, ulong length = 1, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image, destinationIndex, 0, 0, image.Width, length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            int sourceOffset, int destinationIndex, int destinationX,
            int width, int length = 1, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image,
                (ulong) sourceOffset, (ulong) destinationIndex, (ulong) destinationX,
                (ulong) width, (ulong) length, waitList);
        }

        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            ulong sourceOffset, ulong destinationIndex, ulong destinationX,
            ulong width, ulong length = 1, params Event[] waitList)
        {
            var eventId = CopyToImage1DArrayInternal(queue, image,
                sourceOffset, destinationIndex, destinationX, width, length, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage1DArrayInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage1DArrayInternal(CommandQueue queue, Image1DArray<T> image,
            ulong sourceOffset, ulong destinationIndex, ulong destinationX,
            ulong width, ulong length, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image,
                sourceOffset, destinationX, 0, destinationIndex, width, length, 1, waitList);
        }

        #endregion
    }
}
