using System;
using OCL.Net.Native;
using OCL.Net.Native.Enums;

namespace OCL.Net.Internal
{
    public abstract class RefCountedType<TId, TInfo> : OpenClType<TId, TInfo>, IDisposable
        where TId : struct, IHandle
        where TInfo : Enum
    {
        public abstract uint ReferenceCount { get; }

        public uint ManagedReferenceCount
        {
            get { lock (_syncLock) return _references; }
        }

        public override bool IsDisposed
        {
            get { lock (_syncLock) return _disposed; }
        }

        private bool _disposed;
        private uint _references = 1;

        private readonly object _syncLock = new object();

        private protected RefCountedType(TId id, IOpenCl lib) : base(id, lib)
        { }

        protected abstract ErrorCode RetainObject();
        protected abstract ErrorCode ReleaseObject();

        protected virtual void OnDispose()
        {
            Dispose(Id);
        }

        public void Retain()
        {
            lock (_syncLock)
            {
                if (_disposed)
                    throw new ObjectDisposedException($"{GetType().Name} is already disposed");

                RetainObject().HandleError();
                _references++;
            }
        }

        public void Release()
        {
            if (_disposed)
                return;

            lock (_syncLock)
            {
                if (_disposed)
                    return;

                ReleaseObject().HandleError();
                _references--;

                if (_references != 0)
                    return;

                _disposed = true;
                OnDispose();
            }
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            lock (_syncLock)
            {
                DisposeInternal();
            }
        }

        private void DisposeInternal()
        {
            if (_disposed)
                return;

            try
            {
                for (var i = 0; i < _references; i++)
                    ReleaseObject().HandleError();
            }
            catch
            {
                // ignored
            }

            _disposed = true;
            _references = 0;
            OnDispose();
        }

        ~RefCountedType()
        {
            try
            {
                DisposeInternal();
            }
            catch
            {
                // ignored
            }
        }
    }
}
