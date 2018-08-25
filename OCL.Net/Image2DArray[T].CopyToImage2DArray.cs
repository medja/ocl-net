using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2DArray<T>
    {
        #region CopyToImage2DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image, 0, 0, 0, 0, 0, 0, Width, Height, Length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            int sourceIndex, int destinationIndex, int length = 1, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                (ulong) sourceIndex, 0, 0, (ulong) destinationIndex, 0, 0, Width, Height, (ulong) length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            ulong sourceIndex, ulong destinationIndex, ulong length = 1, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                sourceIndex, 0, 0, destinationIndex, 0, 0, Width, Height, length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            int sourceIndex, int sourceX, int sourceY, int destinationIndex, int destinationX, int destinationY,
            int width, int height, int length = 1, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                (ulong) sourceIndex, (ulong) sourceX, (ulong) sourceY,
                (ulong) destinationIndex, (ulong) destinationX, (ulong) destinationY,
                (ulong) width, (ulong) height, (ulong) length, waitList);
        }

        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            ulong sourceIndex, ulong sourceX, ulong sourceY,
            ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, ulong height, ulong length = 1, params Event[] waitList)
        {
            var eventId = CopyToImage2DArrayInternal(queue, image,
                sourceIndex, sourceX, sourceY, destinationIndex, destinationX, destinationY,
                width, height, length, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage2DArrayInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage2DArrayInternal(CommandQueue queue, Image2DArray<T> image,
            ulong sourceIndex, ulong sourceX, ulong sourceY,
            ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, ulong height, ulong length, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceY, sourceIndex, destinationX, destinationY, destinationIndex,
                width, height, length, waitList);
        }

        #endregion
    }
}
