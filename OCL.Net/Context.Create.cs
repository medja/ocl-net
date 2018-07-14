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
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public sealed partial class Context
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Context Create(params Device[] devices)
        {
            return CreateInternal(devices);
        }

        public static Context Create(IEnumerable<Device> devices)
        {
            if (devices is ICollection<Device> collection)
                return CreateInternal(collection);

            return CreateInternal(devices?.ToList());
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static unsafe Context CreateInternal(ICollection<Device> devices)
        {
            if (devices == null)
                throw new ArgumentNullException(nameof(devices));

            if (devices.Count == 0)
                throw new ArgumentException("At least one device is required", nameof(devices));

            if (devices.Any(device => device == null))
                throw new ArgumentException("Devices cannot be null", nameof(devices));

            var lib = devices.First().Library;

            if (devices.Any(device => device.Library != lib))
                throw new ArgumentException(
                    "All devices must to be created using the same OpenCL library instance", nameof(devices));

            var handle = Utils.CreateContextHandle();

            var ids = new DeviceId[devices.Count];

            using (var enumerator = devices.GetEnumerator())
            {
                for (var i = 0; i < ids.Length && enumerator.MoveNext(); i++)
                    ids[i] = enumerator.Current.Id;
            }

            var length = (uint) ids.Length;

            ContextId id;
            ErrorCode errorCode;

            fixed (DeviceId* idsPtr = ids)
            {
                id = lib.clCreateContextUnsafe(null, length, idsPtr, ContextCallback, (void*) handle, &errorCode);
            }

            errorCode.HandleError();

            var context = new Context(id, lib, handle);
            RegisterHandle(handle, context);

            return context;
        }
    }
}
