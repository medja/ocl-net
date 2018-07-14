using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public sealed partial class Device
    {
        #region PartitionEqually

        public Device[] PartitionEqually(int groups)
        {
            if (groups <= 0)
                throw new ArgumentOutOfRangeException(nameof(groups), "At least one group is required");

            return PartitionEquallyInternal((uint) groups);
        }

        public Device[] PartitionEqually(uint groups)
        {
            if (groups == 0)
                throw new ArgumentOutOfRangeException(nameof(groups), "At least one group is required");

            return PartitionEquallyInternal(groups);
        }

        private Device[] PartitionEquallyInternal(uint groups)
        {
            Span<IntPtr> properties = stackalloc IntPtr[3];

            properties[0] = (IntPtr) DevicePartitionProperty.DevicePartitionEqually;
            properties[1] = (IntPtr) groups;
            properties[2] = IntPtr.Zero;

            return Partition(properties);
        }

        #endregion

        #region PartitionByCount

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Device[] PartitionByCount(params int[] counts)
        {
            return PartitionByCount((IEnumerable<int>) counts);
        }

        public Device[] PartitionByCount(IEnumerable<int> counts)
        {
            if (counts == null)
                throw new ArgumentNullException(nameof(counts));

            return PartitionByCountInternal(counts.Select(count =>
            {
                if (count <= 0)
                    throw new ArgumentException("All counts must be greater then 0", nameof(counts));

                return (uint) count;
            }).ToList());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Device[] PartitionByCount(params uint[] counts)
        {
            return PartitionByCount((IEnumerable<uint>) counts);
        }

        public Device[] PartitionByCount(IEnumerable<uint> counts)
        {
            if (counts == null)
                throw new ArgumentNullException(nameof(counts));

            if (!(counts is IList<uint> collection))
                collection = counts.ToList();

            if (collection.Any(count => count == 0))
                throw new ArgumentException("All counts must be greater then 0", nameof(counts));

            return PartitionByCountInternal(collection);
        }

        private Device[] PartitionByCountInternal(IList<uint> counts)
        {
            if (counts.Count == 0)
                throw new ArgumentException("At least one count is required", nameof(counts));

            var index = 0;
            var properties = new IntPtr[counts.Count + 3];

            properties[index++] = (IntPtr) DevicePartitionProperty.DevicePartitionByCounts;

            foreach (var count in counts)
                properties[index++] = (IntPtr) count;

            properties[index++] = (IntPtr) 0; // DevicePartitionByCountsListEnd
            properties[index] = (IntPtr) 0;

            return Partition(properties);
        }

        #endregion

        #region PartitionByCache

        public Device[] PartitionByCache(
            DeviceAffinityDomain affinityDomain = DeviceAffinityDomain.DeviceAffinityDomainNextPartitionable)
        {
            Span<IntPtr> properties = stackalloc IntPtr[3];

            properties[0] = (IntPtr) DevicePartitionProperty.DevicePartitionByAffinityDomain;
            properties[1] = (IntPtr) affinityDomain;
            properties[2] = IntPtr.Zero;

            return Partition(properties);
        }

        #endregion

        private unsafe Device[] Partition(Span<IntPtr> properties)
        {
            DeviceId[] deviceIds;

            fixed (IntPtr* propertiesPtr = properties)
            {
                uint count;
                Library.clCreateSubDevicesUnsafe(Id, propertiesPtr, 0, null, &count).HandleError();

                deviceIds = new DeviceId[count];

                fixed (DeviceId* deviceIdsPtr = deviceIds)
                    Library.clCreateSubDevicesUnsafe(Id, propertiesPtr, count, deviceIdsPtr, null).HandleError();
            }

            var devices = new Device[deviceIds.Length];

            for (var i = 0; i < deviceIds.Length; i++)
                devices[i] = FromId(Library, deviceIds[i]);

            return devices;
        }
    }
}
