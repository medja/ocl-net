using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2DArray<T>
    {
        #region CopyToImage3D

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            int sourceIndex, int destinationZ, params Event[] waitList)
        {
            return CopyToImage3D(queue, image,
                (ulong) sourceIndex, 0, 0, 0, 0, (ulong) destinationZ, Width, Height, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            ulong sourceIndex, ulong destinationZ, params Event[] waitList)
        {
            return CopyToImage3D(queue, image, sourceIndex, 0, 0, 0, 0, destinationZ, Width, Height, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            int sourceIndex, int sourceX, int sourceY, int destinationX, int destinationY, int destinationZ,
            int width, int height, params Event[] waitList)
        {
            return CopyToImage3D(queue, image,
                (ulong) sourceIndex, (ulong) sourceX, (ulong) sourceY,
                (ulong) destinationX, (ulong) destinationY, (ulong) destinationZ,
                (ulong) width, (ulong) height, waitList);
        }

        public AutoDisposedEvent<Image3D<T>> CopyToImage3D(CommandQueue queue, Image3D<T> image,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, params Event[] waitList)
        {
            var eventId = CopyToImage3DInternal(queue, image,
                sourceIndex, sourceX, sourceY, destinationX, destinationY, destinationZ, width, height, waitList);

            return AutoDisposedEvent.FromId(Library, eventId, image);
        }

        #endregion

        #region CopyToImage3DInternal

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EventId CopyToImage3DInternal(CommandQueue queue, Image3D<T> image,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, params Event[] waitList)
        {
            return CopyToImageInternal(queue, image.Id,
                sourceX, sourceY, sourceIndex, destinationX, destinationY, destinationZ, width, height, 1, waitList);
        }

        #endregion
    }
}
