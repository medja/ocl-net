using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Program
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Program> FromBinary(Context context,
            Dictionary<Device, string> filePaths, CancellationToken cancellationToken = default)
        {
            return FromBinary(context, (IEnumerable<KeyValuePair<Device, string>>) filePaths, cancellationToken);
        }

        public static async Task<Program> FromBinary(Context context,
            IEnumerable<KeyValuePair<Device, string>> filePaths, CancellationToken cancellationToken = default)
        {
            if (filePaths == null)
                throw new ArgumentNullException(nameof(filePaths));

            var binaries = await Task.WhenAll(filePaths.Select(async entry =>
            {
                var binary = await Utils.ReadFileAsync(entry.Value, cancellationToken);
                return new KeyValuePair<Device, byte[]>(entry.Key, binary);
            }));

            return FromBinary(context, binaries);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Program FromBinary(Context context, Dictionary<Device, byte[]> binaries)
        {
            return FromBinary(context, (IEnumerable<KeyValuePair<Device, byte[]>>) binaries);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static unsafe Program FromBinary(Context context, IEnumerable<KeyValuePair<Device, byte[]>> binaries)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (binaries == null)
                throw new ArgumentNullException(nameof(binaries));

            if (!(binaries is ICollection<KeyValuePair<Device, byte[]>> collection))
                collection = binaries.ToList();

            var deviceIds = new DeviceId[collection.Count];
            var buffers = new PinnedBuffer[collection.Count];
            var pointers = new byte*[collection.Count];
            var lengths = new UIntPtr[collection.Count];
            var errorCodes = new ErrorCode[collection.Count];

            using (var enumerator = collection.GetEnumerator())
            {
                for (var i = 0; i < buffers.Length && enumerator.MoveNext(); i++)
                {
                    var entry = enumerator.Current;

                    if (entry.Key == null)
                        throw new ArgumentNullException(nameof(binaries), "All devices must be non-null");

                    if (entry.Value == null)
                        throw new ArgumentNullException(nameof(binaries), "All binaries must be non-null");

                    var buffer = new PinnedBuffer(entry.Value);

                    deviceIds[i] = entry.Key;
                    buffers[i] = buffer;
                    pointers[i] = (byte*) buffer.Handle.Pointer;
                    lengths[i] = (UIntPtr) buffer.Buffer.Length;
                }
            }

            ProgramId id;
            ErrorCode errorCode;

            fixed (DeviceId* devicesPtr = deviceIds)
            fixed (byte** buffersPtr = pointers)
            fixed (UIntPtr* lengthsPtr = lengths)
            fixed (ErrorCode* errorCodesPtr = errorCodes)
            {
                id = context.Library.clCreateProgramWithBinaryUnsafe(context,
                    (uint) deviceIds.Length, devicesPtr, lengthsPtr, buffersPtr, errorCodesPtr, &errorCode);
            }

            foreach (var buffer in buffers)
                buffer.Dispose();

            foreach (var code in errorCodes)
                code.HandleError();

            errorCode.HandleError();

            return FromId(context.Library, id);
        }
    }
}
