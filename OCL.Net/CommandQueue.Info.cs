using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class CommandQueue
    {
        public Context Context => _context;
        public Device Device => Device.FromId(Library, LoadValue(CommandQueueInfo.QueueDevice, ref _device));
        public CommandQueueProperties Properties => LoadValue(CommandQueueInfo.QueueProperties, ref _properties);
        public override uint ReferenceCount => LoadValue<uint>(CommandQueueInfo.QueueReferenceCount);

        private ContextId? _context;
        private DeviceId? _device;
        private CommandQueueProperties? _properties;
    }
}
