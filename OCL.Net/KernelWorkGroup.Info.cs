using System;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public sealed partial class KernelWorkGroup
    {
        public ulong[] CompileWorkGroupSize => LoadArray(KernelWorkGroupInfo.KernelCompileWorkGroupSize, ref _compileWorkGroupSize).Cast<ulong>();
        public ulong[] GlobalWorkSize => LoadArray(KernelWorkGroupInfo.KernelGlobalWorkSize, ref _globalWorkSize).Cast<ulong>();
        public ulong LocalMemSize => LoadValue(KernelWorkGroupInfo.KernelLocalMemSize, ref _localMemSize);
        public ulong PreferredGroupSizeMultiple => LoadValue(KernelWorkGroupInfo.KernelPreferredWorkGroupSizeMultiple, ref _preferredGroupSizeMultiple).ToUInt64();
        public ulong PrivateMemSize => LoadValue(KernelWorkGroupInfo.KernelPrivateMemSize, ref _privateMemSize);
        public ulong WorkGroupSize => LoadValue(KernelWorkGroupInfo.KernelWorkGroupSize, ref _workGroupSize).ToUInt64();

        private UIntPtr[] _compileWorkGroupSize;
        private UIntPtr[] _globalWorkSize;
        private ulong? _localMemSize;
        private UIntPtr? _preferredGroupSizeMultiple;
        private ulong? _privateMemSize;
        private UIntPtr? _workGroupSize;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe T LoadValue<T>(KernelWorkGroupInfo info, ref T? cache) where T : unmanaged
        {
            return InfoLoader.LoadValue(info, GetInfo, ref cache);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe T[] LoadArray<T>(KernelWorkGroupInfo info, ref T[] cache) where T : unmanaged
        {
            return InfoLoader.LoadArray(info, GetInfo, ref cache);
        }
    }
}
