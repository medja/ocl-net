using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public partial class Event
    {
        internal void OnComplete(Action continuation)
        {
            lock (_syncLock)
            {
                if (!IsCompleted)
                {
                    if (_continuations == null)
                        _continuations = new List<Action> {continuation};
                    else
                        _continuations.Add(continuation);

                    return;
                }
            }

            try { continuation(); } catch { /* ignored */ }
        }

        internal void UnsafeOnComplete(Action continuation)
        {
            lock (_syncLock)
            {
                if (!IsCompleted)
                {
                    if (_unsafeContinuations == null)
                        _unsafeContinuations = new List<Action> {continuation};
                    else
                        _unsafeContinuations.Add(continuation);

                    return;
                }
            }

            try { continuation(); } catch { /* ignored */ }
        }

        [SuppressMessage("ReSharper", "InvertIf")]
        protected void UpdateExecutionStatus(CommandExecutionStatus status)
        {
            var errorCode = status.GetErrorCode();

            if (IsFlushed && IsCompleted && Status == errorCode)
                return;

            if (status != CommandExecutionStatus.Complete && errorCode == ErrorCode.Success)
                return;

            var dispose = false;
            List<Action> continuations = null;
            List<Action> unsafeContinuations = null;

            lock (_syncLock)
            {
                if (!IsCompleted)
                {
                    dispose = _autoDispose;
                    _autoDispose = false;

                    continuations = _continuations;
                    _continuations = null;

                    unsafeContinuations = _unsafeContinuations;
                    _unsafeContinuations = null;

                    IsCompleted = true;
                    IsFlushed = true;
                }

                if (errorCode != ErrorCode.Success)
                    Status = errorCode;
            }

            if (continuations != null)
            {
                foreach (var continuation in continuations)
                {
                    try { continuation(); } catch { /* ignored */ }
                }
            }

            if (dispose)
                Release();

            if (unsafeContinuations != null)
            {
                foreach (var unsafeContinuation in unsafeContinuations)
                {
                    try { unsafeContinuation(); } catch { /* ignored */ }
                }
            }
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static unsafe void EventCallback(EventId eventId, CommandExecutionStatus eventCommandExecStatus,
            void* userData)
        {
            void HandleCallback(object state)
            {
                (FromId(eventId) as Event)?.UpdateExecutionStatus(eventCommandExecStatus);
            }

            ThreadPool.UnsafeQueueUserWorkItem(HandleCallback, null);
        }
    }
}
