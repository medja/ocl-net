using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Program
    {
        private const string StoreArgumentTypes = "-cl-kernel-arg-info";
        private static readonly Regex StoreArgumentTypesRegex = new Regex("(?:^|\\s)" + StoreArgumentTypes + "\\b");
        private static readonly Dictionary<string, Program> EmptyHeaders = new Dictionary<string, Program>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProgramBuildEvent Compile(bool storeArgumentTypes = true)
        {
            return Compile(
                string.Empty, Devices, (IEnumerable<KeyValuePair<string, Program>>) EmptyHeaders, storeArgumentTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProgramBuildEvent Compile(string options, bool storeArgumentTypes = true)
        {
            return Compile(
                options, Devices, (IEnumerable<KeyValuePair<string, Program>>) EmptyHeaders, storeArgumentTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProgramBuildEvent Compile(string options, IEnumerable<Device> devices, bool storeArgumentTypes = true)
        {
            return Compile(
                options, devices, (IEnumerable<KeyValuePair<string, Program>>) EmptyHeaders, storeArgumentTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProgramBuildEvent Compile(string options,
            Dictionary<string, Program> headers, bool storeArgumentTypes = true)
        {
            return Compile(options, Devices, (IEnumerable<KeyValuePair<string, Program>>) headers, storeArgumentTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProgramBuildEvent Compile(string options, IEnumerable<Device> devices,
            Dictionary<string, Program> headers, bool storeArgumentTypes = true)
        {
            return Compile(options, devices, (IEnumerable<KeyValuePair<string, Program>>) headers, storeArgumentTypes);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public unsafe ProgramBuildEvent Compile(string options, IEnumerable<Device> devices,
            IEnumerable<KeyValuePair<string, Program>> headers, bool storeArgumentTypes = true)
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

            var headersCollection = GetHeadersCollection(headers);

            var buffers = new PinnedBuffer[headersCollection.Count];
            var inputHeaders = new ProgramId[headersCollection.Count];
            var headerIncludeNames = new byte*[headersCollection.Count];

            using (var enumerator = headersCollection.GetEnumerator())
            {
                for (var i = 0; i < buffers.Length && enumerator.MoveNext(); i++)
                {
                    var entry = enumerator.Current;

                    if (entry.Key == null)
                        throw new ArgumentNullException(nameof(headers), "All header include names must be non-null");

                    if (entry.Value == null)
                        throw new ArgumentNullException(nameof(headers), "All input headers must be non-null");

                    var buffer = new PinnedBuffer(Encoding.UTF8.GetBytes(entry.Key));

                    buffers[i] = buffer;
                    inputHeaders[i] = entry.Value.Id;
                    headerIncludeNames[i] = (byte*) buffer.Handle.Pointer;
                }
            }

            var handle = Utils.CreateProgramBuildEventHandle();

            ErrorCode errorCode;

            fixed (DeviceId* deviceIdsPtr = deviceIds)
            fixed (ProgramId* inputHeadersPtr = inputHeaders)
            fixed (byte** headerIncludeNamesPtr = headerIncludeNames)
            {
                errorCode = Library.clCompileProgramUnsafe(Id,
                    (uint) deviceIds.Length, deviceIdsPtr,
                    options, (uint) inputHeaders.Length, inputHeadersPtr, headerIncludeNamesPtr,
                    ProgramBuildEvent.ProgramBuildCallback, (void*) handle);
            }

            foreach (var buffer in buffers)
                buffer.Dispose();

            if (!IsBuildError(errorCode))
                errorCode.HandleError();

            return ProgramBuildEvent.Create(ProgramBuildType.Compile, this, devicesCollection, handle);
        }

        [SuppressMessage("ReSharper", "RedundantIfElseBlock")]
        [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetOptions(string options, bool storeArgumentTypes)
        {
            if (storeArgumentTypes)
            {
                if (StoreArgumentTypesRegex.IsMatch(options))
                    return options;

                return StoreArgumentTypes + " " + options;
            }
            else
            {
                if (!StoreArgumentTypesRegex.IsMatch(options))
                    return options;

                return StoreArgumentTypesRegex.Replace(options, string.Empty);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICollection<KeyValuePair<string, Program>> GetHeadersCollection(
            IEnumerable<KeyValuePair<string, Program>> headers)
        {
            switch (headers)
            {
                case null:
                    return EmptyHeaders;

                case ICollection<KeyValuePair<string, Program>> collection:
                    return collection;

                default:
                    return headers.ToList();
            }
        }
    }
}
