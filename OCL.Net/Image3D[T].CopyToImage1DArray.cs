using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image3D<T>
    {
        #region CopyToImage1DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            int sourceY, int sourceZ, int destinationIndex, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image,
                0, (ulong) sourceY, (ulong) sourceZ, (ulong) destinationIndex, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            ulong sourceY, ulong sourceZ, ulong destinationIndex, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image, 0, sourceY, sourceZ, destinationIndex, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            int sourceX, int sourceY, int sourceZ, int destinationIndex, int destinationX,
            int width, params Event[] waitList)
        {
            return CopyToImage1DArray(queue, image,
                (ulong) sourceX, (ulong) sourceY, (ulong) sourceZ, (ulong) destinationIndex, (ulong) destinationX,
                (ulong) width, waitList);
        }

        public AutoDisposedEvent<Image1DArray<T>> CopyToImage1DArray(CommandQueue queue, Image1DArray<T> image,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationIndex, ulong destinationX,
            ulong width, params Event[] waitList)
        {
            var eventId = CopyToImage1DArrayInternal(queue, image,
                sourceX, sourceY, sourceZ, destinationIndex, destinationX, width, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage1DArrayInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage1DArrayInternal(CommandQueue queue, Image1DArray<T> image,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationIndex, ulong destinationX,
            ulong width, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceY, sourceZ, destinationX, destinationIndex, 0, width, 1, 1, waitList);
        }

        #endregion
    }
}
