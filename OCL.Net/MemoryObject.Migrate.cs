using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class MemoryObject
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoDisposedEvent MigrateToHost(CommandQueue queue, params Event[] waitList)
        {
            Span<MemoryId> ids = stackalloc MemoryId[1];
            ids[0] = Id;

            return MigrateInternal(queue, MemMigrationFlags.MigrateMemObjectHost, ids, waitList);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public AutoDisposedEvent MigrateToHost(CommandQueue queue, IEnumerable<Event> waitList)
        {
            Span<MemoryId> ids = stackalloc MemoryId[1];
            ids[0] = Id;

            return MigrateInternal(queue, MemMigrationFlags.MigrateMemObjectHost, ids, waitList);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public AutoDisposedEvent MigrateToDeviceAsUndefined(CommandQueue queue, params Event[] waitList)
        {
            Span<MemoryId> ids = stackalloc MemoryId[1];
            ids[0] = Id;

            return MigrateInternal(queue, MemMigrationFlags.MigrateMemObjectContentUndefined, ids, waitList);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public AutoDisposedEvent MigrateToDeviceAsUndefined(CommandQueue queue, IEnumerable<Event> waitList)
        {
            Span<MemoryId> ids = stackalloc MemoryId[1];
            ids[0] = Id;

            return MigrateInternal(queue, MemMigrationFlags.MigrateMemObjectContentUndefined, ids, waitList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AutoDisposedEvent MigrateToHost(CommandQueue queue,
            IEnumerable<MemoryObject> memoryObjects, params Event[] waitList)
        {
            return Migrate(queue, MemMigrationFlags.MigrateMemObjectHost, memoryObjects, waitList);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static AutoDisposedEvent MigrateToHost(CommandQueue queue,
            IEnumerable<MemoryObject> memoryObjects, IEnumerable<Event> waitList)
        {
            return Migrate(queue, MemMigrationFlags.MigrateMemObjectHost, memoryObjects, waitList);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static AutoDisposedEvent MigrateToDeviceAsUndefined(CommandQueue queue,
            IEnumerable<MemoryObject> memoryObjects, params Event[] waitList)
        {
            return Migrate(queue, MemMigrationFlags.MigrateMemObjectContentUndefined, memoryObjects, waitList);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static AutoDisposedEvent MigrateToDeviceAsUndefined(CommandQueue queue,
            IEnumerable<MemoryObject> memoryObjects, IEnumerable<Event> waitList)
        {
            return Migrate(queue, MemMigrationFlags.MigrateMemObjectContentUndefined, memoryObjects, waitList);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static AutoDisposedEvent Migrate(CommandQueue queue, MemMigrationFlags flags,
            IEnumerable<MemoryObject> memoryObjects, IEnumerable<Event> waitList)
        {
            if (!(memoryObjects is ICollection<MemoryObject> collection))
                collection = memoryObjects.ToList();

            var ids = new MemoryId[collection.Count];

            using (var enumerator = collection.GetEnumerator())
            {
                for (var i = 0; i < ids.Length && enumerator.MoveNext(); i++)
                    ids[i] = enumerator.Current.Id;
            }

            return MigrateInternal(queue, flags, ids, waitList);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static unsafe AutoDisposedEvent MigrateInternal(CommandQueue queue, MemMigrationFlags flags,
            Span<MemoryId> memoryObjectIds, IEnumerable<Event> waitList)
        {
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            var eventIds = waitList?.Select(@event => @event.Id).ToArray();
            var eventCount = (uint) (eventIds?.Length ?? 0);

            var library = queue.Library;

            EventId eventId;

            fixed (MemoryId* idsPtr = memoryObjectIds)
            fixed (EventId* eventIdsPtr = eventIds)
            {
                library.clEnqueueMigrateMemObjectsUnsafe(queue, (uint) memoryObjectIds.Length, idsPtr,
                    flags, eventCount, eventIdsPtr, &eventId).HandleError();
            }

            return AutoDisposedEvent.FromId(library, eventId);
        }
    }
}
