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
        #region Read3DRect

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read3DRect<TMemory>(CommandQueue queue, TMemory memory,
            int bufferX, int bufferY, int bufferZ, int width, int height, int depth,
            int bufferRowPitch, int bufferSlicePitch, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read3DRect(queue, memory,
                (ulong) bufferX, (ulong) bufferY, (ulong) bufferZ, (ulong) width, (ulong) height, (ulong) depth,
                (ulong) bufferRowPitch, (ulong) bufferSlicePitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe AutoDisposedEvent<TMemory> Read3DRect<TMemory>(CommandQueue queue, TMemory memory,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            var hostRowPitch = width * (ulong) sizeof(T);
            var hostSlicePitch = height * hostRowPitch;

            return Read3DRect(queue, memory, bufferX, bufferY, bufferZ, 0, 0, 0, width, height, depth,
                bufferRowPitch, bufferSlicePitch, hostRowPitch, hostSlicePitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<TMemory> Read3DRect<TMemory>(CommandQueue queue, TMemory memory,
            int bufferX, int bufferY, int bufferZ, int hostX, int hostY, int hostZ,
            int width, int height, int depth,
            int bufferRowPitch, int bufferSlicePitch, int hostRowPitch, int hostSlicePitch, params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            return Read3DRect(queue, memory,
                (ulong) bufferX, (ulong) bufferY, (ulong) bufferZ, (ulong) hostX, (ulong) hostY, (ulong) hostZ,
                (ulong) width, (ulong) height, (ulong) depth,
                (ulong) bufferRowPitch, (ulong) bufferSlicePitch, (ulong) hostRowPitch, (ulong) hostSlicePitch,
                waitList);
        }

        public unsafe AutoDisposedEvent<TMemory> Read3DRect<TMemory>(CommandQueue queue, TMemory memory,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong hostX, ulong hostY, ulong hostZ,
            ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, ulong hostRowPitch, ulong hostSlicePitch,
            params Event[] waitList)
            where TMemory : MemoryManager<T>
        {
            if (hostSlicePitch * depth > (ulong) memory.Memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot write over memory capacity");

            var handle = memory.Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer,
                bufferX, bufferY, bufferZ, hostX, hostY, hostZ, width, height, depth,
                bufferRowPitch, bufferSlicePitch, hostRowPitch, hostSlicePitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read3DRectIntoMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Read3DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            int bufferX, int bufferY, int bufferZ, int width, int height, int depth,
            int bufferRowPitch, int bufferSlicePitch, params Event[] waitList)
        {
            return Read3DRectIntoMemory(queue, memory,
                (ulong) bufferX, (ulong) bufferY, (ulong) bufferZ, (ulong) width, (ulong) height, (ulong) depth,
                (ulong) bufferRowPitch, (ulong) bufferSlicePitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe AutoDisposedEvent<Memory<T>> Read3DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, params Event[] waitList)
        {
            var hostRowPitch = width * (ulong) sizeof(T);
            var hostSlicePitch = height * hostRowPitch;

            return Read3DRectIntoMemory(queue, memory, bufferX, bufferY, bufferZ, 0, 0, 0, width, height, depth,
                bufferRowPitch, bufferSlicePitch, hostRowPitch, hostSlicePitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<Memory<T>> Read3DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            int bufferX, int bufferY, int bufferZ, int hostX, int hostY, int hostZ,
            int width, int height, int depth,
            int bufferRowPitch, int bufferSlicePitch, int hostRowPitch, int hostSlicePitch, params Event[] waitList)
        {
            return Read3DRectIntoMemory(queue, memory,
                (ulong) bufferX, (ulong) bufferY, (ulong) bufferZ, (ulong) hostX, (ulong) hostY, (ulong) hostZ,
                (ulong) width, (ulong) height, (ulong) depth,
                (ulong) bufferRowPitch, (ulong) bufferSlicePitch, (ulong) hostRowPitch, (ulong) hostSlicePitch,
                waitList);
        }

        public unsafe AutoDisposedEvent<Memory<T>> Read3DRectIntoMemory(CommandQueue queue, Memory<T> memory,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong hostX, ulong hostY, ulong hostZ,
            ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, ulong hostRowPitch, ulong hostSlicePitch,
            params Event[] waitList)
        {
            if (hostSlicePitch * depth > (ulong) memory.Length * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(memory), "Cannot write over memory capacity");

            var handle = memory.Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer,
                bufferX, bufferY, bufferZ, hostX, hostY, hostZ, width, height, depth,
                bufferRowPitch, bufferSlicePitch, hostRowPitch, hostSlicePitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read3DRectArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> Read3DRectArray(CommandQueue queue,
            int bufferX, int bufferY, int bufferZ, int width, int height, int depth,
            int bufferRowPitch, int bufferSlicePitch, params Event[] waitList)
        {
            return Read3DRectArray(queue, (ulong) bufferX, (ulong) bufferY, (ulong) bufferZ,
                (ulong) width, (ulong) height, (ulong) depth,
                (ulong) bufferRowPitch, (ulong) bufferSlicePitch, waitList);
        }

        public unsafe AutoDisposedEvent<T[]> Read3DRectArray(CommandQueue queue,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, params Event[] waitList)
        {
            var hostRowPitch = width * (ulong) sizeof(T);
            var hostSlicePitch = height * hostRowPitch;

            var array = new T[(long) width * (long) height * (long) depth];
            var handle = new Memory<T>(array).Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer,
                bufferX, bufferY, bufferZ, 0, 0, 0, width, height, depth,
                bufferRowPitch, bufferSlicePitch, hostRowPitch, hostSlicePitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, array);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read3DRectIntoArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> Read3DRectIntoArray(CommandQueue queue, T[] array,
            int bufferX, int bufferY, int bufferZ, int width, int height, int depth,
            int bufferRowPitch, int bufferSlicePitch, params Event[] waitList)
        {
            return Read3DRectIntoArray(queue, array,
                (ulong) bufferX, (ulong) bufferY, (ulong) bufferZ, 0, 0, 0,
                (ulong) width, (ulong) height, (ulong) depth,
                (ulong) bufferRowPitch, (ulong) bufferSlicePitch, (ulong) width, (ulong) height, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe AutoDisposedEvent<T[]> Read3DRectIntoArray(CommandQueue queue, T[] array,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, params Event[] waitList)
        {
            var hostRowPitch = width * (ulong) sizeof(T);
            var hostSlicePitch = height * hostRowPitch;

            return Read3DRectIntoArray(queue, array, bufferX, bufferY, bufferZ, 0, 0, 0, width, height, depth,
                bufferRowPitch, bufferSlicePitch, hostRowPitch, hostSlicePitch, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<T[]> Read3DRectIntoArray(CommandQueue queue, T[] array,
            int bufferX, int bufferY, int bufferZ, int hostX, int hostY, int hostZ,
            int width, int height, int depth,
            int bufferRowPitch, int bufferSlicePitch, int hostRowPitch, int hostSlicePitch, params Event[] waitList)
        {
            return Read3DRectIntoArray(queue, array,
                (ulong) bufferX, (ulong) bufferY, (ulong) bufferZ, (ulong) hostX, (ulong) hostY, (ulong) hostZ,
                (ulong) width, (ulong) height, (ulong) depth,
                (ulong) bufferRowPitch, (ulong) bufferSlicePitch,
                (ulong) hostRowPitch, (ulong) hostSlicePitch, waitList);
        }

        public unsafe AutoDisposedEvent<T[]> Read3DRectIntoArray(CommandQueue queue, T[] array,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong hostX, ulong hostY, ulong hostZ,
            ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, ulong hostRowPitch, ulong hostSlicePitch,
            params Event[] waitList)
        {
            if (hostSlicePitch * depth > (ulong) array.LongLength * (ulong) sizeof(T))
                throw new ArgumentOutOfRangeException(nameof(array), "Cannot write over array capacity");

            var handle = new Memory<T>(array).Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer,
                bufferX, bufferY, bufferZ, hostX, hostY, hostZ, width, height, depth,
                bufferRowPitch, bufferSlicePitch, hostRowPitch, hostSlicePitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, array);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read3DRectNativeMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent<NativeMemory<T>> Read3DRectNativeMemory(CommandQueue queue,
            int bufferX, int bufferY, int bufferZ, int width, int height, int depth,
            int bufferRowPitch, int bufferSlicePitch, params Event[] waitList)
        {
            return Read3DRectNativeMemory(queue, (ulong) bufferX, (ulong) bufferY, (ulong) bufferZ,
                (ulong) width, (ulong) height, (ulong) depth,
                (ulong) bufferRowPitch, (ulong) bufferSlicePitch, waitList);
        }

        public unsafe AutoDisposedEvent<NativeMemory<T>> Read3DRectNativeMemory(CommandQueue queue,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, params Event[] waitList)
        {
            var hostRowPitch = width * (ulong) sizeof(T);
            var hostSlicePitch = height * hostRowPitch;

            var memory = new NativeMemory<T>((long) width * (long) height * (long) depth);
            var handle = memory.Pin();

            var eventId = Read3DRectInto(queue, (T*) handle.Pointer,
                bufferX, bufferY, bufferZ, 0, 0, 0, width, height, depth,
                bufferRowPitch, bufferSlicePitch, hostRowPitch, hostSlicePitch, waitList);

            var @event = AutoDisposedEvent.FromId(Library, eventId, memory);

            @event.OnComplete(handle.Dispose);

            return @event;
        }

        #endregion

        #region Read3DRectInto

        internal unsafe EventId Read3DRectInto(CommandQueue queue, T* ptr,
            ulong bufferX, ulong bufferY, ulong bufferZ, ulong hostX, ulong hostY, ulong hostZ,
            ulong width, ulong height, ulong depth,
            ulong bufferRowPitch, ulong bufferSlicePitch, ulong hostRowPitch, ulong hostSlicePitch,
            params Event[] waitList)
        {
            var bufferOrigin = stackalloc UIntPtr[3];

            bufferOrigin[0] = (UIntPtr) (bufferX * (ulong) sizeof(T));
            bufferOrigin[1] = (UIntPtr) bufferY;
            bufferOrigin[2] = (UIntPtr) bufferZ;

            var hostOrigin = stackalloc UIntPtr[3];

            hostOrigin[0] = (UIntPtr) (hostX * (ulong) sizeof(T));
            hostOrigin[1] = (UIntPtr) hostY;
            hostOrigin[2] = (UIntPtr) hostZ;

            var region = stackalloc UIntPtr[3];

            region[0] = (UIntPtr) (width * (ulong) sizeof(T));
            region[1] = (UIntPtr) height;
            region[2] = (UIntPtr) depth;

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* idsPtr = eventIds)
            {
                Library.clEnqueueReadBufferRectUnsafe(queue, Id, false,
                    bufferOrigin, hostOrigin, region,
                    (UIntPtr) bufferRowPitch, (UIntPtr) bufferSlicePitch,
                    (UIntPtr) hostRowPitch, (UIntPtr) hostSlicePitch,
                    ptr, eventCount, idsPtr, &eventId).HandleError();
            }

            return eventId;
        }

        #endregion
    }
}
