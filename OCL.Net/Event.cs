using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public partial class Event : RefCountedType<EventId, EventInfo>
    {
        public bool IsFlushed { get; private set; }
        public bool IsCompleted { get; private set; }
        public ErrorCode Status { get; private set; } = ErrorCode.Success;

        private bool _autoDispose;
        private List<Action> _continuations;
        private List<Action> _unsafeContinuations;

        private readonly bool _userEvent;
        private readonly object _syncLock = new object();
        private readonly Lazy<TaskCompletionSource<object>> _taskCompletionSource;

        private protected unsafe Event(EventId id, IOpenCl lib, bool autoDispose = false, bool userEvent = false)
            : base(id, lib)
        {
            _autoDispose = autoDispose;
            _userEvent = userEvent;

            _taskCompletionSource = new Lazy<TaskCompletionSource<object>>(CreateTaskCompletionSource);

            if (userEvent)
            {
                IsFlushed = true;
                return;
            }

            Library.clSetEventCallbackUnsafe(Id, CommandExecutionStatus.Complete, EventCallback, null).HandleError();
        }

        public new void Retain()
        {
            if (!DisableAutoDispose())
                base.Retain();
        }

        [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
        internal bool DisableAutoDispose()
        {
            if (!_autoDispose)
                return false;

            lock (_syncLock)
            {
                if (!_autoDispose)
                    return false;

                _autoDispose = false;
                return true;
            }
        }

        protected override unsafe ErrorCode GetInfo(EventInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
        {
            return Library.clGetEventInfoUnsafe(Id, info, bufferSize, buffer, size);
        }

        protected override ErrorCode RetainObject()
        {
            return Library.clRetainEvent(Id);
        }

        protected override ErrorCode ReleaseObject()
        {
            return Library.clReleaseEvent(Id);
        }

        public static implicit operator Event(EventId id)
        {
            return FromId(id) as Event;
        }

        public static implicit operator EventId(Event @event)
        {
            return @event.Id;
        }

        internal static Event FromId(IOpenCl lib, EventId id, bool autoDispose = false, bool userEvent = false)
        {
            return FromId(id) as Event ?? new Event(id, lib, autoDispose, userEvent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Event<T> FromId<T>(IOpenCl lib, EventId id, T result,
            bool autoDispose = false, bool userEvent = false)
        {
            return Event<T>.FromId(lib, id, result, autoDispose, userEvent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Event<T> FromId<T>(IOpenCl lib, EventId id, Func<T> resultFactory,
            bool autoDispose = false, bool userEvent = false)
        {
            return Event<T>.FromId(lib, id, resultFactory, autoDispose, userEvent);
        }
    }
}
