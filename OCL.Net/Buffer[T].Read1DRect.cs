using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Read1DRect

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read1DRect<TMemory>(CommandQueue queue, TMemory memory,
            int bufferX, int width, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read1DRect(queue, memory, (ulong) bufferX, 0, (ulong) width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read1DRect<TMemory>(CommandQueue queue, TMemory memory,
            ulong bufferX, ulong width, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read1DRect(queue, memory, bufferX, 0, width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read1DRect<TMemory>(CommandQueue queue, TMemory memory,
            int bufferX, int hostX, int width, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read1DRect(queue, memory, (ulong) bufferX, (ulong) hostX, (ulong) width, waitList);
        }

        public unsafe AutoDisposedEvent<TMemory> Read1DRect<TMemory>(CommandQueue queue, TMemory memory,
            ulong bufferX, ulong hostX, ulong width, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            if (width > (ulong) memory.Memory.Length)
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot write over memory capacity");

            var handle = memory.Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer, bufferX, 0, 0, hostX, 0, 0, width, 1, 1,
                0, 0, 0, 0, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read1DRectIntoMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Read1DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            int bufferX, int width, params Event[] waitList)
        {
            return Read1DRectIntoMemory(queue, memory, (ulong) bufferX, 0, (ulong) width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Read1DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong width, params Event[] waitList)
        {
            return Read1DRectIntoMemory(queue, memory, bufferX, 0, width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Read1DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            int bufferX, int hostX, int width, params Event[] waitList)
        {
            return Read1DRectIntoMemory(queue, memory, (ulong) bufferX, (ulong) hostX, (ulong) width, waitList);
        }

        public unsafe AutoDisposedEvent<Memory<T>> Read1DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong hostX, ulong width, params Event[] waitList)
        {
            if (width > (ulong) memory.Length)
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot write over memory capacity");

            var handle = memory.Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer, bufferX, 0, 0, hostX, 0, 0, width, 1, 1,
                0, 0, 0, 0, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read1DRectArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> Read1DRectArray(CommandQueue queue,
            int bufferX, int width, params Event[] waitList)
        {
            return Read1DRectArray(queue, (ulong) bufferX, (ulong) width, waitList);
        }

        public unsafe AutoDisposedEvent<T[]> Read1DRectArray(CommandQueue queue,
            ulong bufferX, ulong width, params Event[] waitList)
        {
            var array = new T[(long) width];
            var handle = new Memory<T>(array).Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer, bufferX, 0, 0, 0, 0, 0, width, 1, 1,
                0, 0, 0, 0, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, array);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read1DRectIntoArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> Read1DRectIntoArray(CommandQueue queue, T[] array,
            int bufferX, int width, params Event[] waitList)
        {
            return Read1DRectIntoArray(queue, array, (ulong) bufferX, 0, (ulong) width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> Read1DRectIntoArray(CommandQueue queue, T[] array,
            ulong bufferX, ulong width, params Event[] waitList)
        {
            return Read1DRectIntoArray(queue, array, bufferX, 0, width, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> Read1DRectIntoArray(CommandQueue queue, T[] array,
            int bufferX, int hostX, int width, params Event[] waitList)
        {
            return Read1DRectIntoArray(queue, array, (ulong) bufferX, (ulong) hostX, (ulong) width, waitList);
        }

        public unsafe AutoDisposedEvent<T[]> Read1DRectIntoArray(CommandQueue queue, T[] array,
            ulong bufferX, ulong hostX, ulong width, params Event[] waitList)
        {
            if (width > (ulong) array.LongLength)
                throw new ArgumentOutOfRangeException(nameof(array), "Cannot write over array capacity");

            var handle = new Memory<T>(array).Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer, bufferX, 0, 0, hostX, 0, 0,
                width, 1, 1, 0, 0, 0, 0, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, array);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read1DRectNativeMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<NativeMemory<T>> Read1DRectNativeMemory(CommandQueue queue,
            int bufferX, int width, params Event[] waitList)
        {
            return Read1DRectNativeMemory(queue, (ulong) bufferX, (ulong) width, waitList);
        }

        public unsafe AutoDisposedEvent<NativeMemory<T>> Read1DRectNativeMemory(CommandQueue queue,
            ulong bufferX, ulong width, params Event[] waitList)
        {
            var memory = new NativeMemory<T>((long) width);
            var handle = memory.Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer, bufferX, 0, 0, 0, 0, 0,
                width, 1, 1, 0, 0, 0, 0, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion
    }
}
