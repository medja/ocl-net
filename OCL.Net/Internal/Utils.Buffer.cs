using System;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static Net.Buffer<T> CreateBuffer<T>(IOpenCl library, BufferId bufferId) where T : unmanaged
        {
            return Buffer.FromId<T>(library, bufferId);
        }

        public static unsafe EventId ReadBuffer<T>(Net.Buffer<T> buffer, CommandQueue queue, IntPtr handle,
            ulong offset, ulong length, params Event[] waitList)
            where T : unmanaged
        {
            return buffer.ReadInto(queue, (T*) handle, offset, length, waitList);
        }

        public static unsafe EventId WriteBuffer<T>(Net.Buffer<T> buffer, CommandQueue queue, IntPtr handle,
            ulong offset, ulong length, params Event[] waitList)
            where T : unmanaged
        {
            return buffer.WriteInto(queue, (T*) handle, offset, length, waitList);
        }

        public static unsafe EventId ReadBufferRect<T>(Net.Buffer<T> buffer, CommandQueue queue, IntPtr handle,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong hostX, ulong hostY, ulong hostZ,
            ulong width, ulong height, ulong depth,
            ulong bufferWidth, ulong bufferHeight, ulong hostWidth, ulong hostHeight, params Event[] waitList)
            where T : unmanaged
        {
            return buffer.Read3DRectInto(queue, (T*) handle, bufferX, bufferY, bufferZ, hostX, hostY, hostZ,
                width, height, depth, bufferWidth, bufferHeight, hostWidth, hostHeight, waitList);
        }

        public static unsafe EventId WriteBufferRect<T>(Net.Buffer<T> buffer, CommandQueue queue, IntPtr handle,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong hostX, ulong hostY, ulong hostZ,
            ulong width, ulong height, ulong depth,
            ulong bufferWidth, ulong bufferHeight, ulong hostWidth, ulong hostHeight, params Event[] waitList)
            where T : unmanaged
        {
            return buffer.Write3DRectInto(queue, (T*) handle, bufferX, bufferY, bufferZ, hostX, hostY, hostZ,
                width, height, depth, bufferWidth, bufferHeight, hostWidth, hostHeight, waitList);
        }

        public static EventId FillBuffer<T>(Net.Buffer<T> buffer, CommandQueue queue, Span<T> pattern,
            ulong offset, ulong length, params Event[] waitList)
            where T : unmanaged
        {
            return buffer.FillInternal(queue, pattern, offset, length, waitList);
        }

        public static EventId CopyBuffer<T>(CommandQueue queue, Net.Buffer<T> source, Net.Buffer<T> destination,
            ulong sourceOffset, ulong destinationOffset, ulong length, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToBufferInternal(queue, destination, sourceOffset, destinationOffset, length, waitList);
        }

        public static EventId CopyBufferRect<T>(CommandQueue queue, Net.Buffer<T> source, Net.Buffer<T> destination,
            ulong sourceX, ulong sourceY, ulong sourceZ, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, ulong depth,
            ulong sourceRowPitch, ulong sourceSlicePitch, ulong destinationRowPitch, ulong destinationSlicePitch,
            params Event[] waitList)
            where T : unmanaged
        {
            return source.Copy3DRectToBufferInternal(queue, destination,
                sourceX, sourceY, sourceZ, destinationX, destinationY, destinationZ, width, height, depth,
                sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, waitList);
        }

        public static EventId CopyBufferToImage1D<T>(CommandQueue queue,
            Net.Buffer<T> source, Image1D<T> destination,
            ulong sourceOffset, ulong destinationX, ulong width, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DInternal(queue, destination, sourceOffset, destinationX, width, waitList);
        }

        public static EventId CopyBufferToImage1DArray<T>(CommandQueue queue,
            Net.Buffer<T> source, Image1DArray<T> destination,
            ulong sourceOffset, ulong destinationIndex, ulong destinationX,
            ulong width, ulong length, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage1DArrayInternal(queue, destination,
                sourceOffset, destinationIndex, destinationX, width, length, waitList);
        }

        public static EventId CopyBufferToImage2D<T>(CommandQueue queue,
            Net.Buffer<T> source, Image2D<T> destination,
            ulong sourceOffset, ulong destinationX, ulong destinationY,
            ulong width, ulong height, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DInternal(queue, destination,
                sourceOffset, destinationX, destinationY, width, height, waitList);
        }

        public static EventId CopyBufferToImage2DArray<T>(CommandQueue queue,
            Net.Buffer<T> source, Image2DArray<T> destination,
            ulong sourceOffset, ulong destinationIndex, ulong destinationX, ulong destinationY,
            ulong width, ulong height, ulong length, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage2DArrayInternal(queue, destination,
                sourceOffset, destinationIndex, destinationX, destinationY, width, height, length, waitList);
        }

        public static EventId CopyBufferToImage3D<T>(CommandQueue queue,
            Net.Buffer<T> source, Image3D<T> destination,
            ulong sourceOffset, ulong destinationX, ulong destinationY, ulong destinationZ,
            ulong width, ulong height, ulong depth, params Event[] waitList)
            where T : unmanaged
        {
            return source.CopyToImage3DInternal(queue, destination,
                sourceOffset, destinationX, destinationY, destinationZ, width, height, depth, waitList);
        }
    }
}
