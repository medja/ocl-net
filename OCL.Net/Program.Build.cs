using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Program
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProgramBuildEvent Build(bool storeArgumentTypes = true)
        {
            return Build(string.Empty, Devices, storeArgumentTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProgramBuildEvent Build(string options, bool storeArgumentTypes = true)
        {
            return Build(options, Devices, storeArgumentTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public unsafe ProgramBuildEvent Build(string options, IEnumerable<Device> devices,
            bool storeArgumentTypes = true)
        {
            if (devices == null)
                throw new ArgumentNullException(nameof(devices));

            if (options == null)
                options = storeArgumentTypes ? StoreArgumentTypes : string.Empty;
            else
                options = GetOptions(options, storeArgumentTypes);

            if (!(devices is ICollection<Device> devicesCollection))
                devicesCollection = devices.ToList();

            var deviceIds = new DeviceId[devicesCollection.Count];

            using (var enumerator = devicesCollection.GetEnumerator())
                for (var i = 0; i < deviceIds.Length && enumerator.MoveNext(); i++)
                    deviceIds[i] = enumerator.Current.Id;

            var handle = Utils.CreateProgramBuildEventHandle();

            ErrorCode errorCode;

            fixed (DeviceId* deviceIdsPtr = deviceIds)
            {
                errorCode = Library.clBuildProgramUnsafe(Id, (uint) deviceIds.Length, deviceIdsPtr,
                    options, ProgramBuildEvent.ProgramBuildCallback, (void*) handle);
            }

            if (!IsBuildError(errorCode))
                errorCode.HandleError();

            return ProgramBuildEvent.Create(ProgramBuildType.Build, this, devicesCollection, handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
        private static bool IsBuildError(ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.BuildProgramFailure:
                case ErrorCode.CompileProgramFailure:
                case ErrorCode.LinkProgramFailure:
                    return true;

                default:
                    return false;
            }
        }
    }
}
