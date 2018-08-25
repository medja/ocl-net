using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image1D<T>
    {
        #region CopyToImage3D

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            int destinationY, int destinationZ, params Event[] waitList)
        {
            return CopyToImage3D(queue, image, 0, 0, (ulong) destinationY, (ulong) destinationZ, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            ulong destinationY, ulong destinationZ, params Event[] waitList)
        {
            return CopyToImage3D(queue, image, 0, 0, destinationY, destinationZ, Width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            int sourceX, int destinationX, int destinationY, int destinationZ, int width, params Event[] waitList)
        {
            return CopyToImage3D(queue, image,
                (ulong) sourceX, (ulong) destinationX, (ulong) destinationY, (ulong) destinationZ,
                (ulong) width, waitList);
        }

        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            ulong sourceX, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width,  params Event[] waitList)
        {
            var eventId = CopyToImage3DInternal(queue, image,
                sourceX, destinationX, destinationY, destinationZ, width, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage3DInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage3DInternal(CommandQueue queue, Image3D<T> image,
            ulong sourceX, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, 0, 0, destinationX, destinationY, destinationZ, width, 1, 1, waitList);
        }

        #endregion
    }
}
