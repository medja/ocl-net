using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image3D<T>
    {
        #region CopyToImage2DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            int sourceZ, int destinationIndex, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                0, 0, (ulong) sourceZ, (ulong) destinationIndex, 0, 0, Width, Height, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            ulong sourceZ, ulong destinationIndex, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image, 0, 0, sourceZ, destinationIndex, 0, 0, Width, Height, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            int sourceX, int sourceY, int sourceZ, int destinationIndex, int destinationX, int destinationY,
            int width, int height, params Event[] waitList)
        {
            return CopyToImage2DArray(queue, image,
                (ulong) sourceX, (ulong) sourceY, (ulong) sourceZ,
                (ulong) destinationIndex, (ulong) destinationX, (ulong) destinationY,
                (ulong) width, (ulong) height, waitList);
        }

        public AutoDisposedEvent<Image2DArray<T>> CopyToImage2DArray(CommandQueue queue, Image2DArray<T> image,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, ulong height, params Event[] waitList)
        {
            var eventId = CopyToImage2DArrayInternal(queue, image,
                sourceX, sourceY, sourceZ, destinationIndex, destinationX, destinationY, width, height, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage2DArrayInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage2DArrayInternal(CommandQueue queue, Image2DArray<T> image,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, ulong height, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceY, sourceZ, destinationX, destinationY, destinationIndex, width, height, 1, waitList);
        }

        #endregion
    }
}
