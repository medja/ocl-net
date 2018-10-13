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
        public ProgramBuildEvent Link()
        {
            return Link(string.Empty, (IEnumerable<Device>) Devices);
        }

        public ProgramBuildEvent Link(string options, params Device[] devices)
        {
            return Link(string.Empty, (IEnumerable<Device>) devices);
        }

        public ProgramBuildEvent Link(string options, IEnumerable<Device> devices)
        {
            return Link(Context, options, devices, this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProgramBuildEvent Link(Context context, params Program[] programs)
        {
            return Link(context, string.Empty, context.Devices, (IEnumerable<Program>) programs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProgramBuildEvent Link(Context context, IEnumerable<Program> programs)
        {
            return Link(context, string.Empty, context.Devices, programs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProgramBuildEvent Link(Context context, string options, params Program[] programs)
        {
            return Link(context, options, context.Devices, (IEnumerable<Program>) programs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProgramBuildEvent Link(Context context, string options, IEnumerable<Program> programs)
        {
            return Link(context, options, context.Devices, programs);
        }

        public static ProgramBuildEvent Link(
            Context context, string options, IEnumerable<Device> devices, params Program[] programs)
        {
            return Link(context, options, devices, (IEnumerable<Program>) programs);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static unsafe ProgramBuildEvent Link(
            Context context, string options, IEnumerable<Device> devices, IEnumerable<Program> programs)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (devices == null)
                throw new ArgumentNullException(nameof(devices));

            if (programs == null)
                throw new ArgumentNullException(nameof(programs));

            if (options == null)
                options = string.Empty;

            if (!(devices is ICollection<Device> devicesCollection))
                devicesCollection = devices.ToList();

            var deviceIds = new DeviceId[devicesCollection.Count];

            using (var enumerator = devicesCollection.GetEnumerator())
                for (var i = 0; i < deviceIds.Length && enumerator.MoveNext(); i++)
                    deviceIds[i] = enumerator.Current.Id;

            if (!(programs is ICollection<Program> programsCollection))
                programsCollection = programs.ToList();

            var programIds = new ProgramId[programsCollection.Count];

            using (var enumerator = programsCollection.GetEnumerator())
                for (var i = 0; i < programIds.Length && enumerator.MoveNext(); i++)
                    programIds[i] = enumerator.Current.Id;

            var handle = Utils.CreateProgramBuildEventHandle();

            ProgramId id;
            ErrorCode errorCode;

            fixed (DeviceId* deviceIdsPtr = deviceIds)
            fixed (ProgramId* programIdsPtr = programIds)
            {
                id = context.Library.clLinkProgramUnsafe(context,
                    (uint) deviceIds.Length, deviceIdsPtr, options, (uint) programIds.Length, programIdsPtr,
                    ProgramBuildEvent.ProgramBuildCallback, (void*) handle, &errorCode);
            }

            if (!IsBuildError(errorCode))
                errorCode.HandleError();

            var program = FromId(context.Library, id);

            return ProgramBuildEvent.Create(ProgramBuildType.Link, program, devicesCollection, handle);
        }
    }
}
