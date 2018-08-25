using System;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static Image2DArray<T> CreateImage2DArray<T>(IOpenCl library, ImageId imageId) where T : unmanaged
        {
            return Image2DArray.FromId<T>(library, imageId);
        }

        public static unsafe EventId ReadImage2DArray<T>(Image2DArray<T> image, CommandQueue queue, IntPtr handle,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length, ulong rowPitch, ulong slicePitch,
            params Event[] waitList)
            where T : unmanaged
        {
            return image.ReadInto(queue, (T*) handle,
                index, x, y, width, height, length, rowPitch, slicePitch, waitList);
        }

        public static unsafe EventId WriteImage2DArray<T>(Image2DArray<T> image, CommandQueue queue, IntPtr handle,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length, ulong rowPitch, ulong slicePitch,
            params Event[] waitList)
            where T : unmanaged
        {
            return image.WriteInto(queue, (T*) handle,
                index, x, y, width, height, length, rowPitch, slicePitch, waitList);
        }

        public static unsafe EventId FillImage2DArray<T>(Image2DArray<T> image, CommandQueue queue, IntPtr color,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length, params Event[] waitList)
            where T : unmanaged
        {
            return image.FillInternal(queue, (T*) color, index, x, y, width, height, length, waitList);
        }

        public static EventId CopyImage2DArrayToImage1D<T>(CommandQueue queue,
            Image2DArray<T> source, Image1D<T> destination,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationX, ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DInternal(queue, destination,
                sourceIndex, sourceX, sourceY, destinationX, width, waitList);
        }

        public static EventId CopyImage2DArrayToImage1DArray<T>(CommandQueue queue,
            Image2DArray<T> source, Image1DArray<T> destination,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationIndex, ulong destinationX,
            ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DArrayInternal(queue, destination,
                sourceIndex, sourceX, sourceY, destinationIndex, destinationX, width, waitList);
        }

        public static EventId CopyImage2DArrayToImage2D<T>(CommandQueue queue,
            Image2DArray<T> source, Image2D<T> destination,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationX, ulong destinationY,
            ulong width, ulong height, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DInternal(queue, destination,
                sourceIndex, sourceX, sourceY, destinationX, destinationY, width, height, waitList);
        }

        public static EventId CopyImage2DArrayToImage2DArray<T>(CommandQueue queue,
            Image2DArray<T> source, Image2DArray<T> destination,
            ulong sourceIndex, ulong sourceX, ulong sourceY,
            ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, ulong height, ulong length, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DArrayInternal(queue, destination,
                sourceIndex, sourceX, sourceY, destinationIndex, destinationX, destinationY,
                width, height, length, waitList);
        }

        public static EventId CopyImage2DArrayToImage3D<T>(CommandQueue queue,
            Image2DArray<T> source, Image3D<T> destination,
            ulong sourceIndex, ulong sourceX, ulong sourceY, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, params Event[] waitList)
            where T : unmanaged
        {
           return source.CopyToImage3DInternal(queue, destination,
               sourceIndex, sourceX, sourceY, destinationX, destinationY, destinationZ, width, height, waitList);
        }
    }
}
