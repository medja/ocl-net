using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class Image1D
    {
        public Buffer Buffer => LoadValue(ImageInfo.ImageBuffer, ref BufferId);

        protected BufferId? BufferId;
    }
}
