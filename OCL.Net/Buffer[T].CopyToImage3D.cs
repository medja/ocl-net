using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region CopyToImage3D

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            params Event[] waitList)
        {
            return CopyToImage3D(queue, image, 0, 0, 0, 0, image.Width, image.Height, image.Depth, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            int sourceOffset, int destinationX, int destinationY, int destinationZ,
            int width, int height, int depth, params Event[] waitList)
        {
            return CopyToImage3D(queue, image,
                (ulong) sourceOffset, (ulong) destinationX, (ulong) destinationY, (ulong) destinationZ,
                (ulong) width, (ulong) height, (ulong) depth, waitList);
        }

        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            ulong sourceOffset, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, ulong depth, params Event[] waitList)
        {
            var eventId = CopyToImage3DInternal(queue, image,
                sourceOffset, destinationX, destinationY, destinationZ, width, height, depth, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage3DInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage3DInternal(CommandQueue queue, Image3D<T> image,
            ulong sourceOffset, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, ulong depth, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image,
                sourceOffset, destinationX, destinationY, destinationZ, width, height, depth, waitList);
        }

        #endregion
    }
}
