using System;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static Image2D<T> CreateImage2D<T>(IOpenCl library, ImageId imageId) where T : unmanaged
        {
            return Image2D.FromId<T>(library, imageId);
        }

        public static unsafe EventId ReadImage2D<T>(Image2D<T> image, CommandQueue queue, IntPtr handle,
            ulong x, ulong y, ulong width, ulong height, ulong rowPitch, ulong slicePitch, params Event[] waitList)
            where T : unmanaged
        {
            return image.ReadInto(queue, (T*) handle, x, y, width, height, rowPitch, slicePitch, waitList);
        }

        public static unsafe EventId WriteImage2D<T>(Image2D<T> image, CommandQueue queue, IntPtr handle,
            ulong x, ulong y, ulong width, ulong height, ulong rowPitch, ulong slicePitch, params Event[] waitList)
            where T : unmanaged
        {
            return image.WriteInto(queue, (T*) handle, x, y, width, height, rowPitch, slicePitch, waitList);
        }

        public static unsafe EventId FillImage2D<T>(Image2D<T> image, CommandQueue queue, IntPtr color,
            ulong x, ulong y, ulong width, ulong height, params Event[] waitList)
            where T : unmanaged
        {
            return image.FillInternal(queue, (T*) color, x, y, width, height, waitList);
        }

        public static EventId CopyImage2DToImage1D<T>(CommandQueue queue,
            Image2D<T> source, Image1D<T> destination,
            ulong sourceX, ulong sourceY, ulong destinationX, ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DInternal(queue, destination,
                sourceX, sourceY, destinationX, width, waitList);
        }

        public static EventId CopyImage2DToImage1DArray<T>(CommandQueue queue,
            Image2D<T> source, Image1DArray<T> destination,
            ulong sourceX, ulong sourceY, ulong destinationIndex, ulong destinationX,
            ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DArrayInternal(queue, destination,
                sourceX, sourceY, destinationIndex, destinationX, width, waitList);
        }

        public static EventId CopyImage2DToImage2D<T>(CommandQueue queue,
            Image2D<T> source, Image2D<T> destination,
            ulong sourceX, ulong sourceY, ulong destinationX, ulong destinationY,
            ulong width, ulong height, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DInternal(queue, destination,
                sourceX, sourceY, destinationX, destinationY, width, height, waitList);
        }

        public static EventId CopyImage2DToImage2DArray<T>(CommandQueue queue,
            Image2D<T> source, Image2DArray<T> destination,
            ulong sourceX, ulong sourceY, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, ulong height, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DArrayInternal(queue, destination,
                sourceX, sourceY, destinationIndex, destinationX, destinationY,
                width, height, waitList);
        }

        public static EventId CopyImage2DToImage3D<T>(CommandQueue queue,
            Image2D<T> source, Image3D<T> destination,
            ulong sourceX, ulong sourceY, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, params Event[] waitList)
            where T : unmanaged
        {
           return source.CopyToImage3DInternal(queue, destination,
               sourceX, sourceY, destinationX, destinationY, destinationZ, width, height, waitList);
        }
    }
}
