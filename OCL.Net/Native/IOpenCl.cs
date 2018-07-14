using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using AdvancedDLSupport;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net.Native
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IOpenCl : IDisposable
    {
        ErrorCode clGetPlatformIDs(
            uint numEntries,
            [Out] PlatformId[] platforms,
            out uint numPlatforms);

        [NativeSymbol(nameof(clGetPlatformIDs))]
        unsafe ErrorCode clGetPlatformIDsUnsafe(
            uint numEntries,
            [Out] PlatformId* platforms,
            [Out] uint* numPlatforms);

        ErrorCode clGetPlatformInfo(
            PlatformId platform,
            PlatformInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetPlatformInfo))]
        unsafe ErrorCode clGetPlatformInfoUnsafe(
            PlatformId platform,
            PlatformInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        ErrorCode clGetDeviceIDs(
            PlatformId platform,
            DeviceType deviceType,
            uint numEntries,
            [Out] DeviceId[] devices,
            out uint numDevices);

        [NativeSymbol(nameof(clGetDeviceIDs))]
        unsafe ErrorCode clGetDeviceIDsUnsafe(
            PlatformId platform,
            DeviceType deviceType,
            uint numEntries,
            [Out] DeviceId* devices,
            [Out] uint* numDevices);

        ErrorCode clGetDeviceInfo(
            DeviceId device,
            DeviceInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetDeviceInfo))]
        unsafe ErrorCode clGetDeviceInfoUnsafe(
            DeviceId device,
            DeviceInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        ErrorCode clCreateSubDevices(
            DeviceId inDevice,
            [In] IntPtr[] properties,
            uint numDevices,
            [Out] DeviceId[] outDevices,
            out uint numDevicesRet);

        [NativeSymbol(nameof(clCreateSubDevices))]
        unsafe ErrorCode clCreateSubDevicesUnsafe(
            DeviceId inDevice,
            [In] IntPtr* properties,
            uint numDevices,
            [Out] DeviceId* outDevices,
            [Out] uint* numDevicesRet);

        ErrorCode clRetainDevice(DeviceId device);

        ErrorCode clReleaseDevice(DeviceId device);

        ContextId clCreateContext(
            [In] IntPtr[] properties,
            uint numDevices,
            [In] DeviceId[] devices,
            ContextNotify pfnNotify,
            [In] IntPtr userData,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateContext))]
        unsafe ContextId clCreateContextUnsafe(
            [In] IntPtr* properties,
            uint numDevices,
            [In] DeviceId* devices,
            ContextNotifyUnsafe pfnNotify,
            [In] void* userData,
            [Out] ErrorCode* errcodeRet);

        ContextId clCreateContextFromType(
            [In] IntPtr[] properties,
            DeviceType deviceType,
            ContextNotify pfnNotify,
            [In] IntPtr userData,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateContextFromType))]
        unsafe ContextId clCreateContextFromTypeUnsafe(
            [In] IntPtr* properties,
            DeviceType deviceType,
            ContextNotifyUnsafe pfnNotify,
            [In] void* userData,
            [Out] ErrorCode* errcodeRet);

        ErrorCode clRetainContext(ContextId context);

        ErrorCode clReleaseContext(ContextId context);

        ErrorCode clGetContextInfo(
            ContextId context,
            ContextInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetContextInfo))]
        unsafe ErrorCode clGetContextInfoUnsafe(
            ContextId context,
            ContextInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        CommandQueueId clCreateCommandQueue(
            ContextId context,
            DeviceId device,
            CommandQueueProperties properties,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateCommandQueue))]
        unsafe CommandQueueId clCreateCommandQueueUnsafe(
            ContextId context,
            DeviceId device,
            CommandQueueProperties properties,
            [Out] ErrorCode* errcodeRet);

        ErrorCode clRetainCommandQueue(CommandQueueId commandQueue);

        ErrorCode clReleaseCommandQueue(CommandQueueId commandQueue);

        ErrorCode clGetCommandQueueInfo(
            CommandQueueId commandQueue,
            CommandQueueInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetCommandQueueInfo))]
        unsafe ErrorCode clGetCommandQueueInfoUnsafe(
            CommandQueueId commandQueue,
            CommandQueueInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        BufferId clCreateBuffer(
            ContextId context,
            MemFlags flags,
            UIntPtr size,
            IntPtr hostPtr,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateBuffer))]
        unsafe BufferId clCreateBufferUnsafe(
            ContextId context,
            MemFlags flags,
            UIntPtr size,
            void* hostPtr,
            [Out] ErrorCode* errcodeRet);

        BufferId clCreateSubBuffer(
            BufferId buffer,
            MemFlags flags,
            BufferCreateType bufferCreateType,
            [In] IntPtr bufferCreateInfo,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateSubBuffer))]
        unsafe BufferId clCreateSubBufferUnsafe(
            BufferId buffer,
            MemFlags flags,
            BufferCreateType bufferCreateType,
            [In] void* bufferCreateInfo,
            [Out] ErrorCode* errcodeRet);

        ErrorCode clEnqueueReadBuffer(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingRead,
            UIntPtr offset,
            UIntPtr size,
            [Out] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueReadBuffer(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingRead,
            UIntPtr offset,
            UIntPtr size,
            [Out] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueReadBuffer))]
        unsafe ErrorCode clEnqueueReadBufferUnsafe(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingRead,
            UIntPtr offset,
            UIntPtr size,
            [Out] void* ptr,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueWriteBuffer(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingWrite,
            UIntPtr offset,
            UIntPtr size,
            [In] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueWriteBuffer(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingWrite,
            UIntPtr offset,
            UIntPtr size,
            [In] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueWriteBuffer))]
        unsafe ErrorCode clEnqueueWriteBufferUnsafe(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingWrite,
            UIntPtr offset,
            UIntPtr size,
            [In] void* ptr,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueReadBufferRect(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingRead,
            [In] UIntPtr[] bufferOrigin,
            [In] UIntPtr[] hostOrigin,
            [In] UIntPtr[] region,
            UIntPtr bufferRowPitch,
            UIntPtr bufferSlicePitch,
            UIntPtr hostRowPitch,
            UIntPtr hostSlicePitch,
            [Out] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueReadBufferRect(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingRead,
            [In] UIntPtr[] bufferOrigin,
            [In] UIntPtr[] hostOrigin,
            [In] UIntPtr[] region,
            UIntPtr bufferRowPitch,
            UIntPtr bufferSlicePitch,
            UIntPtr hostRowPitch,
            UIntPtr hostSlicePitch,
            [Out] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueReadBufferRect))]
        unsafe ErrorCode clEnqueueReadBufferRectUnsafe(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingRead,
            [In] UIntPtr* bufferOrigin,
            [In] UIntPtr* hostOrigin,
            [In] UIntPtr* region,
            UIntPtr bufferRowPitch,
            UIntPtr bufferSlicePitch,
            UIntPtr hostRowPitch,
            UIntPtr hostSlicePitch,
            [Out] void* ptr,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueWriteBufferRect(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingWrite,
            [In] UIntPtr[] bufferOrigin,
            [In] UIntPtr[] hostOrigin,
            [In] UIntPtr[] region,
            UIntPtr bufferRowPitch,
            UIntPtr bufferSlicePitch,
            UIntPtr hostRowPitch,
            UIntPtr hostSlicePitch,
            [Out] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueWriteBufferRect(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingWrite,
            [In] UIntPtr[] bufferOrigin,
            [In] UIntPtr[] hostOrigin,
            [In] UIntPtr[] region,
            UIntPtr bufferRowPitch,
            UIntPtr bufferSlicePitch,
            UIntPtr hostRowPitch,
            UIntPtr hostSlicePitch,
            [Out] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueWriteBufferRect))]
        unsafe ErrorCode clEnqueueWriteBufferRectUnsafe(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingWrite,
            [In] UIntPtr* bufferOrigin,
            [In] UIntPtr* hostOrigin,
            [In] UIntPtr* region,
            UIntPtr bufferRowPitch,
            UIntPtr bufferSlicePitch,
            UIntPtr hostRowPitch,
            UIntPtr hostSlicePitch,
            [Out] void* ptr,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueFillBuffer(
            CommandQueueId commandQueue,
            BufferId buffer,
            [In] IntPtr pattern,
            UIntPtr patternSize,
            UIntPtr offset,
            UIntPtr size,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueFillBuffer(
            CommandQueueId commandQueue,
            BufferId buffer,
            [In] IntPtr pattern,
            UIntPtr patternSize,
            UIntPtr offset,
            UIntPtr size,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueFillBuffer))]
        unsafe ErrorCode clEnqueueFillBufferUnsafe(
            CommandQueueId commandQueue,
            BufferId buffer,
            [In] void* pattern,
            UIntPtr patternSize,
            UIntPtr offset,
            UIntPtr size,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueCopyBuffer(
            CommandQueueId commandQueue,
            BufferId srcBuffer,
            BufferId dstBuffer,
            UIntPtr srcOffset,
            UIntPtr dstOffset,
            UIntPtr size,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueCopyBuffer(
            CommandQueueId commandQueue,
            BufferId srcBuffer,
            BufferId dstBuffer,
            UIntPtr srcOffset,
            UIntPtr dstOffset,
            UIntPtr size,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueCopyBuffer))]
        unsafe ErrorCode clEnqueueCopyBufferUnsafe(
            CommandQueueId commandQueue,
            BufferId srcBuffer,
            BufferId dstBuffer,
            UIntPtr srcOffset,
            UIntPtr dstOffset,
            UIntPtr size,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueCopyBufferRect(
            CommandQueueId commandQueue,
            BufferId srcBuffer,
            BufferId dstBuffer,
            [In] UIntPtr[] srcOrigin,
            [In] UIntPtr[] dstOrigin,
            [In] UIntPtr[] region,
            UIntPtr srcRowPitch,
            UIntPtr srcSlicePitch,
            UIntPtr dstRowPitch,
            UIntPtr dstSlicePitch,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueCopyBufferRect(
            CommandQueueId commandQueue,
            BufferId srcBuffer,
            BufferId dstBuffer,
            [In] UIntPtr[] srcOrigin,
            [In] UIntPtr[] dstOrigin,
            [In] UIntPtr[] region,
            UIntPtr srcRowPitch,
            UIntPtr srcSlicePitch,
            UIntPtr dstRowPitch,
            UIntPtr dstSlicePitch,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueCopyBufferRect))]
        unsafe ErrorCode clEnqueueCopyBufferRectUnsafe(
            CommandQueueId commandQueue,
            BufferId srcBuffer,
            BufferId dstBuffer,
            [In] UIntPtr* srcOrigin,
            [In] UIntPtr* dstOrigin,
            [In] UIntPtr* region,
            UIntPtr srcRowPitch,
            UIntPtr srcSlicePitch,
            UIntPtr dstRowPitch,
            UIntPtr dstSlicePitch,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        IntPtr clEnqueueMapBuffer(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingMap,
            MapFlags mapFlags,
            UIntPtr offset,
            UIntPtr size,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event,
            out ErrorCode errcodeRet);

        IntPtr clEnqueueMapBuffer(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingMap,
            MapFlags mapFlags,
            UIntPtr offset,
            UIntPtr size,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clEnqueueMapBuffer))]
        unsafe void* clEnqueueMapBufferUnsafe(
            CommandQueueId commandQueue,
            BufferId buffer,
            bool blockingMap,
            MapFlags mapFlags,
            UIntPtr offset,
            UIntPtr size,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event,
            [Out] ErrorCode* errcodeRet);

        ImageId clCreateImage(
            ContextId context,
            MemFlags flags,
            [In] ref ImageFormat imageFormat,
            [In] ref ImageDescription imageDesc,
            IntPtr hostPtr,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateImage))]
        unsafe ImageId clCreateImageUnsafe(
            ContextId context,
            MemFlags flags,
            [In] ImageFormat* imageFormat,
            [In] ImageDescription* imageDesc,
            void* hostPtr,
            [Out] ErrorCode* errcodeRet);

        ErrorCode clGetSupportedImageFormats(
            ContextId context,
            MemFlags flags,
            MemObjectType imageType,
            uint numEntries,
            [Out] ImageFormat[] imageFormats,
            out uint numImageFormats);

        [NativeSymbol(nameof(clGetSupportedImageFormats))]
        unsafe ErrorCode clGetSupportedImageFormatsUnsafe(
            ContextId context,
            MemFlags flags,
            MemObjectType imageType,
            uint numEntries,
            [Out] ImageFormat* imageFormats,
            [Out] uint* numImageFormats);

        ErrorCode clEnqueueReadImage(
            CommandQueueId commandQueue,
            ImageId image,
            bool blockingRead,
            [In] UIntPtr[] origin,
            [In] UIntPtr[] region,
            UIntPtr rowPitch,
            UIntPtr slicePitch,
            [Out] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueReadImage(
            CommandQueueId commandQueue,
            ImageId image,
            bool blockingRead,
            [In] UIntPtr[] origin,
            [In] UIntPtr[] region,
            UIntPtr rowPitch,
            UIntPtr slicePitch,
            [Out] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueReadImage))]
        unsafe ErrorCode clEnqueueReadImageUnsafe(
            CommandQueueId commandQueue,
            ImageId image,
            bool blockingRead,
            [In] UIntPtr* origin,
            [In] UIntPtr* region,
            UIntPtr rowPitch,
            UIntPtr slicePitch,
            [Out] void* ptr,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueWriteImage(
            CommandQueueId commandQueue,
            ImageId image,
            bool blockingWrite,
            [In] UIntPtr[] origin,
            [In] UIntPtr[] region,
            UIntPtr rowPitch,
            UIntPtr slicePitch,
            [In] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueWriteImage(
            CommandQueueId commandQueue,
            ImageId image,
            bool blockingWrite,
            [In] UIntPtr[] origin,
            [In] UIntPtr[] region,
            UIntPtr rowPitch,
            UIntPtr slicePitch,
            [In] IntPtr ptr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueWriteImage))]
        unsafe ErrorCode clEnqueueWriteImageUnsafe(
            CommandQueueId commandQueue,
            ImageId image,
            bool blockingWrite,
            [In] UIntPtr* origin,
            [In] UIntPtr* region,
            UIntPtr rowPitch,
            UIntPtr slicePitch,
            [In] void* ptr,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueFillImage(
            CommandQueueId commandQueue,
            ImageId image,
            [In] IntPtr fillColor,
            [In] UIntPtr[] origin,
            [In] UIntPtr[] region,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueFillImage(
            CommandQueueId commandQueue,
            ImageId image,
            [In] IntPtr fillColor,
            [In] UIntPtr[] origin,
            [In] UIntPtr[] region,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueFillImage))]
        unsafe ErrorCode clEnqueueFillImageUnsafe(
            CommandQueueId commandQueue,
            ImageId image,
            [In] void* fillColor,
            [In] UIntPtr* origin,
            [In] UIntPtr* region,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueCopyImage(
            CommandQueueId commandQueue,
            ImageId srcImage,
            ImageId dstImage,
            [In] UIntPtr[] srcOrigin,
            [In] UIntPtr[] dstOrigin,
            [In] UIntPtr[] region,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueCopyImage(
            CommandQueueId commandQueue,
            ImageId srcImage,
            ImageId dstImage,
            [In] UIntPtr[] srcOrigin,
            [In] UIntPtr[] dstOrigin,
            [In] UIntPtr[] region,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueCopyImage))]
        unsafe ErrorCode clEnqueueCopyImageUnsafe(
            CommandQueueId commandQueue,
            ImageId srcImage,
            ImageId dstImage,
            [In] UIntPtr* srcOrigin,
            [In] UIntPtr* dstOrigin,
            [In] UIntPtr* region,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueCopyImageToBuffer(
            CommandQueueId commandQueue,
            ImageId srcImage,
            BufferId dstBuffer,
            [In] UIntPtr[] srcOrigin,
            [In] UIntPtr[] region,
            UIntPtr dstOffset,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueCopyImageToBuffer(
            CommandQueueId commandQueue,
            ImageId srcImage,
            BufferId dstBuffer,
            [In] UIntPtr[] srcOrigin,
            [In] UIntPtr[] region,
            UIntPtr dstOffset,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueCopyImageToBuffer))]
        unsafe ErrorCode clEnqueueCopyImageToBufferUnsafe(
            CommandQueueId commandQueue,
            ImageId srcImage,
            BufferId dstBuffer,
            [In] UIntPtr* srcOrigin,
            [In] UIntPtr* region,
            UIntPtr dstOffset,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueCopyBufferToImage(
            CommandQueueId commandQueue,
            BufferId srcBuffer,
            ImageId dstImage,
            UIntPtr srcOffset,
            [In] UIntPtr[] dstOrigin,
            [In] UIntPtr[] region,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueCopyBufferToImage(
            CommandQueueId commandQueue,
            BufferId srcBuffer,
            ImageId dstImage,
            UIntPtr srcOffset,
            [In] UIntPtr[] dstOrigin,
            [In] UIntPtr[] region,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueCopyBufferToImage))]
        unsafe ErrorCode clEnqueueCopyBufferToImageUnsafe(
            CommandQueueId commandQueue,
            BufferId srcBuffer,
            ImageId dstImage,
            UIntPtr srcOffset,
            [In] UIntPtr* dstOrigin,
            [In] UIntPtr* region,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        IntPtr clEnqueueMapImage(
            CommandQueueId commandQueue,
            ImageId image,
            bool blockingMap,
            MapFlags mapFlags,
            [In] UIntPtr[] origin,
            [In] UIntPtr[] region,
            out UIntPtr imageRowPitch,
            out UIntPtr imageSlicePitch,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event,
            out ErrorCode errcodeRet);

        IntPtr clEnqueueMapImage(
            CommandQueueId commandQueue,
            ImageId image,
            bool blockingMap,
            MapFlags mapFlags,
            [In] UIntPtr[] origin,
            [In] UIntPtr[] region,
            out UIntPtr imageRowPitch,
            out UIntPtr imageSlicePitch,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clEnqueueMapImage))]
        unsafe void* clEnqueueMapImageUnsafe(
            CommandQueueId commandQueue,
            ImageId image,
            bool blockingMap,
            MapFlags mapFlags,
            [In] UIntPtr* origin,
            [In] UIntPtr* region,
            [Out] UIntPtr* imageRowPitch,
            [Out] UIntPtr* imageSlicePitch,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event,
            [Out] ErrorCode* errcodeRet);

        ErrorCode clEnqueueUnmapMemObject(
            CommandQueueId commandQueue,
            MemoryId memObj,
            IntPtr mappedPtr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueUnmapMemObject(
            CommandQueueId commandQueue,
            MemoryId memObj,
            IntPtr mappedPtr,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueUnmapMemObject))]
        unsafe ErrorCode clEnqueueUnmapMemObjectUnsafe(
            CommandQueueId commandQueue,
            MemoryId memObj,
            [In] void* mappedPtr,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueMigrateMemObjects(
            CommandQueueId commandQueue,
            uint numMemObjects,
            [In] MemoryId[] memObjects,
            MemMigrationFlags flags,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueMigrateMemObjects(
            CommandQueueId commandQueue,
            uint numMemObjects,
            [In] MemoryId[] memObjects,
            MemMigrationFlags flags,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueMigrateMemObjects))]
        unsafe ErrorCode clEnqueueMigrateMemObjectsUnsafe(
            CommandQueueId commandQueue,
            uint numMemObjects,
            [In] MemoryId* memObjects,
            MemMigrationFlags flags,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clGetMemObjectInfo(
            MemoryId memObj,
            MemInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetMemObjectInfo))]
        unsafe ErrorCode clGetMemObjectInfoUnsafe(
            MemoryId memObj,
            MemInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        ErrorCode clGetImageInfo(
            ImageId image,
            ImageInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetImageInfo))]
        unsafe ErrorCode clGetImageInfoUnsafe(
            ImageId image,
            ImageInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        ErrorCode clRetainMemObject(MemoryId memory);

        ErrorCode clReleaseMemObject(MemoryId memory);

        ErrorCode clSetMemObjectDestructorCallback(
            MemoryId memObj,
            MemoryDestructorNotify pfnNotify,
            [In] IntPtr userData);

        [NativeSymbol(nameof(clSetMemObjectDestructorCallback))]
        unsafe ErrorCode clSetMemObjectDestructorCallbackUnsafe(
            MemoryId memObj,
            MemoryDestructorNotifyUnsafe pfnNotify,
            [In] void* userData);

        SamplerId clCreateSampler(
            ContextId context,
            bool normalizedCoords,
            AddressingMode addressingMode,
            FilterMode filterMode,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateSampler))]
        unsafe SamplerId clCreateSamplerUnsafe(
            ContextId context,
            bool normalizedCoords,
            AddressingMode addressingMode,
            FilterMode filterMode,
            [Out] ErrorCode* errcodeRet);

        ErrorCode clRetainSampler(SamplerId sampler);

        ErrorCode clReleaseSampler(SamplerId sampler);

        ErrorCode clGetSamplerInfo(
            SamplerId sampler,
            SamplerInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetSamplerInfo))]
        unsafe ErrorCode clGetSamplerInfoUnsafe(
            SamplerId sampler,
            SamplerInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        ProgramId clCreateProgramWithSource(
            ContextId context,
            uint count,
            [In] string[] strings,
            [In] UIntPtr[] lengths,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateProgramWithSource))]
        unsafe ProgramId clCreateProgramWithSourceUnsafe(
            ContextId context,
            uint count,
            [In] byte** strings,
            [In] UIntPtr* lengths,
            [Out] ErrorCode* errcodeRet);

        ProgramId clCreateProgramWithBinary(
            ContextId context,
            uint numDevices,
            [In] DeviceId[] deviceList,
            [In] UIntPtr[] lengths,
            [In] IntPtr[] binaries,
            [Out] ErrorCode[] binaryStatus,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateProgramWithBinary))]
        unsafe ProgramId clCreateProgramWithBinaryUnsafe(
            ContextId context,
            uint numDevices,
            [In] DeviceId* deviceList,
            [In] UIntPtr* lengths,
            [In] byte** binaries,
            [Out] ErrorCode* binaryStatus,
            [Out] ErrorCode* errcodeRet);

        ProgramId clCreateProgramWithBuiltInKernels(
            ContextId context,
            uint numDevices,
            [In] DeviceId[] deviceList,
            [In] string kernelNames,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateProgramWithBuiltInKernels))]
        unsafe ProgramId clCreateProgramWithBuiltInKernelsUnsafe(
            ContextId context,
            uint numDevices,
            [In] DeviceId* deviceList,
            [In] string kernelNames,
            [Out] ErrorCode* errcodeRet);

        ErrorCode clRetainProgram(ProgramId program);

        ErrorCode clReleaseProgram(ProgramId program);

        ErrorCode clBuildProgram(
            ProgramId program,
            uint numDevices,
            [In] DeviceId[] deviceList,
            [In] string options,
            ProgramBuildNotify pfnNotify,
            [In] IntPtr userData);

        [NativeSymbol(nameof(clBuildProgram))]
        unsafe ErrorCode clBuildProgramUnsafe(
            ProgramId program,
            uint numDevices,
            [In] DeviceId* deviceList,
            [In] string options,
            ProgramBuildNotifyUnsafe pfnNotify,
            [In] void* userData);

        ErrorCode clCompileProgram(
            ProgramId program,
            uint numDevices,
            [In] DeviceId[] deviceList,
            [In] string options,
            uint numInputHeaders,
            [In] ProgramId[] inputHeaders,
            [In] string[] headerIncludeNames,
            ProgramBuildNotify pfnNotify,
            [In] IntPtr userData);

        [NativeSymbol(nameof(clCompileProgram))]
        unsafe ErrorCode clCompileProgramUnsafe(
            ProgramId program,
            uint numDevices,
            [In] DeviceId* deviceList,
            [In] string options,
            uint numInputHeaders,
            [In] ProgramId* inputHeaders,
            [In] byte** headerIncludeNames,
            ProgramBuildNotifyUnsafe pfnNotify,
            [In] void* userData);

        ProgramId clLinkProgram(
            ContextId context,
            uint numDevices,
            [In] DeviceId[] deviceList,
            [In] string options,
            uint numInputPrograms,
            [In] ProgramId[] inputPrograms,
            ProgramBuildNotify pfnNotify,
            [In] IntPtr userData,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clLinkProgram))]
        unsafe ProgramId clLinkProgramUnsafe(
            ContextId context,
            uint numDevices,
            [In] DeviceId* deviceList,
            [In] string options,
            uint numInputPrograms,
            [In] ProgramId* inputPrograms,
            ProgramBuildNotifyUnsafe pfnNotify,
            [In] void* userData,
            [Out] ErrorCode* errcodeRet);

        ErrorCode clUnloadPlatformCompiler(PlatformId platform);

        ErrorCode clGetProgramInfo(
            ProgramId program,
            ProgramInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetProgramInfo))]
        unsafe ErrorCode clGetProgramInfoUnsafe(
            ProgramId program,
            ProgramInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        ErrorCode clGetProgramBuildInfo(
            ProgramId program,
            DeviceId device,
            ProgramBuildInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetProgramBuildInfo))]
        unsafe ErrorCode clGetProgramBuildInfoUnsafe(
            ProgramId program,
            DeviceId device,
            ProgramBuildInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        KernelId clCreateKernel(
            ProgramId program,
            [In] string kernelName,
            out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateKernel))]
        unsafe KernelId clCreateKernelUnsafe(
            ProgramId program,
            [In] string kernelName,
            [Out] ErrorCode* errcodeRet);

        ErrorCode clCreateKernelsInProgram(
            ProgramId program,
            uint numKernels,
            [Out] KernelId[] kernels,
            out uint numKernelsRet);

        [NativeSymbol(nameof(clCreateKernelsInProgram))]
        unsafe ErrorCode clCreateKernelsInProgramUnsafe(
            ProgramId program,
            uint numKernels,
            [Out] KernelId* kernels,
            [Out] uint* numKernelsRet);

        ErrorCode clRetainKernel(KernelId kernel);

        ErrorCode clReleaseKernel(KernelId kernel);

        ErrorCode clSetKernelArg(
            KernelId kernel,
            uint argIndex,
            UIntPtr argSize,
            [In] IntPtr argValue);

        [NativeSymbol(nameof(clSetKernelArg))]
        unsafe ErrorCode clSetKernelArgUnsafe(
            KernelId kernel,
            uint argIndex,
            UIntPtr argSize,
            [In] void* argValue);

        ErrorCode clGetKernelInfo(
            KernelId kernel,
            KernelInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetKernelInfo))]
        unsafe ErrorCode clGetKernelInfoUnsafe(
            KernelId kernel,
            KernelInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        ErrorCode clGetKernelArgInfo(
            KernelId kernel,
            uint argIndex,
            KernelArgInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetKernelArgInfo))]
        unsafe ErrorCode clGetKernelArgInfoUnsafe(
            KernelId kernel,
            uint argIndex,
            KernelArgInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        ErrorCode clGetKernelWorkGroupInfo(
            KernelId kernel,
            DeviceId device,
            KernelWorkGroupInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetKernelWorkGroupInfo))]
        unsafe ErrorCode clGetKernelWorkGroupInfoUnsafe(
            KernelId kernel,
            DeviceId device,
            KernelWorkGroupInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        ErrorCode clEnqueueNDRangeKernel(
            CommandQueueId commandQueue,
            KernelId kernel,
            uint workDim,
            [In] UIntPtr[] globalWorkOffset,
            [In] UIntPtr[] globalWorkSize,
            [In] UIntPtr[] localWorkSize,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueNDRangeKernel(
            CommandQueueId commandQueue,
            KernelId kernel,
            uint workDim,
            [In] UIntPtr[] globalWorkOffset,
            [In] UIntPtr[] globalWorkSize,
            [In] UIntPtr[] localWorkSize,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueNDRangeKernel))]
        unsafe ErrorCode clEnqueueNDRangeKernelUnsafe(
            CommandQueueId commandQueue,
            KernelId kernel,
            uint workDim,
            [In] UIntPtr* globalWorkOffset,
            [In] UIntPtr* globalWorkSize,
            [In] UIntPtr* localWorkSize,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueTask(
            CommandQueueId queue,
            KernelId kernel,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueTask(
            CommandQueueId queue,
            KernelId kernel,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueTask))]
        unsafe ErrorCode clEnqueueTaskUnsafe(
            CommandQueueId queue,
            KernelId kernel,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueNativeKernel(
            CommandQueueId commandQueue,
            NativeKernel userFunc,
            [In] IntPtr args,
            UIntPtr cbArgs,
            uint numMemObjects,
            [In] MemoryId[] memList,
            [In] IntPtr[] argsMemLoc,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueNativeKernel(
            CommandQueueId commandQueue,
            NativeKernel userFunc,
            [In] IntPtr args,
            UIntPtr cbArgs,
            uint numMemObjects,
            [In] MemoryId[] memList,
            [In] IntPtr[] argsMemLoc,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueNativeKernel))]
        unsafe ErrorCode clEnqueueNativeKernelUnsafe(
            CommandQueueId commandQueue,
            NativeKernelUnsafe userFunc,
            [In] void* args,
            UIntPtr cbArgs,
            uint numMemObjects,
            [In] MemoryId* memList,
            [In] void** argsMemLoc,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        EventId clCreateUserEvent(ContextId context, out ErrorCode errcodeRet);

        [NativeSymbol(nameof(clCreateUserEvent))]
        unsafe EventId clCreateUserEventUnsafe(ContextId context, [Out] ErrorCode* errcodeRet);

        ErrorCode clSetUserEventStatus(EventId @event, CommandExecutionStatus executionStatus);

        ErrorCode clWaitForEvents(
            uint numEvents,
            [In] EventId[] eventList);

        [NativeSymbol(nameof(clWaitForEvents))]
        unsafe ErrorCode clWaitForEventsUnsafe(
            uint numEvents,
            [In] EventId* eventList);

        ErrorCode clGetEventInfo(
            EventId @event,
            EventInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetEventInfo))]
        unsafe ErrorCode clGetEventInfoUnsafe(
            EventId @event,
            EventInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        ErrorCode clSetEventCallback(
            EventId @event,
            CommandExecutionStatus commandExecCallbackType,
            EventNotify pfnEventNotify,
            [In] IntPtr userData);

        [NativeSymbol(nameof(clSetEventCallback))]
        unsafe ErrorCode clSetEventCallbackUnsafe(
            EventId @event,
            CommandExecutionStatus commandExecCallbackType,
            EventNotifyUnsafe pfnEventNotify,
            [In] void* userData);

        ErrorCode clRetainEvent(EventId @event);

        ErrorCode clReleaseEvent(EventId @event);

        ErrorCode clEnqueueMarkerWithWaitList(
            CommandQueueId commandQueue,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueMarkerWithWaitList(
            CommandQueueId commandQueue,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueMarkerWithWaitList))]
        unsafe ErrorCode clEnqueueMarkerWithWaitListUnsafe(
            CommandQueueId commandQueue,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clEnqueueBarrierWithWaitList(
            CommandQueueId commandQueue,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            out EventId @event);

        ErrorCode clEnqueueBarrierWithWaitList(
            CommandQueueId commandQueue,
            uint numEventsInWaitList,
            [In] EventId[] eventWaitList,
            [Out] IntPtr @event);

        [NativeSymbol(nameof(clEnqueueBarrierWithWaitList))]
        unsafe ErrorCode clEnqueueBarrierWithWaitListUnsafe(
            CommandQueueId commandQueue,
            uint numEventsInWaitList,
            [In] EventId* eventWaitList,
            [Out] EventId* @event);

        ErrorCode clGetEventProfilingInfo(
            EventId @event,
            ProfilingInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        [NativeSymbol(nameof(clGetEventProfilingInfo))]
        unsafe ErrorCode clGetEventProfilingInfoUnsafe(
            EventId @event,
            ProfilingInfo paramName,
            UIntPtr paramValueSize,
            [Out] void* paramValue,
            [Out] UIntPtr* paramValueSizeRet);

        ErrorCode clFlush(CommandQueueId commandQueue);

        ErrorCode clFinish(CommandQueueId commandQueue);
    }
}
