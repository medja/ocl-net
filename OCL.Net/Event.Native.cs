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
    public partial class Event
    {
        public void Flush()
        {
            if (IsFlushed || IsCompleted)
                return;

            CommandQueue.Flush();
            IsFlushed = true;
        }

        public unsafe void Wait()
        {
            if (IsCompleted)
            {
                Status.HandleError();
                return;
            }

            var id = Id;
            var errorCode = Library.clWaitForEventsUnsafe(1, &id);

            Status.HandleError();
            errorCode.HandleError();

            UpdateExecutionStatus(CommandExecutionStatus.Complete);
        }

        public AutoDisposedEvent Enqueue(CommandQueue commandQueue, bool blocking = true)
        {
            Span<EventId> ids = stackalloc EventId[1];
            ids[0] = Id;

            var eventId = Utils.EnqueueEvents(commandQueue, blocking, ids);

            return AutoDisposedEvent.FromId(Library, eventId);
        }

        #region WaitAll

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WaitAll(params Event[] events)
        {
            WaitAll((IEnumerable<Event>) events);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static void WaitAll(IEnumerable<Event> events)
        {
            if (events == null)
                return;

            foreach (var group in events.GroupBy(@event => @event.Library))
            {
                var ids = new EventId[group.Count()];

                using (var enumerator = group.GetEnumerator())
                {
                    for (var i = 0; i < ids.Length && enumerator.MoveNext(); i++)
                        ids[i] = enumerator.Current.Id;
                }

                group.Key.clWaitForEvents((uint) ids.Length, ids).HandleError();
            }
        }

        #endregion

        #region WhenAll

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Event WhenAll(bool blocking, params Event[] events)
        {
            return WhenAll(blocking, (IEnumerable<Event>) events);
        }

        public static Event WhenAll(bool blocking, IEnumerable<Event> events)
        {
            if (events == null)
                throw new ArgumentNullException(nameof(events));

            if (!(events is ICollection<Event> collection))
                collection = events.ToList();

            if (collection.Count == 0)
                throw new ArgumentException("At least one event is required", nameof(events));

            CommandQueue commandQueue = null;

            foreach (var @event in collection)
            {
                if (@event == null)
                    throw new ArgumentException("Events cannot be null", nameof(events));

                var queue = @event.CommandQueue;

                if (commandQueue == null)
                    commandQueue = queue;
                else if (queue != null && queue != commandQueue)
                    throw new ArgumentException("All events must belong to the same command queue", nameof(events));
            }

            if (commandQueue == null)
                throw new ArgumentException("At least one event must be bound to a command queue", nameof(events));

            return WhenAllInternal(commandQueue, blocking, collection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Event WhenAll(CommandQueue commandQueue, bool blocking = true)
        {
            return WhenAllInternal(commandQueue, blocking, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Event WhenAll(CommandQueue commandQueue, bool blocking, params Event[] events)
        {
            return WhenAll(commandQueue, blocking, (IEnumerable<Event>) events);
        }

        public static Event WhenAll(CommandQueue commandQueue, bool blocking, IEnumerable<Event> events)
        {
            if (commandQueue == null)
                throw new ArgumentNullException(nameof(commandQueue));

            if (!(events is ICollection<Event> collection))
                collection = events?.ToList();

            if (collection != null && collection.Any(@event => @event == null))
                throw new ArgumentException("Events cannot be null", nameof(events));

            return WhenAllInternal(commandQueue, blocking, collection);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static Event WhenAllInternal(CommandQueue commandQueue, bool blocking, ICollection<Event> events)
        {
            EventId[] eventIds;

            if (events == null || events.Count == 0)
            {
                eventIds = null;
            }
            else
            {
                eventIds = new EventId[events.Count];

                using (var enumerator = events.GetEnumerator())
                {
                    for (var i = 0; i < eventIds.Length && enumerator.MoveNext(); i++)
                        eventIds[i] = enumerator.Current.Id;
                }
            }

            var eventId = Utils.EnqueueEvents(commandQueue, blocking, eventIds);

            return FromId(commandQueue.Library, eventId);
        }

        #endregion
    }
}
