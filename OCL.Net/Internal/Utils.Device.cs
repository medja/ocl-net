using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static Device CreateDevice(IOpenCl library, DeviceId deviceId)
        {
            return Device.FromId(library, deviceId);
        }
    }
}
