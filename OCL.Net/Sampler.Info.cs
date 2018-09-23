using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Sampler
    {
        public AddressingMode AddressingMode => LoadValue(SamplerInfo.SamplerAddressingMode, ref _addressingMode);
        public Context Context => Context.FromId(Library, LoadValue(SamplerInfo.SamplerContext, ref _context));
        public FilterMode FilterMode => LoadValue(SamplerInfo.SamplerFilterMode, ref _filterMode);
        public bool NormalizedCoords => LoadValue(SamplerInfo.SamplerNormalizedCoords, ref _normalizedCoords);
        public override uint ReferenceCount => LoadValue<uint>(SamplerInfo.SamplerReferenceCount);

        private AddressingMode? _addressingMode;
        private ContextId? _context;
        private FilterMode? _filterMode;
        private bool? _normalizedCoords;
    }
}
