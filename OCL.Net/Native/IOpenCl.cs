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

        ErrorCode clGetPlatformInfo(
            PlatformId platform,
            PlatformInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        ErrorCode clGetDeviceIDs(
            PlatformId platform,
            DeviceType deviceType,
            uint numEntries,
            [Out] DeviceId[] devices,
            out uint numDevices);

        ErrorCode clGetDeviceInfo(
            DeviceId device,
            DeviceInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        ErrorCode clCreateSubDevices(
            DeviceId inDevice,
            [In] IntPtr[] properties,
            uint numDevices,
            [Out] DeviceId[] outDevices,
            out uint numDevicesRet);

        ErrorCode clRetainDevice(DeviceId device);

        ErrorCode clReleaseDevice(DeviceId device);

        ContextId clCreateContext(
            [In] IntPtr[] properties,
            uint numDevices,
            [In] DeviceId[] devices,
            ContextNotify pfnNotify,
            [In] IntPtr userData,
            out ErrorCode errcodeRet);

        ContextId clCreateContextFromType(
            [In] IntPtr[] properties,
            DeviceType deviceType,
            ContextNotify pfnNotify,
            [In] IntPtr userData,
            out ErrorCode errcodeRet);

        ErrorCode clRetainContext(ContextId context);

        ErrorCode clReleaseContext(ContextId context);

        ErrorCode clGetContextInfo(
            ContextId context,
            ContextInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        CommandQueueId clCreateCommandQueue(
            ContextId context,
            DeviceId device,
            CommandQueueProperties properties,
            out ErrorCode errcodeRet);

        ErrorCode clRetainCommandQueue(CommandQueueId commandQueue);

        ErrorCode clReleaseCommandQueue(CommandQueueId commandQueue);

        ErrorCode clGetCommandQueueInfo(
            CommandQueueId commandQueue,
            CommandQueueInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        BufferId clCreateBuffer(
            ContextId context,
            MemFlags flags,
            UIntPtr size,
            IntPtr hostPtr,
            out ErrorCode errcodeRet);

        BufferId clCreateSubBuffer(
            BufferId buffer,
            MemFlags flags,
            BufferCreateType bufferCreateType,
            [In] IntPtr bufferCreateInfo,
            out ErrorCode errcodeRet);

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

        ImageId clCreateImage(
            ContextId context,
            MemFlags flags,
            [In] ref ImageFormat imageFormat,
            [In] ref ImageDescription imageDesc,
            IntPtr hostPtr,
            out ErrorCode errcodeRet);

        ErrorCode clGetSupportedImageFormats(
            ContextId context,
            MemFlags flags,
            MemObjectType imageType,
            uint numEntries,
            [Out] ImageFormat[] imageFormats,
            out uint numImageFormats);

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

        ErrorCode clGetMemObjectInfo(
            MemoryId memObj,
            MemInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        ErrorCode clGetImageInfo(
            ImageId image,
            ImageInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        ErrorCode clRetainMemObject(MemoryId memory);

        ErrorCode clReleaseMemObject(MemoryId memory);

        ErrorCode clSetMemObjectDestructorCallback(
            MemoryId memObj,
            MemoryDestructorNotify pfnNotify,
            [In] IntPtr userData);

        SamplerId clCreateSampler(
            ContextId context,
            bool normalizedCoords,
            AddressingMode addressingMode,
            FilterMode filterMode,
            out ErrorCode errcodeRet);

        ErrorCode clRetainSampler(SamplerId sampler);

        ErrorCode clReleaseSampler(SamplerId sampler);

        ErrorCode clGetSamplerInfo(
            SamplerId sampler,
            SamplerInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        ProgramId clCreateProgramWithSource(
            ContextId context,
            uint count,
            [In] string[] strings,
            [In] UIntPtr[] lengths,
            out ErrorCode errcodeRet);

        ProgramId clCreateProgramWithBinary(
            ContextId context,
            uint numDevices,
            [In] DeviceId[] deviceList,
            [In] UIntPtr[] lengths,
            [In] IntPtr[] binaries,
            [Out] ErrorCode[] binaryStatus,
            out ErrorCode errcodeRet);

        ProgramId clCreateProgramWithBuiltInKernels(
            ContextId context,
            uint numDevices,
            [In] DeviceId[] deviceList,
            [In] string kernelNames,
            out ErrorCode errcodeRet);

        ErrorCode clRetainProgram(ProgramId program);

        ErrorCode clReleaseProgram(ProgramId program);

        ErrorCode clBuildProgram(
            ProgramId program,
            uint numDevices,
            [In] DeviceId[] deviceList,
            [In] string options,
            ProgramBuildNotify pfnNotify,
            [In] IntPtr userData);

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

        ErrorCode clUnloadPlatformCompiler(PlatformId platform);

        ErrorCode clGetProgramInfo(
            ProgramId program,
            ProgramInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        ErrorCode clGetProgramBuildInfo(
            ProgramId program,
            DeviceId device,
            ProgramBuildInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        KernelId clCreateKernel(
            ProgramId program,
            [In] string kernelName,
            out ErrorCode errcodeRet);

        ErrorCode clCreateKernelsInProgram(
            ProgramId program,
            uint numKernels,
            [Out] KernelId[] kernels,
            out uint numKernelsRet);

        ErrorCode clRetainKernel(KernelId kernel);

        ErrorCode clReleaseKernel(KernelId kernel);

        ErrorCode clSetKernelArg(
            KernelId kernel,
            uint argIndex,
            UIntPtr argSize,
            [In] IntPtr argValue);

        ErrorCode clGetKernelInfo(
            KernelId kernel,
            KernelInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        ErrorCode clGetKernelArgInfo(
            KernelId kernel,
            uint argIndex,
            KernelArgInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        ErrorCode clGetKernelWorkGroupInfo(
            KernelId kernel,
            DeviceId device,
            KernelWorkGroupInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

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

        EventId clCreateUserEvent(ContextId context, out ErrorCode errcodeRet);

        ErrorCode clSetUserEventStatus(EventId @event, CommandExecutionStatus executionStatus);

        ErrorCode clWaitForEvents(
            uint numEvents,
            [In] EventId[] eventList);

        ErrorCode clGetEventInfo(
            EventId @event,
            EventInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        ErrorCode clSetEventCallback(
            EventId @event,
            CommandExecutionStatus commandExecCallbackType,
            EventNotify pfnEventNotify,
            [In] IntPtr userData);

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

        ErrorCode clGetEventProfilingInfo(
            EventId @event,
            ProfilingInfo paramName,
            UIntPtr paramValueSize,
            [Out] IntPtr paramValue,
            out UIntPtr paramValueSizeRet);

        ErrorCode clFlush(CommandQueueId commandQueue);

        ErrorCode clFinish(CommandQueueId commandQueue);
    }
}
