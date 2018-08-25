using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image3D<T>
    {
        #region CopyToImage3D

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            params Event[] waitList)
        {
            return CopyToImage3D(queue, image, 0, 0, 0, 0, 0, 0, Width, Height, Depth, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            int sourceX, int sourceY, int sourceZ, int destinationX, int destinationY, int destinationZ,
            int width, int height, int depth, params Event[] waitList)
        {
            return CopyToImage3D(queue, image,
                (ulong) sourceX, (ulong) sourceY, (ulong) sourceZ,
                (ulong) destinationX, (ulong) destinationY, (ulong) destinationZ,
                (ulong) width, (ulong) height, (ulong) depth, waitList);
        }

        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, ulong depth, params Event[] waitList)
        {
            var eventId = CopyToImage3DInternal(queue, image,
                sourceX, sourceY, sourceZ, destinationX, destinationY, destinationZ, width, height, depth, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage3DInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage3DInternal(CommandQueue queue, Image3D<T> image,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, ulong depth, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceY, sourceZ, destinationX, destinationY, destinationZ, width, height, depth, waitList);
        }

        #endregion
    }
}
