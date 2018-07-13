using System;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public sealed partial class CommandQueue
    {
        public static CommandQueue Create(Device device,
            bool profiling = false, bool outOfOrderExecution = false)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            return CreateInternal(Context.Create(device), device, profiling, outOfOrderExecution, true);
        }

        public static CommandQueue Create(Context context, Device device,
            bool profiling = false, bool outOfOrderExecution = false)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (device == null)
                throw new ArgumentNullException(nameof(device));

            if (context.Library != device.Library)
                throw new ArgumentException(
                    "Device and context must belong to the same OpenCL library instance");

            return CreateInternal(context, device, profiling, outOfOrderExecution, false);
        }

        private static CommandQueue CreateInternal(Context context, Device device,
            bool profiling, bool outOfOrderExecution, bool disposeContext)
        {
            CommandQueueProperties properties = 0;

            if (profiling)
                properties |= CommandQueueProperties.QueueProfilingEnable;

            if (outOfOrderExecution)
                properties |= CommandQueueProperties.QueueOutOfOrderExecModeEnable;

            var id = context.Library.clCreateCommandQueue(context, device, properties, out var errorCode);
            errorCode.HandleError();

            return FromId(context.Library, id, disposeContext);
        }
    }
}
