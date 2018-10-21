using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class KernelArgument
    {
        private static readonly HashSet<Type> WellKnownTypes = new HashSet<Type>
        {
            typeof(bool),
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double)
        };

        public unsafe void Set<T>(Buffer<T> buffer) where T : unmanaged
        {
            var type = Type;

            if (type != null && type != typeof(Buffer<T>) && !IsCompatibleBufferType<T>(type))
                throw new ArgumentException($"Argument requires a value of type {type}", nameof(buffer));

            var id = buffer.Id;
            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(BufferId), &id).HandleError();
        }

        public unsafe void SetId(BufferId bufferId)
        {
            var type = Type;

            if (type != null && typeof(Buffer).IsAssignableFrom(type))
                throw new ArgumentException($"Argument requires a value of type {type}", nameof(bufferId));

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(BufferId), &bufferId).HandleError();
        }

        public unsafe void Set(Event @event)
        {
            var type = Type;

            if (type != null && type != typeof(Event) && Marshal.SizeOf(type) != sizeof(EventId))
                throw new ArgumentException($"Argument requires a value of type {type}", nameof(@event));

            var id = @event.Id;
            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(SamplerId), &id).HandleError();
        }

        public unsafe void SetId(EventId eventId)
        {
            var type = Type;

            if (type != null && type != typeof(Event) && Marshal.SizeOf(type) != sizeof(EventId))
                throw new ArgumentException($"Argument requires a value of type {type}", nameof(eventId));

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(SamplerId), &eventId).HandleError();
        }

        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
        public unsafe void Set(Image image)
        {
            var typeName = TypeName;

            if (typeName != null)
            {
                switch (image.Type)
                {
                    case MemObjectType.MemObjectImage1D when typeName != "image1d_t":
                        throw new ArgumentException("Argument requires a 1D image", nameof(image));

                    case MemObjectType.MemObjectImage1DBuffer when typeName != "image1d_buffer_t":
                        throw new ArgumentException("Argument requires a 1D image backed by a buffer", nameof(image));

                    case MemObjectType.MemObjectImage1DArray when typeName != "image1d_array_t":
                        throw new ArgumentException("Argument requires a 1D image array", nameof(image));

                    case MemObjectType.MemObjectImage2D when typeName != "image2d_t":
                        throw new ArgumentException("Argument requires a 2D image", nameof(image));

                    case MemObjectType.MemObjectImage2DArray when typeName != "image2d_array_t":
                        throw new ArgumentException("Argument requires a 2D image array", nameof(image));

                    case MemObjectType.MemObjectImage3D when typeName != "image3d_t":
                        throw new ArgumentException("Argument requires a 3D image", nameof(image));
                }
            }

            var id = image.Id;
            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(ImageId), &id).HandleError();
        }

        public unsafe void SetId(ImageId imageId)
        {
            var type = Type;

            if (type != null && typeof(Image).IsAssignableFrom(type))
                throw new ArgumentException($"Argument requires a value of type {type}", nameof(imageId));

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(ImageId), &imageId).HandleError();
        }

        public unsafe void Set(Sampler sampler)
        {
            var type = Type;

            if (type != null && type != typeof(Sampler) && Marshal.SizeOf(type) != sizeof(SamplerId))
                throw new ArgumentException($"Argument requires a value of type {type}", nameof(sampler));

            var id = sampler.Id;
            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(SamplerId), &id).HandleError();
        }

        public unsafe void SetId(SamplerId samplerId)
        {
            var type = Type;

            if (type != null && type != typeof(Sampler) && Marshal.SizeOf(type) != sizeof(SamplerId))
                throw new ArgumentException($"Argument requires a value of type {type}", nameof(samplerId));

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(SamplerId), &samplerId).HandleError();
        }

        public unsafe void Set<T>(T value) where T : unmanaged
        {
            var type = Type;

            if (type != null && !IsCompatibleType<T>(type))
            {
                var updated = false;

                if (type == typeof(sbyte))
                    updated = SetSByte(value);
                else if (type == typeof(byte))
                    updated = SetByte(value);
                else if (type == typeof(short))
                    updated = SetShort(value);
                else if (type == typeof(ushort))
                    updated = SetUShort(value);
                else if (type == typeof(int))
                    updated = SetInt(value);
                else if (type == typeof(uint))
                    updated = SetUInt(value);
                else if (type == typeof(long))
                    updated = SetLong(value);
                else if (type == typeof(ulong))
                    updated = SetULong(value);
                else if (type == typeof(float))
                    updated = SetFloat(value);
                else if (type == typeof(double))
                    updated = SetDouble(value);

                if (updated)
                    return;

                SetValue(value, type);
            }

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) sizeof(T), &value).HandleError();
        }

        private unsafe void SetValue<T>(T value, Type type) where T : unmanaged
        {
            object corrected;

            try
            {
                corrected = Convert.ChangeType(value, type);
            }
            catch
            {
                throw new ArgumentException($"Argument requires a value of type {type}", nameof(value));
            }
            
            var handle = GCHandle.Alloc(corrected, GCHandleType.Pinned);

            var errorCode = Library.clSetKernelArgUnsafe(Kernel, Index,
                (UIntPtr) Marshal.SizeOf(type), handle.AddrOfPinnedObject().ToPointer());

            handle.Free();
            errorCode.HandleError();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLength(int length)
        {
            SetLength((ulong) length);
        }

        public unsafe void SetLength(ulong length)
        {
            if (AddressQualifier != KernelArgAddressQualifier.KernelArgAddressLocal)
                throw new InvalidOperationException("Argument length can only be set for local arguments");

            Library.clSetKernelArgUnsafe(Kernel, Index, (UIntPtr) length, null).HandleError();
        }

        private static unsafe bool IsCompatibleType<T>(Type expectedType) where T : unmanaged
        {
            var providedType = typeof(T);

            if (providedType == expectedType)
                return true;

            if (WellKnownTypes.Contains(providedType) && WellKnownTypes.Contains(expectedType))
                return false;

            return sizeof(T) == Marshal.SizeOf(expectedType);
        }

        private static unsafe bool IsCompatibleBufferType<T>(Type expectedType) where T : unmanaged
        {
            if (!expectedType.IsGenericType || expectedType.GetGenericTypeDefinition() != typeof(Buffer<>))
                return false;

            var providedGeneric = typeof(T);
            var expectedGeneric = expectedType.GenericTypeArguments[0];

            if (WellKnownTypes.Contains(providedGeneric) && WellKnownTypes.Contains(expectedGeneric))
                return false;

            return sizeof(T) == Marshal.SizeOf(expectedGeneric);
        }
    }
}
