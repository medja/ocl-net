using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Read2DRect

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read2DRect<TMemory>(CommandQueue queue, TMemory memory,
            int bufferX, int bufferY, int width, int height, int bufferRowPitch, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read2DRect(queue, memory, (ulong) bufferX, (ulong) bufferY,
                (ulong) width, (ulong) height, (ulong) bufferRowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe AutoDisposedEvent<TMemory> Read2DRect<TMemory>(CommandQueue queue, TMemory memory,
            ulong bufferX, ulong bufferY, ulong width, ulong height, ulong bufferRowPitch, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            var hostRowPitch = width * (ulong) sizeof(T);

            return Read2DRect(queue, memory, bufferX, bufferY, 0, 0,
                width, height, bufferRowPitch, hostRowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read2DRect<TMemory>(CommandQueue queue, TMemory memory,
            int bufferX, int bufferY, int hostX, int hostY, int width, int height,
            int bufferRowPitch, int hostRowPitch, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read2DRect(queue, memory, (ulong) bufferX, (ulong) bufferY, (ulong) hostX, (ulong) hostY,
                (ulong) width, (ulong) height, (ulong) bufferRowPitch, (ulong) hostRowPitch, waitList);
        }

        public unsafe AutoDisposedEvent<TMemory> Read2DRect<TMemory>(CommandQueue queue, TMemory memory,
            ulong bufferX, ulong bufferY, ulong hostX, ulong hostY, ulong width, ulong height,
            ulong bufferRowPitch, ulong hostRowPitch, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            if (hostRowPitch * height > (ulong) memory.Memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot write over memory capacity");

            var handle = memory.Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer,
                bufferX, bufferY, 0, hostX, hostY, 0, width, height, 1,
                bufferRowPitch, bufferRowPitch, hostRowPitch, hostRowPitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read2DRectIntoMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Read2DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            int bufferX, int bufferY, int width, int height, int bufferRowPitch, params Event[] waitList)
        {
            return Read2DRectIntoMemory(queue, memory, (ulong) bufferX, (ulong) bufferY,
                (ulong) width, (ulong) height, (ulong) bufferRowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe AutoDisposedEvent<Memory<T>> Read2DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong bufferY, ulong width, ulong height, ulong bufferRowPitch, params Event[] waitList)
        {
            var hostRowPitch = width * (ulong) sizeof(T);

            return Read2DRectIntoMemory(queue, memory, bufferX, bufferY, 0, 0, width, height,
                bufferRowPitch, hostRowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Read2DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            int bufferX, int bufferY, int hostX, int hostY, int width, int height,
            int bufferRowPitch, int hostRowPitch, params Event[] waitList)
        {
            return Read2DRectIntoMemory(queue, memory, (ulong) bufferX, (ulong) bufferY, (ulong) hostX, (ulong) hostY,
                (ulong) width, (ulong) height, (ulong) bufferRowPitch, (ulong) hostRowPitch, waitList);
        }

        public unsafe AutoDisposedEvent<Memory<T>> Read2DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong bufferY, ulong hostX, ulong hostY, ulong width, ulong height,
            ulong bufferRowPitch, ulong hostRowPitch, params Event[] waitList)
        {
            if (hostRowPitch * height > (ulong) memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot write over memory capacity");

            var handle = memory.Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer,
                bufferX, bufferY, 0, hostX, hostY, 0, width, height, 1,
                bufferRowPitch, bufferRowPitch, hostRowPitch, hostRowPitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read2DRectArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> Read2DRectArray(CommandQueue queue,
            int bufferX, int bufferY, int width, int height, int bufferRowPitch, params Event[] waitList)
        {
            return Read2DRectArray(queue, (ulong) bufferX, (ulong) bufferY,
                (ulong) width, (ulong) height, (ulong) bufferRowPitch, waitList);
        }

        public unsafe AutoDisposedEvent<T[]> Read2DRectArray(CommandQueue queue,
            ulong bufferX, ulong bufferY, ulong width, ulong height, ulong bufferRowPitch, params Event[] waitList)
        {
            var hostRowPitch = width * (ulong) sizeof(T);

            var array = new T[(long) width * (long) height];
            var handle = new Memory<T>(array).Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer,
                bufferX, bufferY, 0, 0, 0, 0, width, height, 1,
                bufferRowPitch, bufferRowPitch, hostRowPitch, hostRowPitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, array);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read2DRectIntoArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> Read2DRectIntoArray(CommandQueue queue, T[] array,
            int bufferX, int bufferY, int width, int height, int bufferRowPitch, params Event[] waitList)
        {
            return Read2DRectIntoArray(queue, array, (ulong) bufferX, (ulong) bufferY,
                (ulong) width, (ulong) height, (ulong) bufferRowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe AutoDisposedEvent<T[]> Read2DRectIntoArray(CommandQueue queue, T[] array,
            ulong bufferX, ulong bufferY, ulong width, ulong height, ulong bufferRowPitch, params Event[] waitList)
        {
            var hostRowPitch = width * (ulong) sizeof(T);

            return Read2DRectIntoArray(queue, array, bufferX, bufferY, 0, 0, width, height,
                bufferRowPitch, hostRowPitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> Read2DRectIntoArray(CommandQueue queue, T[] array,
            int bufferX, int bufferY, int hostX, int hostY, int width, int height,
            int bufferRowPitch, int hostRowPitch, params Event[] waitList)
        {
            return Read2DRectIntoArray(queue, array, (ulong) bufferX, (ulong) bufferY, (ulong) hostX, (ulong) hostY,
                (ulong) width, (ulong) height, (ulong) bufferRowPitch, (ulong) hostRowPitch, waitList);
        }

        public unsafe AutoDisposedEvent<T[]> Read2DRectIntoArray(CommandQueue queue, T[] array,
            ulong bufferX, ulong bufferY, ulong hostX, ulong hostY, ulong width, ulong height,
            ulong bufferRowPitch, ulong hostRowPitch, params Event[] waitList)
        {
            if (hostRowPitch * height > (ulong) array.LongLength * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(array), "Cannot write over array capacity");

            var handle = new Memory<T>(array).Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer, bufferX, bufferY, 0, hostX, hostY, 0,
                width, height, 1, bufferRowPitch, bufferRowPitch, hostRowPitch, hostRowPitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, array);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read2DRectNativeMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<NativeMemory<T>> Read2DRectNativeMemory(CommandQueue queue,
            int bufferX, int bufferY, int width, int height, int bufferRowPitch, params Event[] waitList)
        {
            return Read2DRectNativeMemory(queue, (ulong) bufferX, (ulong) bufferY,
                (ulong) width, (ulong) height, (ulong) bufferRowPitch, waitList);
        }

        public unsafe AutoDisposedEvent<NativeMemory<T>> Read2DRectNativeMemory(CommandQueue queue,
            ulong bufferX, ulong bufferY, ulong width, ulong height, ulong bufferRowPitch, params Event[] waitList)
        {
            var hostRowPitch = width * (ulong) sizeof(T);

            var memory = new NativeMemory<T>((long) width * (long) height);
            var handle = memory.Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer, bufferX, bufferY, 0, 0, 0, 0,
                width, height, 1, bufferRowPitch, bufferRowPitch, hostRowPitch, hostRowPitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion
    }
}
