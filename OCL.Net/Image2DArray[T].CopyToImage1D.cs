using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2DArray<T>
    {
        #region CopyToImage1D

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1D<T>> CopyToImage1D(CommandQueue queue, Image1D<T> image,
            int sourceIndex, int sourceY, params Event[] waitList)
        {
            return CopyToImage1D(queue, image, (ulong) sourceIndex, 0, (ulong) sourceY, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1D<T>> CopyToImage1D(CommandQueue queue, Image1D<T> image,
            ulong sourceIndex, ulong sourceY, params Event[] waitList)
        {
            return CopyToImage1D(queue, image, sourceIndex, 0, sourceY, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1D<T>> CopyToImage1D(CommandQueue queue, Image1D<T> image,
            int sourceIndex, int sourceX, int sourceY, int destinationX, int width, params Event[] waitList)
        {
            return CopyToImage1D(queue, image,
                (ulong) sourceIndex, (ulong) sourceX, (ulong) sourceY, (ulong) destinationX, (ulong) width, waitList);
        }

        public AutoDisposedEvent<Image1D<T>> CopyToImage1D(CommandQueue queue, Image1D<T> image,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationX, ulong width, params Event[] waitList)
        {
            var eventId = CopyToImage1DInternal(queue, image,
                sourceIndex, sourceX, sourceY, destinationX, width, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage1DInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage1DInternal(CommandQueue queue, Image1D<T> image,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationX, ulong width, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceY, sourceIndex, destinationX, 0, 0, width, 1, 1, waitList);
        }

        #endregion
    }
}
