using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Kernel
    {
        public unsafe AutoDisposedEvent Enqueue(CommandQueue queue, params Event[] waitList)
        {
            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* eventsPtr = eventIds)
            {
                Library.clEnqueueTaskUnsafe(queue, Id, eventCount, eventsPtr, &eventId).HandleError();
            }

            return AutoDisposedEvent.FromId(Library, eventId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Enqueue(CommandQueue queue, int size, params Event[] waitList)
        {
            return Enqueue(queue, 0, (ulong) size, (ulong) size, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Enqueue(CommandQueue queue, ulong size, params Event[] waitList)
        {
            return Enqueue(queue, 0, size, size, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Enqueue(CommandQueue queue, int globalSize, int localSize, params Event[] waitList)
        {
            return Enqueue(queue, 0, (ulong) globalSize, (ulong) localSize, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Enqueue(CommandQueue queue, ulong globalSize, ulong localSize, params Event[] waitList)
        {
            return Enqueue(queue, 0, globalSize, localSize, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Enqueue(CommandQueue queue,
            int globalOffset, int globalSize, int localSize, params Event[] waitList)
        {
            return Enqueue(queue, (ulong) globalOffset, (ulong) globalSize, (ulong) localSize, waitList);
        }

        public unsafe AutoDisposedEvent Enqueue(CommandQueue queue,
            ulong globalOffset, ulong globalSize, ulong localSize, params Event[] waitList)
        {
            var globalOffsetValue = (UIntPtr) globalOffset;
            var globalSizeValue = (UIntPtr) globalSize;
            var localSizeValue = (UIntPtr) localSize;

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (EventId* eventsPtr = eventIds)
            {
                Library.clEnqueueNDRangeKernelUnsafe(queue, Id, 1,
                    &globalOffsetValue, &globalSizeValue, &localSizeValue,
                    eventCount, eventsPtr, &eventId).HandleError();
            }

            return AutoDisposedEvent.FromId(Library, eventId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Enqueue(CommandQueue queue, IEnumerable<int> sizes, params Event[] waitList)
        {
            return Enqueue(queue, null, sizes, null, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Enqueue(CommandQueue queue, IEnumerable<ulong> sizes, params Event[] waitList)
        {
            return Enqueue(queue, null, sizes, null, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Enqueue(CommandQueue queue,
            IEnumerable<int> globalSizes, IEnumerable<int> localSizes, params Event[] waitList)
        {
            return Enqueue(queue, null, globalSizes, localSizes, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent Enqueue(CommandQueue queue,
            IEnumerable<ulong> globalSizes, IEnumerable<ulong> localSizes, params Event[] waitList)
        {
            return Enqueue(queue, null, globalSizes, localSizes, waitList);
        }

        public AutoDisposedEvent Enqueue(CommandQueue queue,
            IEnumerable<int> globalOffsets, IEnumerable<int> globalSizes, IEnumerable<int> localSizes,
            params Event[] waitList)
        {
            return Enqueue(queue,
                globalOffsets?.Select(ToULong), globalSizes?.Select(ToULong), localSizes?.Select(ToULong), waitList);
        }

        public unsafe AutoDisposedEvent Enqueue(CommandQueue queue,
            IEnumerable<ulong> globalOffsets, IEnumerable<ulong> globalSizes, IEnumerable<ulong> localSizes,
            params Event[] waitList)
        {
            if (globalSizes == null)
                throw new ArgumentNullException(nameof(globalSizes));

            if (!(globalSizes is IList<ulong> globalSizesList))
                globalSizesList = globalSizes.ToList();

            if (!(globalOffsets is IList<ulong> globalOffsetsList))
                globalOffsetsList = globalOffsets?.ToList();

            if (!(localSizes is IList<ulong> localSizesList))
                localSizesList = localSizes?.ToList();

            var dimensions = globalSizesList.Count;

            if (!CompareDimensions(dimensions, globalOffsetsList) || !CompareDimensions(dimensions, localSizesList))
                throw new ArgumentException("Sizes and offsets need to have the same number of values");

            var globalOffsetValues = new UIntPtr[dimensions];
            var globalSizeValues = new UIntPtr[dimensions];
            var localSizeValues = new UIntPtr[dimensions];

            for (var i = 0; i < dimensions; i++)
            {
                if (globalOffsetsList != null)
                    globalOffsetValues[i] = (UIntPtr) globalOffsetsList[i];

                globalSizeValues[i] = (UIntPtr) globalSizesList[i];

                if (localSizesList == null)
                    localSizeValues[i] = globalSizeValues[i];
                else
                    localSizeValues[i] = (UIntPtr) localSizesList[i];
            }

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            EventId eventId;

            fixed (UIntPtr* globalOffsetsPtr = globalOffsetValues)
            fixed (UIntPtr* globalSizesPtr = globalSizeValues)
            fixed (UIntPtr* localSizesPtr = localSizeValues)
            fixed (EventId* eventsPtr = eventIds)
            {
                Library.clEnqueueNDRangeKernelUnsafe(queue, Id,
                    (uint) dimensions, globalOffsetsPtr, globalSizesPtr, localSizesPtr,
                    eventCount, eventsPtr, &eventId).HandleError();
            }

            return AutoDisposedEvent.FromId(Library, eventId);
        }

        private static ulong ToULong(int value) => (ulong) value;

        private static bool CompareDimensions<T>(int dimensions, ICollection<T> items)
        {
            return items == null || items.Count == dimensions;
        }
    }
}
