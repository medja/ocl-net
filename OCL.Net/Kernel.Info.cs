using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Kernel
    {
        public string Attributes => LoadString(KernelInfo.KernelAttributes, ref _attributes);
        public Context Context => Context.FromId(Library, LoadValue(KernelInfo.KernelContext, ref _context));
        public string FunctionName => LoadString(KernelInfo.KernelFunctionName, ref _functionName);
        public uint NumArgs => LoadValue(KernelInfo.KernelNumArgs, ref _numArgs);
        public Program Program => Program.FromId(Library, LoadValue(KernelInfo.KernelProgram, ref _program));
        public override uint ReferenceCount => LoadValue<uint>(KernelInfo.KernelReferenceCount);

        private string _attributes;
        private ContextId? _context;
        private string _functionName;
        private uint? _numArgs;
        private ProgramId? _program;
    }
}
