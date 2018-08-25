using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image1D
    {
        #region For

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image1D<T> For<T>(Context context, MemoryManager<T> memoryManager,
            ImageChannelOrder channelOrder, bool normalized,
            int width = 0, int rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return For(context, memoryManager, channelOrder, normalized,
                (ulong) width, (ulong) rowPitch, deviceFlags, hostFlags);
        }

        public static Image1D<T> For<T>(Context context, MemoryManager<T> memoryManager,
            ImageChannelOrder channelOrder, bool normalized,
            ulong width = 0, ulong rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            if (memoryManager == null)
                throw new ArgumentNullException(nameof(memoryManager));

            return ForMemory(context, memoryManager.Memory, memoryManager, channelOrder, normalized,
                width, rowPitch, deviceFlags, hostFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image1D<T> For<T>(Context context, MemoryManager<T> memoryManager,
            ImageFormat format,
            int width = 0, int rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return For(context, memoryManager, format, (ulong) width, (ulong) rowPitch, deviceFlags, hostFlags);
        }

        public static Image1D<T> For<T>(Context context, MemoryManager<T> memoryManager,
            ImageFormat format,
            ulong width = 0, ulong rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            if (memoryManager == null)
                throw new ArgumentNullException(nameof(memoryManager));

            return ForMemory(context, memoryManager.Memory, memoryManager, format,
                width, rowPitch, deviceFlags, hostFlags);
        }

        #endregion

        #region ForMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image1D<T> ForMemory<T>(Context context, Memory<T> memory,
            ImageChannelOrder channelOrder, bool normalized,
            int width = 0, int rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return ForMemory(context, memory, channelOrder, normalized,
                (ulong) width, (ulong) rowPitch, deviceFlags, hostFlags);
        }

        public static Image1D<T> ForMemory<T>(Context context, Memory<T> memory,
            ImageChannelOrder channelOrder, bool normalized,
            ulong width = 0, ulong rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return ForMemory(context, memory, null, channelOrder, normalized, width, rowPitch, deviceFlags, hostFlags);
        }

        private static Image1D<T> ForMemory<T>(Context context, Memory<T> memory, IDisposable disposable,
            ImageChannelOrder channelOrder, bool normalized,
            ulong width = 0, ulong rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            var format = new ImageFormatBuilder()
                .WithDataType(typeof(T))
                .WithNormalized(normalized)
                .WithChannelOrder(channelOrder)
                .Build();

            return ForMemory(context, memory, disposable, format, width, rowPitch, deviceFlags, hostFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image1D<T> ForMemory<T>(Context context, Memory<T> memory, ImageFormat format,
            int width = 0, int rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return ForMemory(context, memory, format, (ulong) width, (ulong) rowPitch, deviceFlags, hostFlags);
        }

        public static Image1D<T> ForMemory<T>(Context context, Memory<T> memory, ImageFormat format,
            ulong width = 0, ulong rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return ForMemory(context, memory, null, format, width, rowPitch, deviceFlags, hostFlags);
        }

        private static unsafe Image1D<T> ForMemory<T>(Context context, Memory<T> memory, IDisposable disposable,
            ImageFormat format,
            ulong width = 0, ulong rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (rowPitch == 0)
                rowPitch = (ulong) memory.Length * (ulong) sizeof(T);

            if (width == 0)
                width = rowPitch / (ulong) format.ElementSize;

            if (rowPitch > (ulong) memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Memory capacity is too small");

            var lib = context.Library;
            var flags = deviceFlags.ToDeviceNativeFlags() | hostFlags.ToHostNativeFlags() | MemFlags.MemUseHostPtr;
            var description = ImageDescription.Create1D(width, rowPitch);

            var handle = memory.Pin();

            ErrorCode errorCode;
            var id = lib.clCreateImageUnsafe(context, flags, &format, &description, handle.Pointer, &errorCode);
            errorCode.HandleError();

            return FromId<T>(lib, id, handle, disposable);
        }

        #endregion

        #region ForBuffer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image1D<T> ForBuffer<T>(Buffer<T> buffer,
            ImageChannelOrder channelOrder, bool normalized,
            int width = 0, int rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return ForBuffer(buffer, channelOrder, normalized, (ulong) width, (ulong) rowPitch, deviceFlags, hostFlags);
        }

        public static Image1D<T> ForBuffer<T>(Buffer<T> buffer,
            ImageChannelOrder channelOrder, bool normalized,
            ulong width = 0, ulong rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            var format = new ImageFormatBuilder()
                .WithDataType(typeof(T))
                .WithNormalized(normalized)
                .WithChannelOrder(channelOrder)
                .Build();

            return ForBuffer(buffer, format, width, rowPitch, deviceFlags, hostFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image1D<T> ForBuffer<T>(Buffer<T> buffer, ImageFormat format,
            int width = 0, int rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return ForBuffer(buffer, format, (ulong) width, (ulong) rowPitch, deviceFlags, hostFlags);
        }

        public static unsafe Image1D<T> ForBuffer<T>(Buffer<T> buffer, ImageFormat format,
            ulong width = 0, ulong rowPitch = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            if (rowPitch == 0)
                rowPitch = (ulong) buffer.Length * (ulong) sizeof(T);

            if (width == 0)
                width = rowPitch / (ulong) format.ElementSize;

            deviceFlags &= buffer.DeviceFlags;
            hostFlags &= buffer.HostFlags;

            var lib = buffer.Library;
            var context = buffer.Context;

            var flags = deviceFlags.ToDeviceNativeFlags() | hostFlags.ToHostNativeFlags();
            var description = ImageDescription.Create1DBuffer(width, rowPitch, buffer);

            ErrorCode errorCode;
            var id = lib.clCreateImageUnsafe(context, flags, &format, &description, null, &errorCode);
            errorCode.HandleError();

            return FromId<T>(lib, id);
        }

        #endregion
    }
}
