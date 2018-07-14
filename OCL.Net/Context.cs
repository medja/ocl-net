using System;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class Context : RefCountedType<ContextId, ContextInfo>
    {
        public event EventHandler<RuntimeExceptionArgs> Error;

        private readonly IntPtr _handle;

        private Context(ContextId id, IOpenCl lib, IntPtr handle) : base(id, lib)
        {
            _handle = handle;
        }

        protected override unsafe ErrorCode GetInfo
            (ContextInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
        {
            return Library.clGetContextInfoUnsafe(Id, info, bufferSize, buffer, size);
        }

        protected override ErrorCode RetainObject()
        {
            return Library.clRetainContext(Id);
        }

        protected override ErrorCode ReleaseObject()
        {
            return Library.clReleaseContext(Id);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            DisposeHandle(_handle);
        }

        private void OnError(RuntimeExceptionArgs e)
        {
            Error?.Invoke(this, e);
        }

        public static implicit operator Context(ContextId id)
        {
            return FromId(id) as Context;
        }

        public static implicit operator ContextId(Context context)
        {
            return context.Id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Context FromId(IOpenCl lib, ContextId id)
        {
            return FromId(lib, id, Utils.CreateContextHandle());
        }

        internal static Context FromId(IOpenCl lib, ContextId id, IntPtr handle)
        {
            if (FromId(id) is Context context)
                return context;

            context = new Context(id, lib, handle);
            RegisterHandle(handle, context);

            return context;
        }
    }
}
