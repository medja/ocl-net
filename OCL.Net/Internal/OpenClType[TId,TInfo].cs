using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using OCL.Net.Native;
using OCL.Net.Native.Enums;

namespace OCL.Net.Internal
{
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public abstract class OpenClType<TId, TInfo>
        where TId : struct, IHandle
        where TInfo : struct
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

        protected abstract ErrorCode GetInfo(TInfo info, UIntPtr bufferSize, IntPtr buffer, out UIntPtr size);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected string LoadString(TInfo info, ref string cache)
        {
            return cache ?? (cache = LoadString(info));
        }

        protected string LoadString(TInfo info)
        {
            return Encoding.UTF8.GetString(LoadArray<byte>(info)).TrimEnd('\0');
        }

        protected T LoadValue<T>(TInfo info, ref T? cache) where T : struct
        {
            if (cache.HasValue)
                return cache.Value;

            var value = LoadValue<T>(info);
            cache = value;

            return value;
        }

        protected T LoadValue<T>(TInfo info) where T : struct
        {
            return LoadArray<T>(info).FirstOrDefault();
        }

        protected T[] LoadArray<T>(TInfo info, ref T[] cache) where T : struct
        {
            return cache ?? (cache = LoadArray<T>(info));
        }

        protected unsafe T[] LoadArray<T>(TInfo info) where T : struct
        {
            GetInfo(info, UIntPtr.Zero, IntPtr.Zero, out var size).HandleError();

            var buffer = new T[(int) size / Unsafe.SizeOf<T>()];
            var span = MemoryMarshal.AsBytes(new Span<T>(buffer));

            fixed (byte* ptr = &span.GetPinnableReference())
            {
                GetInfo(info, size, (IntPtr) ptr, out _).HandleError();
            }

            return buffer;
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
