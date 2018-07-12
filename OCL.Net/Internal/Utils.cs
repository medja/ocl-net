using OCL.Net.Native;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public static class Utils
    {
        public static Device CreateDevice(IOpenCl library, DeviceId deviceId)
        {
            return Device.FromId(library, deviceId);
        }

        public static Platform CreatePlatform(IOpenCl library, PlatformId platformId)
        {
            return Platform.FromId(library, platformId);
        }
    }
}
