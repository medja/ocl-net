using System;
using System.Buffers;
using OCL.Net.Internal;
using OCL.Net.Native;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public abstract partial class MemoryObject : RefCountedType<MemoryId, MemInfo>
    {
        public const MemoryFlags DefaultFlags = MemoryFlags.Read | MemoryFlags.Write;

        public event EventHandler<EventArgs> Destroy
        {
            add => AddOnDestroy(value);
            remove => RemoveOnDestroy(value);
        }

        private event EventHandler<EventArgs> DestroyInternal;

        private MemoryHandle _handle;
        private IDisposable _disposable;
        private bool _hasDestructorCallback;

        private readonly object _syncLock = new object();

        private protected unsafe MemoryObject(MemoryId id, IOpenCl lib, MemoryHandle handle, IDisposable disposable)
            : base(id, lib)
        {
            _handle = handle;
            _disposable = disposable;

            if (_handle.Pointer == null && _disposable == null)
                return;

            _hasDestructorCallback = true;
            Library.clSetMemObjectDestructorCallback(Id, DestructorCallback, IntPtr.Zero).HandleError();
        }

        protected override unsafe ErrorCode GetInfo(MemInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
        {
            return Library.clGetMemObjectInfoUnsafe(Id, info, bufferSize, buffer, size);
        }

        protected unsafe ErrorCode GetImageInfo(ImageInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size)
        {
            return Library.clGetImageInfoUnsafe((ImageId) Id, info, bufferSize, buffer, size);
        }

        protected override ErrorCode RetainObject()
        {
            return Library.clRetainMemObject(Id);
        }

        protected override ErrorCode ReleaseObject()
        {
            return Library.clReleaseMemObject(Id);
        }

        private void AddOnDestroy(EventHandler<EventArgs> handler)
        {
            DestroyInternal += handler;

            if (_hasDestructorCallback)
                return;

            lock (_syncLock)
            {
                if (_hasDestructorCallback)
                    return;

                _hasDestructorCallback = true;
            }

            Library.clSetMemObjectDestructorCallback(Id, DestructorCallback, IntPtr.Zero).HandleError();
        }

        private void RemoveOnDestroy(EventHandler<EventArgs> handler)
        {
            DestroyInternal -= handler;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            OnDestroy();
        }

        private unsafe void OnDestroy()
        {
            DestroyInternal?.Invoke(this, EventArgs.Empty);

            lock (_syncLock)
            {
                if (_handle.Pointer != null)
                {
                    _handle.Dispose();
                    _handle = default;
                }

                _disposable?.Dispose();
                _disposable = null;
            }
        }

        public static implicit operator MemoryObject(MemoryId id)
        {
            return FromId(id) as MemoryObject;
        }

        public static implicit operator MemoryId(MemoryObject memory)
        {
            return memory.Id;
        }

        internal static MemoryObject FromId(IOpenCl lib, MemoryId id)
        {
            return FromId(id) as MemoryObject;
        }
    }
}
