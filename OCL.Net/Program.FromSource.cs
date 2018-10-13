using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Program
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Program FromSource(Context context, params string[] sources)
        {
            return FromSource(context, (IEnumerable<string>) sources);
        }

        public static Program FromSource(Context context, IEnumerable<string> sources)
        {
            return FromSource(context, sources?.Select(source => Encoding.UTF8.GetBytes(source)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Program FromSource(Context context, params byte[][] sources)
        {
            return FromSource(context, (IEnumerable<byte[]>) sources);
        }

        public static unsafe Program FromSource(Context context, IEnumerable<byte[]> sources)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (sources == null)
                throw new ArgumentNullException(nameof(sources));

            if (!(sources is ICollection<byte[]> collection))
                collection = sources.ToList();

            if (collection.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(sources), "At least one source is required");

            var buffers = new PinnedBuffer[collection.Count];
            var pointers = new byte*[collection.Count];
            var lengths = new UIntPtr[collection.Count];

            using (var enumerator = collection.GetEnumerator())
            {
                for (var i = 0; i < buffers.Length && enumerator.MoveNext(); i++)
                {
                    if (enumerator.Current == null)
                        throw new ArgumentNullException(nameof(sources), "All sources must be non-null");

                    var buffer = new PinnedBuffer(enumerator.Current);

                    buffers[i] = buffer;
                    pointers[i] = (byte*) buffer.Handle.Pointer;
                    lengths[i] = (UIntPtr) buffer.Buffer.Length;
                }
            }

            ProgramId id;
            ErrorCode errorCode;

            fixed (byte** buffersPtr = pointers)
            fixed (UIntPtr* lengthsPtr = lengths)
            {
                id = context.Library.clCreateProgramWithSourceUnsafe(
                    context, (uint) pointers.Length, buffersPtr, lengthsPtr, &errorCode);
            }

            foreach (var buffer in buffers)
                buffer.Dispose();

            errorCode.HandleError();

            return FromId(context.Library, id);
        }
    }
}
