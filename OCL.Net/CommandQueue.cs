using System;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class CommandQueue : RefCountedType<CommandQueueId, CommandQueueInfo>
    {
        public bool Profiling => Properties.HasFlag(CommandQueueProperties.QueueProfilingEnable);
        public bool OutOfOrderExecution => Properties.HasFlag(CommandQueueProperties.QueueOutOfOrderExecModeEnable);

        private readonly bool _disposeContext;

        private CommandQueue(CommandQueueId id, IOpenCl lib, bool disposeContext) : base(id, lib)
        {
            _context = Context.FromId(Library, LoadValue<ContextId>(CommandQueueInfo.QueueContext));
            _disposeContext = disposeContext;
        }

        public void Flush()
        {
            Library.clFlush(Id).HandleError();
        }

        public void Finish()
        {
            Library.clFinish(Id).HandleError();
        }

        protected override unsafe ErrorCode GetInfo(
            CommandQueueInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
        {
            return Library.clGetCommandQueueInfoUnsafe(Id, info, bufferSize, buffer, size);
        }

        protected override ErrorCode RetainObject()
        {
            return Library.clRetainCommandQueue(Id);
        }

        protected override ErrorCode ReleaseObject()
        {
            return Library.clReleaseCommandQueue(Id);
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            if (!_disposeContext || _context == null)
                return;

            ((Context) _context.Value)?.Dispose();

            _context = null;
        }

        public static implicit operator CommandQueue(CommandQueueId id)
        {
            return FromId(id) as CommandQueue;
        }

        public static implicit operator CommandQueueId(CommandQueue commandQueue)
        {
            return commandQueue.Id;
        }

        public static implicit operator Context(CommandQueue commandQueue)
        {
            return commandQueue.Context;
        }

        internal static CommandQueue FromId(IOpenCl lib, CommandQueueId id, bool disposeContext)
        {
            return FromId(id) as CommandQueue ?? new CommandQueue(id, lib, disposeContext);
        }
    }
}
