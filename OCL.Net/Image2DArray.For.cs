using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image2DArray
    {
        #region For

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image2DArray<T> For<T>(Context context, MemoryManager<T> memoryManager,
            ImageChannelOrder channelOrder, bool normalized,
            int width, int height, int rowPitch = 0, int slicePitch = 0, int length = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return For(context, memoryManager, channelOrder, normalized,
                (ulong) width, (ulong) height, (ulong) rowPitch, (ulong) slicePitch, (ulong) length,
                deviceFlags, hostFlags);
        }

        public static Image2DArray<T> For<T>(Context context, MemoryManager<T> memoryManager,
            ImageChannelOrder channelOrder, bool normalized,
            ulong width, ulong height, ulong rowPitch = 0, ulong slicePitch = 0, ulong length = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            if (memoryManager == null)
                throw new ArgumentNullException(nameof(memoryManager));

            return ForMemory(context, memoryManager.Memory, memoryManager, channelOrder, normalized,
                width, height, rowPitch, slicePitch, length, deviceFlags, hostFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image2DArray<T> For<T>(Context context, MemoryManager<T> memoryManager,
            ImageFormat format,
            int width, int height, int rowPitch = 0, int slicePitch = 0, int length = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return For(context, memoryManager, format,
                (ulong) width, (ulong) height, (ulong) rowPitch, (ulong) slicePitch, (ulong) length,
                deviceFlags, hostFlags);
        }

        public static Image2DArray<T> For<T>(Context context, MemoryManager<T> memoryManager,
            ImageFormat format,
            ulong width, ulong height, ulong rowPitch = 0, ulong slicePitch = 0, ulong length = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            if (memoryManager == null)
                throw new ArgumentNullException(nameof(memoryManager));

            return ForMemory(context, memoryManager.Memory, memoryManager, format,
                width, height, rowPitch, slicePitch, length, deviceFlags, hostFlags);
        }

        #endregion

        #region ForMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image2DArray<T> ForMemory<T>(Context context,  Memory<T> memory,
            ImageChannelOrder channelOrder, bool normalized,
            int width, int height, int rowPitch = 0, int slicePitch = 0, int length = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return ForMemory(context, memory, channelOrder, normalized,
                (ulong) width, (ulong) height, (ulong) rowPitch, (ulong) slicePitch, (ulong) length,
                deviceFlags, hostFlags);
        }

        public static Image2DArray<T> ForMemory<T>(Context context, Memory<T> memory,
            ImageChannelOrder channelOrder, bool normalized,
            ulong width, ulong height, ulong rowPitch = 0, ulong slicePitch = 0, ulong length = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return ForMemory(context, memory, null, channelOrder, normalized,
                width, height, rowPitch, slicePitch, length, deviceFlags, hostFlags);
        }

        private static Image2DArray<T> ForMemory<T>(Context context, Memory<T> memory, IDisposable disposable,
            ImageChannelOrder channelOrder, bool normalized,
            ulong width, ulong height, ulong rowPitch = 0, ulong slicePitch = 0, ulong length = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            var format = new ImageFormatBuilder()
                .WithDataType(typeof(T))
                .WithNormalized(normalized)
                .WithChannelOrder(channelOrder)
                .Build();

            return ForMemory(context, memory, disposable, format,
                width, height, length, rowPitch, slicePitch, deviceFlags, hostFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Image2DArray<T> ForMemory<T>(Context context, Memory<T> memory, ImageFormat format,
            int width, int height, int rowPitch = 0, int slicePitch = 0, int length = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return ForMemory(context, memory, format,
                (ulong) width, (ulong) height, (ulong) rowPitch, (ulong) slicePitch, (ulong) length,
                deviceFlags, hostFlags);
        }

        public static Image2DArray<T> ForMemory<T>(Context context, Memory<T> memory, ImageFormat format,
            ulong width, ulong height, ulong rowPitch = 0, ulong slicePitch = 0, ulong length = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            return ForMemory(context, memory, null, format,
                width, height, rowPitch, slicePitch, length, deviceFlags, hostFlags);
        }

        private static unsafe Image2DArray<T> ForMemory<T>(Context context, Memory<T> memory, IDisposable disposable,
            ImageFormat format,
            ulong width, ulong height, ulong rowPitch = 0, ulong slicePitch = 0, ulong length = 0,
            MemoryFlags deviceFlags = DefaultFlags, MemoryFlags hostFlags = DefaultFlags)
            where T : unmanaged
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (rowPitch == 0)
                rowPitch = width * (ulong) format.ElementSize;

            if (slicePitch == 0)
                slicePitch = height * rowPitch;

            if (length == 0)
                length = (ulong) memory.Length * (ulong) sizeof(T) / slicePitch;

            if (slicePitch * length > (ulong) memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Memory capacity is too small");

            var lib = context.Library;
            var flags = deviceFlags.ToDeviceNativeFlags() | hostFlags.ToHostNativeFlags() | MemFlags.MemUseHostPtr;
            var description = ImageDescription.Create2DArray(width, height, rowPitch, slicePitch, length);

            var handle = memory.Pin();

            ErrorCode errorCode;
            var id = lib.clCreateImageUnsafe(context, flags, &format, &description, handle.Pointer, &errorCode);
            errorCode.HandleError();

            return FromId<T>(lib, id, handle, disposable);
        }

        #endregion
    }
}
