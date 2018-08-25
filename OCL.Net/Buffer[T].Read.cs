using System;
using System.Buffers;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        #region Read

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read<TMemory>(CommandQueue queue, TMemory memory, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read(queue, memory, 0, LongLength, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read<TMemory>(CommandQueue queue, TMemory memory,
            int offset, int length, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read(queue, memory, (ulong) offset, (ulong) length, waitList);
        }

        public unsafe AutoDisposedEvent<TMemory> Read<TMemory>(CommandQueue queue, TMemory memory,
            ulong offset, ulong length, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            if (length > (ulong) memory.Memory.Length)
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot write over memory capacity");

            var handle = memory.Pin();

            var eventId = ReadInto(queue, (T*) handle.Pointer, offset, length, waitList);
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
            return ReadIntoMemory(queue, memory, 0, LongLength, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> ReadIntoMemory(CommandQueue queue, Memory<T> memory,
            int offset, int length, params Event[] waitList)
        {
            return ReadIntoMemory(queue, memory, (ulong) offset, (ulong) length,  waitList);
        }

        public unsafe AutoDisposedEvent<Memory<T>> ReadIntoMemory(CommandQueue queue, Memory<T> memory,
            ulong offset, ulong length, params Event[] waitList)
        {
            if (length > (ulong) memory.Length)
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot write over memory capacity");

            var handle = memory.Pin();

            var eventId = ReadInto(queue, (T*) handle.Pointer, offset, length, waitList);
            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region ReadArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadArray(CommandQueue queue, params Event[] waitList)
        {
            return ReadArray(queue, 0, LongLength, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadArray(CommandQueue queue, int offset, int length, params Event[] waitList)
        {
            return ReadArray(queue, (ulong) offset, (ulong) length, waitList);
        }

        public unsafe AutoDisposedEvent<T[]> ReadArray(CommandQueue queue,
            ulong offset, ulong length, params Event[] waitList)
        {
            var array = new T[(long) length];
            var handle = new Memory<T>(array).Pin();

            var eventId = ReadInto(queue, (T*) handle.Pointer, offset, length, waitList);
            var @event = AutoDisposedEvent.FromId(Library, eventId, array);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region ReadIntoArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadIntoArray(CommandQueue queue, T[] array, params Event[] waitList)
        {
            return ReadIntoArray(queue, array, 0, LongLength, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> ReadIntoArray(CommandQueue queue, T[] array,
            int offset, int length, params Event[] waitList)
        {
            return ReadIntoArray(queue, array, (ulong) offset, (ulong) length, waitList);
        }

        public unsafe AutoDisposedEvent<T[]> ReadIntoArray(CommandQueue queue, T[] array,
            ulong offset, ulong length, params Event[] waitList)
        {
            if (length > (ulong) array.LongLength)
                throw new ArgumentOutOfRangeException(nameof(array), "Cannot write over array capacity");

            var handle = new Memory<T>(array).Pin();

            var eventId = ReadInto(queue, (T*) handle.Pointer, offset, length, waitList);
            var @event = AutoDisposedEvent.FromId(Library, eventId, array);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region ReadNativeMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<NativeMemory<T>> ReadNativeMemory(CommandQueue queue, params Event[] waitList)
        {
            return ReadNativeMemory(queue, 0, LongLength, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<NativeMemory<T>> ReadNativeMemory(CommandQueue queue,
            int offset, int length, params Event[] waitList)
        {
            return ReadNativeMemory(queue, (ulong) offset, (ulong) length, waitList);
        }

        public unsafe AutoDisposedEvent<NativeMemory<T>> ReadNativeMemory(CommandQueue queue,
            ulong offset, ulong length, params Event[] waitList)
        {
            var memory = new NativeMemory<T>((long) length);
            var handle = memory.Pin();

            var eventId = ReadInto(queue, (T*) handle.Pointer, offset, length, waitList);
            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region ReadInto

        internal unsafe EventId ReadInto(CommandQueue queue, T* ptr,
            ulong offset, ulong length, params Event[] waitList)
        {
            var byteOffset = (UIntPtr) (offset * (ulong) sizeof(T));
            var size = (UIntPtr) (length * (ulong) sizeof(T));

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* idsPtr = eventIds)
            {
                Library.clEnqueueReadBufferUnsafe(queue, Id, false, byteOffset, size, ptr,
                    eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }

        #endregion
    }
}
