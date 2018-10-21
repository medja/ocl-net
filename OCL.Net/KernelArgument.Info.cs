using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public sealed partial class KernelArgument
    {
        public KernelArgAccessQualifier AccessQualifier => LoadValue(KernelArgInfo.KernelArgAccessQualifier, ref _accessQualifier);
        public KernelArgAddressQualifier AddressQualifier => LoadValue(KernelArgInfo.KernelArgAddressQualifier, ref _addressQualifier);
        public string Name => LoadString(KernelArgInfo.KernelArgName, ref _name);
        public string TypeName => LoadString(KernelArgInfo.KernelArgTypeName, ref _typeName);
        public KernelArgTypeQualifier TypeQualifier => LoadValue(KernelArgInfo.KernelArgTypeQualifier, ref _typeQualifier);

        private KernelArgAccessQualifier? _accessQualifier;
        private KernelArgAddressQualifier? _addressQualifier;
        private string _name;
        private string _typeName;
        private KernelArgTypeQualifier? _typeQualifier;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe string LoadString(KernelArgInfo info, ref string cache)
        {
            return InfoLoader.LoadString(info, GetInfo, ref cache);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe T LoadValue<T>(KernelArgInfo info, ref T? cache) where T : unmanaged
        {
            return InfoLoader.LoadValue(info, GetInfo, ref cache);
        }
    }
}
