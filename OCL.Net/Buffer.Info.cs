using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Buffer
    {
        public Buffer AssociatedMemory => LoadValue(MemInfo.MemAssociatedMemObject, ref AssociatedMemoryId);

        protected BufferId? AssociatedMemoryId;
    }
}
