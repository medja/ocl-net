using System;
using System.Buffers;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Image2DArray<T>
    {
        #region Read

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read<TMemory>(CommandQueue queue, TMemory memory, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read(queue, memory, 0, 0, 0, Width, Height, Length, 0, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read<TMemory>(CommandQueue queue, TMemory memory, int index, int length = 0,
            params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read(queue, memory, (ulong) index, 0, 0, Width, Height, (ulong) length, 0, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read<TMemory>(CommandQueue queue, TMemory memory,
            ulong index, ulong length = 0, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read(queue, memory, index, 0, 0, Width, Height, length, 0, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read<TMemory>(CommandQueue queue, TMemory memory,
            int index, int x, int y, int width, int height, int length = 1,
            int rowPitch = 0, int slicePitch = 0, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read(queue, memory, (ulong) index, (ulong) x, (ulong) y,
                (ulong) width, (ulong) height, (ulong) length, (ulong) rowPitch, (ulong) slicePitch, waitList);
        }

        public unsafe AutoDisposedEvent<TMemory> Read<TMemory>(CommandQueue queue, TMemory memory,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length = 1,
            ulong rowPitch = 0, ulong slicePitch = 0, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            if (rowPitch == 0)
                rowPitch = width * (ulong) Format.ElementSize;

            if (slicePitch == 0)
                slicePitch = height * rowPitch;

            if (slicePitch * length > (ulong) memory.Memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot write over memory capacity");

            var handle = memory.Pin();

            var eventId = ReadInto(queue, (T*) handle.Pointer,
                index, x, y, width, height, length, rowPitch, slicePitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region ReadIntoMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> ReadIntoMemory(CommandQueue queue, Memory<T> memory,
            params Event[] waitList)
        {
            return ReadIntoMemory(queue, memory, 0, 0, 0, Width, Height, Length, 0, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> ReadIntoMemory(CommandQueue queue, Memory<T> memory,
            int index, int length = 1, params Event[] waitList)
        {
            return ReadIntoMemory(queue, memory, (ulong) index, 0, 0, Width, Height, (ulong) length, 0, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> ReadIntoMemory(CommandQueue queue, Memory<T> memory,
            ulong index, ulong length = 1, params Event[] waitList)
        {
            return ReadIntoMemory(queue, memory, index, 0, 0, Width, Height, length, 0, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> ReadIntoMemory(CommandQueue queue, Memory<T> memory,
            int index, int x, int y, int width, int height, int length = 1,
            int rowPitch = 0, int slicePitch = 0, params Event[] waitList)
        {
            return ReadIntoMemory(queue, memory, (ulong) index, (ulong) x, (ulong) y,
                (ulong) width, (ulong) height, (ulong) length, (ulong) rowPitch, (ulong) slicePitch, waitList);
        }

        public unsafe AutoDisposedEvent<Memory<T>> ReadIntoMemory(CommandQueue queue, Memory<T> memory,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length = 1,
            ulong rowPitch = 0, ulong slicePitch = 0, params Event[] waitList)
        {
            if (rowPitch == 0)
                rowPitch = width * (ulong) Format.ElementSize;

            if (slicePitch == 0)
                slicePitch = height * rowPitch;

            if (slicePitch * length > (ulong) memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot write over memory capacity");

            var handle = memory.Pin();

            var eventId = ReadInto(queue, (T*) handle.Pointer,
                index, x, y, width, height, length, rowPitch, slicePitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region ReadArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadArray(CommandQueue queue, params Event[] waitList)
        {
            return ReadArray(queue, 0, 0, 0, Width, Height, Length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadArray(CommandQueue queue, int index, int length = 1, params Event[] waitList)
        {
            return ReadArray(queue, (ulong) index, 0, 0, Width, Height, (ulong) length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadArray(CommandQueue queue, ulong index, ulong length = 1,
            params Event[] waitList)
        {
            return ReadArray(queue, index, 0, 0, Width, Height, length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadArray(CommandQueue queue,
            int index, int x, int y, int width, int height, int length = 1, params Event[] waitList)
        {
            return ReadArray(queue,
                (ulong) index, (ulong) x, (ulong) y, (ulong) width, (ulong) height, (ulong) length, waitList);
        }

        public unsafe AutoDisposedEvent<T[]> ReadArray(CommandQueue queue,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length = 1, params Event[] waitList)
        {
            var rowPitch = width * (ulong) Format.ElementSize;
            var slicePitch = height * rowPitch;

            var array = new T[(slicePitch * length - 1) / (ulong) sizeof(T) + 1];
            var handle = new Memory<T>(array).Pin();

            var eventId = ReadInto(queue, (T*) handle.Pointer,
                index, x, y, width, height, length, rowPitch, slicePitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, array);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region ReadIntoArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadIntoArray(CommandQueue queue, T[] array, params Event[] waitList)
        {
            return ReadIntoArray(queue, array, 0, 0, 0, Width, Height, Length, 0, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadIntoArray(CommandQueue queue, T[] array, int index, int length = 1,
            params Event[] waitList)
        {
            return ReadIntoArray(queue, array, (ulong) index, 0, 0, Width, Height, (ulong) length, 0, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadIntoArray(CommandQueue queue, T[] array, ulong index, ulong length = 1,
            params Event[] waitList)
        {
            return ReadIntoArray(queue, array, index, 0, 0, Width, Height, length, 0, 0, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadIntoArray(CommandQueue queue, T[] array,
            int index, int x, int y, int width, int height, int length = 1,
            int rowPitch = 0, int slicePitch = 0, params Event[] waitList)
        {
            return ReadIntoArray(queue, array, (ulong) index, (ulong) x, (ulong) y,
                (ulong) width, (ulong) height, (ulong) length, (ulong) rowPitch, (ulong) slicePitch, waitList);
        }

        public unsafe AutoDisposedEvent<T[]> ReadIntoArray(CommandQueue queue, T[] array,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length = 1,
            ulong rowPitch = 0, ulong slicePitch = 0, params Event[] waitList)
        {
            if (rowPitch == 0)
                rowPitch = width * (ulong) Format.ElementSize;

            if (slicePitch == 0)
                slicePitch = height * rowPitch;

            if (slicePitch * length > (ulong) array.LongLength * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(array), "Cannot write over array capacity");

            var handle = new Memory<T>(array).Pin();

            var eventId = ReadInto(queue, (T*) handle.Pointer,
                index, x, y, width, height, length, rowPitch, slicePitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, array);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region ReadNativeMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<NativeMemory<T>> ReadNativeMemory(CommandQueue queue, params Event[] waitList)
        {
            return ReadNativeMemory(queue, 0, 0, 0, Width, Height, Length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<NativeMemory<T>> ReadNativeMemory(CommandQueue queue, int index, int length = 1,
            params Event[] waitList)
        {
            return ReadNativeMemory(queue, (ulong) index, 0, 0, Width, Height, (ulong) length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<NativeMemory<T>> ReadNativeMemory(CommandQueue queue, ulong index, ulong length = 1,
            params Event[] waitList)
        {
            return ReadNativeMemory(queue, index, 0, 0, Width, Height, length, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<NativeMemory<T>> ReadNativeMemory(CommandQueue queue,
            int index, int x, int y, int width, int height, int length = 1, params Event[] waitList)
        {
            return ReadNativeMemory(queue,
                (ulong) index, (ulong) x, (ulong) y, (ulong) width, (ulong) height, (ulong) length, waitList);
        }

        public unsafe AutoDisposedEvent<NativeMemory<T>> ReadNativeMemory(CommandQueue queue,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length = 1, params Event[] waitList)
        {
            var rowPitch = width * (ulong) Format.ElementSize;
            var slicePitch = height * rowPitch;

            var memory = new NativeMemory<T>((long) ((slicePitch * length - 1) / (ulong) sizeof(T) + 1));
            var handle = memory.Pin();

            var eventId = ReadInto(queue, (T*) handle.Pointer,
                index, x, y, width, height, length, rowPitch, slicePitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region ReadInto

        internal unsafe EventId ReadInto(CommandQueue queue, T* ptr,
            ulong index, ulong x, ulong y, ulong width, ulong height, ulong length,
            ulong rowPitch, ulong slicePitch, params Event[] waitList)
        {
            var origin = stackalloc UIntPtr[3];

            origin[0] = (UIntPtr) x;
            origin[1] = (UIntPtr) y;
            origin[2] = (UIntPtr) index;

            var region = stackalloc UIntPtr[3];

            region[0] = (UIntPtr) width;
            region[1] = (UIntPtr) height;
            region[2] = (UIntPtr) length;

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* idsPtr = eventIds)
            {
                Library.clEnqueueReadImageUnsafe(queue, Id, false, origin, region,
                    (UIntPtr) rowPitch, (UIntPtr) slicePitch, ptr, eventCount, idsPtr, &eventId);
            }

            return eventId;
        }

        #endregion
    }
}
