using System;
using System.Diagnostics.CodeAnalysis;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public partial class Event
    {
        public CommandExecutionStatus CommandExecutionStatus => LoadValue<CommandExecutionStatus>(EventInfo.EventCommandExecutionStatus);
        public CommandQueue CommandQueue => CommandQueue.FromId(Library, LoadValue(EventInfo.EventCommandQueue, ref _commandQueue), false);
        public CommandType CommandType => LoadValue(EventInfo.EventCommandType, ref _commandType);
        public Context Context => Context.FromId(Library, LoadValue(EventInfo.EventContext, ref _context));
        public override uint ReferenceCount => LoadValue<uint>(EventInfo.EventReferenceCount);

        public ulong QueuedTimestamp => LoadProfilingTimestamp(ProfilingInfo.ProfilingCommandQueued);
        public ulong SubmittedTimestamp => LoadProfilingTimestamp(ProfilingInfo.ProfilingCommandSubmit);
        public ulong StartedTimestamp => LoadProfilingTimestamp(ProfilingInfo.ProfilingCommandStart);
        public ulong EndedTimestamp => LoadProfilingTimestamp(ProfilingInfo.ProfilingCommandEnd);

        public ulong CreatedTimestamp => _userEvent ? SubmittedTimestamp : QueuedTimestamp;

        public TimeSpan ExecutionDuration => ComputeDuration(StartedTimestamp, EndedTimestamp);
        public TimeSpan TotalDuration => ComputeDuration(CreatedTimestamp, EndedTimestamp);

        private ulong LoadProfilingTimestamp(ProfilingInfo info)
        {
            return InfoLoader.LoadValue<ulong, ProfilingInfo>(info, GetProfilingInfo);
        }

        private ErrorCode GetProfilingInfo(ProfilingInfo info, UIntPtr bufferSize, IntPtr buffer, out UIntPtr size)
        {
            return Library.clGetEventProfilingInfo(Id, info, bufferSize, buffer, out size);
        }

        private static TimeSpan ComputeDuration(ulong start, ulong end)
        {
            return TimeSpan.FromTicks((long) (end - start) * TimeSpan.TicksPerMillisecond / 1_000_000);
        }

        private CommandQueueId? _commandQueue;
        private CommandType? _commandType;
        private ContextId? _context;
    }
}
