using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public sealed partial class Image1D<T>
    {
        public new Buffer<T> Buffer => Net.Buffer.FromId<T>(Library, LoadValue(ImageInfo.ImageBuffer, ref BufferId));
    }
}
