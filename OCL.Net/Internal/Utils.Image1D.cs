using System;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static Image1D<T> CreateImage1D<T>(IOpenCl library, ImageId imageId) where T : unmanaged
        {
            return Image1D.FromId<T>(library, imageId);
        }

        public static unsafe EventId ReadImage1D<T>(Image1D<T> image, CommandQueue queue, IntPtr handle,
            ulong x, ulong width, ulong rowPitch, params Event[] waitList)
            where T : unmanaged
        {
            return image.ReadInto(queue, (T*) handle, x, width, rowPitch, waitList);
        }

        public static unsafe EventId WriteImage1D<T>(Image1D<T> image, CommandQueue queue, IntPtr handle,
            ulong x, ulong width, ulong rowPitch, params Event[] waitList)
            where T : unmanaged
        {
            return image.WriteInto(queue, (T*) handle, x, width, rowPitch, waitList);
        }

        public static unsafe EventId FillImage1D<T>(Image1D<T> image, CommandQueue queue, IntPtr color,
            ulong x, ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return image.FillInternal(queue, (T*) color, x, width, waitList);
        }

        public static EventId CopyImage1DToImage1D<T>(CommandQueue queue,
            Image1D<T> source, Image1D<T> destination,
            ulong sourceX, ulong destinationX, ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DInternal(queue, destination, sourceX, destinationX, width, waitList);
        }

        public static EventId CopyImage1DToImage1DArray<T>(CommandQueue queue,
            Image1D<T> source, Image1DArray<T> destination,
            ulong sourceX, ulong destinationIndex, ulong destinationX, ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DArrayInternal(queue, destination,
                sourceX, destinationIndex, destinationX, width, waitList);
        }

        public static EventId CopyImage1DToImage2D<T>(CommandQueue queue,
            Image1D<T> source, Image2D<T> destination,
            ulong sourceX, ulong destinationX, ulong destinationY, ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DInternal(queue, destination,
                sourceX, destinationX, destinationY, width, waitList);
        }

        public static EventId CopyImage1DToImage2DArray<T>(CommandQueue queue,
            Image1D<T> source, Image2DArray<T> destination,
            ulong sourceX, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DArrayInternal(queue, destination,
                sourceX, destinationIndex, destinationX, destinationY, width, waitList);
        }

        public static EventId CopyImage1DToImage3D<T>(CommandQueue queue,
            Image1D<T> source, Image3D<T> destination,
            ulong sourceX, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, params Event[] waitList)
            where T : unmanaged
        {
           return source.CopyToImage3DInternal(queue, destination,
               sourceX, destinationX, destinationY, destinationZ, width, waitList);
        }
    }
}
