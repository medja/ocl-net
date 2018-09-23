using System;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public sealed partial class Sampler
    {
        public static Sampler Create(Context context,
            bool normalized, AddressingMode addressingMode, FilterMode filterMode)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var id = context.Library.clCreateSampler(
                context, normalized, addressingMode, filterMode, out var errorCode);

            errorCode.HandleError();

            return FromId(context.Library, id);
        }
    }
}
