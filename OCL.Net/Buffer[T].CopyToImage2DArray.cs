using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region CopyToImage2DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image, 0, 0, 0, 0, image.Width, image.Height, image.Length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            int destinationIndex, int length = 1, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                (ulong) destinationIndex, 0, 0, 0, image.Width, image.Height, (ulong) length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            ulong destinationIndex, ulong length = 1, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image, destinationIndex, 0, 0, 0, image.Width, image.Height, length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            int sourceOffset, int destinationIndex, int destinationX, int destinationY,
            int width, int height, int length = 1, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                (ulong) sourceOffset, (ulong) destinationIndex, (ulong) destinationX, (ulong) destinationY,
                (ulong) width, (ulong) height, (ulong) length, waitList);
        }

        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            ulong sourceOffset, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, ulong height, ulong length = 1, params Event[] waitList)
        {
            var eventId = CopyToImage2DArrayInternal(queue, image,
                sourceOffset, destinationIndex, destinationX, destinationY, width, height, length, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage2DArrayInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage2DArrayInternal(CommandQueue queue, Image2DArray<T> image,
            ulong sourceOffset, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, ulong height, ulong length, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image,
                sourceOffset, destinationX, destinationY, destinationIndex, width, height, length, waitList);
        }

        #endregion
    }
}
