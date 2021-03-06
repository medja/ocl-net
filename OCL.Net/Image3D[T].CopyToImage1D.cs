using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image3D<T>
    {
        #region CopyToImage1D

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1D<T>> CopyToImage1D(CommandQueue queue, Image1D<T> image,
            int sourceY, int sourceZ, params Event[] waitList)
        {
            return CopyToImage1D(queue, image, 0, (ulong) sourceY, (ulong) sourceZ, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1D<T>> CopyToImage1D(CommandQueue queue, Image1D<T> image,
            ulong sourceY, ulong sourceZ, params Event[] waitList)
        {
            return CopyToImage1D(queue, image, 0, sourceY, sourceZ, 0, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image1D<T>> CopyToImage1D(CommandQueue queue, Image1D<T> image,
            int sourceX, int sourceY, int sourceZ, int destinationX, int width, params Event[] waitList)
        {
            return CopyToImage1D(queue, image,
                (ulong) sourceX, (ulong) sourceY, (ulong) sourceZ, (ulong) destinationX, (ulong) width, waitList);
        }

        public AutoDisposedEvent<Image1D<T>> CopyToImage1D(CommandQueue queue, Image1D<T> image,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationX, ulong width, params Event[] waitList)
        {
            var eventId = CopyToImage1DInternal(queue, image,
                sourceX, sourceY, sourceZ, destinationX, width, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage1DInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage1DInternal(CommandQueue queue, Image1D<T> image,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationX, ulong width, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceY, sourceZ, destinationX, 0, 0, width, 1, 1, waitList);
        }

        #endregion
    }
}
