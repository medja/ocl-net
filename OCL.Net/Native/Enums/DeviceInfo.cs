namespace OCL.Net.Native.Enums
{
    public enum DeviceInfo : uint
    {
        DeviceType                       = 0x1000,
        DeviceVendorId                   = 0x1001,
        DeviceMaxComputeUnits            = 0x1002,
        DeviceMaxWorkItemDimensions      = 0x1003,
        DeviceMaxWorkGroupSize           = 0x1004,
        DeviceMaxWorkItemSizes           = 0x1005,
        DevicePreferredVectorWidthChar   = 0x1006,
        DevicePreferredVectorWidthShort  = 0x1007,
        DevicePreferredVectorWidthInt    = 0x1008,
        DevicePreferredVectorWidthLong   = 0x1009,
        DevicePreferredVectorWidthFloat  = 0x100A,
        DevicePreferredVectorWidthDouble = 0x100B,
        DeviceMaxClockFrequency          = 0x100C,
        DeviceAddressBits                = 0x100D,
        DeviceMaxReadImageArgs           = 0x100E,
        DeviceMaxWriteImageArgs          = 0x100F,
        DeviceMaxMemAllocSize            = 0x1010,
        DeviceImage2DMaxWidth            = 0x1011,
        DeviceImage2DMaxHeight           = 0x1012,
        DeviceImage3DMaxWidth            = 0x1013,
        DeviceImage3DMaxHeight           = 0x1014,
        DeviceImage3DMaxDepth            = 0x1015,
        DeviceImageSupport               = 0x1016,
        DeviceMaxParameterSize           = 0x1017,
        DeviceMaxSamplers                = 0x1018,
        DeviceMemBaseAddrAlign           = 0x1019,
        DeviceMinDataTypeAlignSize       = 0x101A,
        DeviceSingleFpConfig             = 0x101B,
        DeviceGlobalMemCacheType         = 0x101C,
        DeviceGlobalMemCachelineSize     = 0x101D,
        DeviceGlobalMemCacheSize         = 0x101E,
        DeviceGlobalMemSize              = 0x101F,
        DeviceMaxConstantBufferSize      = 0x1020,
        DeviceMaxConstantArgs            = 0x1021,
        DeviceLocalMemType               = 0x1022,
        DeviceLocalMemSize               = 0x1023,
        DeviceErrorCorrectionSupport     = 0x1024,
        DeviceProfilingTimerResolution   = 0x1025,
        DeviceEndianLittle               = 0x1026,
        DeviceAvailable                  = 0x1027,
        DeviceCompilerAvailable          = 0x1028,
        DeviceExecutionCapabilities      = 0x1029,
        DeviceQueueProperties            = 0x102A,
        DeviceName                       = 0x102B,
        DeviceVendor                     = 0x102C,
        DriverVersion                    = 0x102D,
        DeviceProfile                    = 0x102E,
        DeviceVersion                    = 0x102F,
        DeviceExtensions                 = 0x1030,
        DevicePlatform                   = 0x1031,
        DeviceDoubleFpConfig             = 0x1032,
        DeviceHalfFpConfig               = 0x1033,
        DevicePreferredVectorWidthHalf   = 0x1034,
        DeviceHostUnifiedMemory          = 0x1035,
        DeviceNativeVectorWidthChar      = 0x1036,
        DeviceNativeVectorWidthShort     = 0x1037,
        DeviceNativeVectorWidthInt       = 0x1038,
        DeviceNativeVectorWidthLong      = 0x1039,
        DeviceNativeVectorWidthFloat     = 0x103A,
        DeviceNativeVectorWidthDouble    = 0x103B,
        DeviceNativeVectorWidthHalf      = 0x103C,
        DeviceOpenClCVersion             = 0x103D,
        DeviceLinkerAvailable            = 0x103E,
        DeviceBuiltInKernels             = 0x103F,
        DeviceImageMaxBufferSize         = 0x1040,
        DeviceImageMaxArraySize          = 0x1041,
        DeviceParentDevice               = 0x1042,
        DevicePartitionMaxSubDevices     = 0x1043,
        DevicePartitionProperties        = 0x1044,
        DevicePartitionAffinityDomain    = 0x1045,
        DevicePartitionType              = 0x1046,
        DeviceReferenceCount             = 0x1047,
        DevicePreferredInteropUserSync   = 0x1048,
        DevicePrintfBufferSize           = 0x1049,
        DeviceImagePitchAlignment        = 0x104A,
        DeviceImageBaseAddressAlignment  = 0x104B
    }
}