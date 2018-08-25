using System;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static Image3D<T> CreateImage3D<T>(IOpenCl library, ImageId imageId) where T : unmanaged
        {
            return Image3D.FromId<T>(library, imageId);
        }

        public static unsafe EventId ReadImage3D<T>(Image3D<T> image, CommandQueue queue, IntPtr handle,
            ulong x, ulong y, ulong z, ulong width, ulong height, ulong depth,
            ulong rowPitch, ulong slicePitch, params Event[] waitList)
            where T : unmanaged
        {
            return image.ReadInto(queue, (T*) handle, x, y, z, width, height, depth, rowPitch, slicePitch, waitList);
        }

        public static unsafe EventId WriteImage3D<T>(Image3D<T> image, CommandQueue queue, IntPtr handle,
            ulong x, ulong y, ulong z, ulong width, ulong height, ulong depth,
            ulong rowPitch, ulong slicePitch, params Event[] waitList)
            where T : unmanaged
        {
            return image.WriteInto(queue, (T*) handle, x, y, z, width, height, depth, rowPitch, slicePitch, waitList);
        }

        public static unsafe EventId FillImage3D<T>(Image3D<T> image, CommandQueue queue, IntPtr color,
            ulong x, ulong y, ulong z, ulong width, ulong height, ulong depth, params Event[] waitList)
            where T : unmanaged
        {
            return image.FillInternal(queue, (T*) color, x, y, z, width, height, depth, waitList);
        }

        public static EventId CopyImage3DToImage1D<T>(CommandQueue queue,
            Image3D<T> source, Image1D<T> destination,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationX, ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DInternal(queue, destination,
                sourceX, sourceY, sourceZ, destinationX, width, waitList);
        }

        public static EventId CopyImage3DToImage1DArray<T>(CommandQueue queue,
            Image3D<T> source, Image1DArray<T> destination,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationIndex, ulong destinationX,
            ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DArrayInternal(queue, destination,
                sourceX, sourceY, sourceZ, destinationIndex, destinationX, width, waitList);
        }

        public static EventId CopyImage3DToImage2D<T>(CommandQueue queue,
            Image3D<T> source, Image2D<T> destination,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationX, ulong destinationY,
            ulong width, ulong height, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DInternal(queue, destination,
                sourceX, sourceY, sourceZ, destinationX, destinationY, width, height, waitList);
        }

        public static EventId CopyImage3DToImage2DArray<T>(CommandQueue queue,
            Image3D<T> source, Image2DArray<T> destination,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, ulong height, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DArrayInternal(queue, destination,
                sourceX, sourceY, sourceZ, destinationIndex, destinationX, destinationY, width, height, waitList);
        }

        public static EventId CopyImage3DToImage3D<T>(CommandQueue queue,
            Image3D<T> source, Image3D<T> destination,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, ulong depth, params Event[] waitList)
            where T : unmanaged
        {
           return source.CopyToImage3DInternal(queue, destination,
               sourceX, sourceY, sourceZ, destinationX, destinationY, destinationZ, width, height, depth, waitList);
        }
    }
}
