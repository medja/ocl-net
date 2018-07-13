using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
    public class UserEvent : Event
    {
        private readonly object _syncLock = new object();

        protected UserEvent(EventId id, IOpenCl lib) : base(id, lib, userEvent: true)
        { }

        public void SetCompleted()
        {
            SetStatus(CommandExecutionStatus.Complete);
        }

        public void SetError(ErrorCode errorCode)
        {
            var status = errorCode >= ErrorCode.Success
                ? CommandExecutionStatus.Complete
                : (CommandExecutionStatus) errorCode;

            SetStatus(status);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetStatus(CommandExecutionStatus status)
        {
            if (IsCompleted)
                return;

            lock (_syncLock)
            {
                if (IsCompleted)
                    return;

                Library.clSetUserEventStatus(Id, status).HandleError();
                UpdateExecutionStatus(status);
            }
        }

        public static UserEvent Create(Context context)
        {
            if (context == null)
                throw new ArgumentNullException();

            var id = context.Library.clCreateUserEvent(context, out var errorCode);
            errorCode.HandleError();

            return FromId(context.Library, id);
        }

        public static implicit operator UserEvent(EventId id)
        {
            return FromId(id) as UserEvent;
        }

        public static implicit operator EventId(UserEvent @event)
        {
            return @event.Id;
        }

        internal static UserEvent FromId(IOpenCl lib, EventId id)
        {
            return FromId(id) as UserEvent ?? new UserEvent(id, lib);
        }
    }
}
