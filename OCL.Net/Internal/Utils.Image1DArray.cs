using System;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static Image1DArray<T> CreateImage1DArray<T>(IOpenCl library, ImageId imageId) where T : unmanaged
        {
            return Image1DArray.FromId<T>(library, imageId);
        }

        public static unsafe EventId ReadImage1DArray<T>(Image1DArray<T> image, CommandQueue queue, IntPtr handle,
            ulong index, ulong x, ulong width, ulong length, ulong rowPitch, params Event[] waitList)
            where T : unmanaged
        {
            return image.ReadInto(queue, (T*) handle, index, x, width, length, rowPitch, waitList);
        }

        public static unsafe EventId WriteImage1DArray<T>(Image1DArray<T> image, CommandQueue queue, IntPtr handle,
            ulong index, ulong x, ulong width, ulong length, ulong rowPitch, params Event[] waitList)
            where T : unmanaged
        {
            return image.WriteInto(queue, (T*) handle, index, x, width, length, rowPitch, waitList);
        }

        public static unsafe EventId FillImage1DArray<T>(Image1DArray<T> image, CommandQueue queue, IntPtr color,
            ulong index, ulong x, ulong width, ulong length, params Event[] waitList)
            where T : unmanaged
        {
            return image.FillInternal(queue, (T*) color, index, x, width, length, waitList);
        }

        public static EventId CopyImage1DArrayToImage1D<T>(CommandQueue queue,
            Image1DArray<T> source, Image1D<T> destination,
            ulong sourceIndex, ulong sourceX, ulong destinationX, ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DInternal(queue, destination,
                sourceIndex, sourceX, destinationX, width, waitList);
        }

        public static EventId CopyImage1DArrayToImage1DArray<T>(CommandQueue queue,
            Image1DArray<T> source, Image1DArray<T> destination,
            ulong sourceIndex, ulong sourceX, ulong destinationIndex, ulong destinationX,
            ulong width, ulong length, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DArrayInternal(queue, destination,
                sourceIndex, sourceX, destinationIndex, destinationX, width, length, waitList);
        }

        public static EventId CopyImage1DArrayToImage2D<T>(CommandQueue queue,
            Image1DArray<T> source, Image2D<T> destination,
            ulong sourceIndex, ulong sourceX, ulong destinationX, ulong destinationY,
            ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DInternal(queue, destination,
                sourceIndex, sourceX, destinationX, destinationY, width, waitList);
        }

        public static EventId CopyImage1DArrayToImage2DArray<T>(CommandQueue queue,
            Image1DArray<T> source, Image2DArray<T> destination,
            ulong sourceIndex, ulong sourceX, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DArrayInternal(queue, destination,
                sourceIndex, sourceX, destinationIndex, destinationX, destinationY, width, waitList);
        }

        public static EventId CopyImage1DArrayToImage3D<T>(CommandQueue queue,
            Image1DArray<T> source, Image3D<T> destination,
            ulong sourceIndex, ulong sourceX, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, params Event[] waitList)
            where T : unmanaged
        {
           return source.CopyToImage3DInternal(queue, destination,
               sourceIndex, sourceX, destinationX, destinationY, destinationZ, width, waitList);
        }
    }
}
