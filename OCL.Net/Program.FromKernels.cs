using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Program
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Program FromKernels(Context context, string kernelNames, params Device[] devices)
        {
            return FromKernels(context, kernelNames, (IEnumerable<Device>) devices);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public Program FromKernels(Context context, string kernelNames, IEnumerable<Device> devices)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (devices == null)
                throw new ArgumentNullException(nameof(devices));

            if (!(devices is ICollection<Device> collection))
                collection = devices.ToList();

            var deviceIds = new DeviceId[collection.Count];

            using (var enumerator = collection.GetEnumerator())
                for (var i = 0; i < deviceIds.Length && enumerator.MoveNext(); i++)
                    deviceIds[i] = enumerator.Current.Id;

            var id = context.Library.clCreateProgramWithBuiltInKernels(context,
                (uint) deviceIds.Length, deviceIds, kernelNames, out var errorCode);

            errorCode.HandleError();

            return FromId(context.Library, id);
        }
    }
}
