using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public sealed partial class Platform
    {
        public string[] Extensions => LoadString(PlatformInfo.PlatformExtensions, ref _extensions).Split(' ');
        public string Name => LoadString(PlatformInfo.PlatformName, ref _name);
        public string Profile => LoadString(PlatformInfo.PlatformProfile, ref _profile);
        public string Vendor => LoadString(PlatformInfo.PlatformVendor, ref _vendor);
        public string Version => LoadString(PlatformInfo.PlatformVersion, ref _version);

        private string _extensions;
        private string _name;
        private string _profile;
        private string _vendor;
        private string _version;
    }
}
