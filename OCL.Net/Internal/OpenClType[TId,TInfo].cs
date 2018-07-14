using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using OCL.Net.Native;
using OCL.Net.Native.Enums;

namespace OCL.Net.Internal
{
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public abstract class OpenClType<TId, TInfo>
        where TId : struct, IHandle
        where TInfo : Enum
    {
        private static readonly Dictionary<TId, WeakReference<OpenClType<TId, TInfo>>> Instances
            = new Dictionary<TId, WeakReference<OpenClType<TId, TInfo>>>();

        public TId Id { get; }
        public virtual bool IsDisposed { get; }

        public IOpenCl Library { get; }

        private protected OpenClType(TId id, IOpenCl lib)
        {
            Id = id;
            Library = lib;

            lock (Instances)
            {
                Instances[id] = new WeakReference<OpenClType<TId, TInfo>>(this);
            }
        }

        protected abstract unsafe ErrorCode GetInfo(TInfo info, UIntPtr bufferSize, byte* buffer, UIntPtr* size);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected string LoadString(TInfo info, ref string cache)
        {
            return cache ?? (cache = LoadString(info));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected unsafe string LoadString(TInfo info)
        {
            return InfoLoader.LoadString(info, GetInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected T LoadValue<T>(TInfo info, ref T? cache) where T : unmanaged
        {
            if (cache.HasValue)
                return cache.Value;

            var value = LoadValue<T>(info);
            cache = value;

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected unsafe T LoadValue<T>(TInfo info) where T : unmanaged
        {
            return InfoLoader.LoadValue<T, TInfo>(info, GetInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected T[] LoadArray<T>(TInfo info, ref T[] cache) where T : unmanaged
        {
            return cache ?? (cache = LoadArray<T>(info));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected unsafe T[] LoadArray<T>(TInfo info) where T : unmanaged
        {
            return InfoLoader.LoadArray<T, TInfo>(info, GetInfo);
        }

        protected static OpenClType<TId, TInfo> FromId(TId id)
        {
            if (id.Handle == IntPtr.Zero)
                return null;

            lock (Instances)
            {
                if (!Instances.TryGetValue(id, out var weakReference))
                    return null;

                if (weakReference.TryGetTarget(out var instance) && !instance.IsDisposed)
                    return instance;

                Instances.Remove(id);
            }

            return null;
        }

        protected static void Dispose(TId id)
        {
            lock (Instances)
            {
                Instances.Remove(id);
            }
        }
    }
}
