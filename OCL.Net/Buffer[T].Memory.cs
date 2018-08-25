using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Buffer<T>
    {
        public unsafe class Memory : NativeMemoryManager<T>
        {
            private readonly CommandQueueId _queue;
            private readonly MemoryId _memory;
            private readonly IOpenCl _library;
            private AutoDisposedEvent _unmapEvent;

            public Memory(T* ptr, long length, BufferId buffer, CommandQueueId queue, IOpenCl library)
                : this(ptr, length, (MemoryId) buffer, queue, library)
            { }

            protected Memory(T* ptr, long length, MemoryId memory, CommandQueueId queue, IOpenCl library)
                : base(ptr, length)
            {
                _memory = memory;
                _queue = queue;
                _library = library;
            }

            protected override void OnDispose()
            {
                UnmapInternal(_queue, null);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public AutoDisposedEvent Unmap(params Event[] waitList)
            {
                return Unamp(_queue, waitList);
            }

            public AutoDisposedEvent Unmap(CommandQueue queue, params Event[] waitList)
            {
                if (queue == null)
                    throw new ArgumentNullException(nameof(queue));

                return Unamp(queue.Id, waitList);
            }

            private AutoDisposedEvent Unamp(CommandQueueId queue, Event[] waitList)
            {
                if (ReferenceCount > 0)
                    throw new InvalidOperationException("Cannot unmap pinned memory");

                lock (SyncLock)
                {
                    if (ReferenceCount > 0)
                        throw new InvalidOperationException("Cannot unmap pinned memory");

                    return UnmapInternal(queue, waitList);
                }
            }

            [SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Local")]
            private AutoDisposedEvent UnmapInternal(CommandQueueId queue, params Event[] waitList)
            {
                if (_unmapEvent != null)
                    return _unmapEvent;

                var eventIds = waitList?.Select(@event => @event.Id).ToArray();
                var eventCount = (uint) (eventIds?.Length ?? 0);

                EventId eventId;

                fixed (EventId* idsPtr = eventIds)
                {
                    _library.clEnqueueUnmapMemObjectUnsafe(
                        queue, _memory, Pointer, eventCount, idsPtr, &eventId).HandleError();
                }

                return _unmapEvent = AutoDisposedEvent.FromId(_library, eventId);
            }
        }
    }
}
