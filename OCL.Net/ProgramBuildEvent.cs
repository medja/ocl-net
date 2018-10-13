using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public sealed partial class ProgramBuildEvent
    {
        public ProgramBuildType BuildType { get; }
        public BuildStatus BuildStatus { get; set; } = BuildStatus.BuildNone;
        public IReadOnlyDictionary<Device, DeviceBuildInfo> Devices { get; }

        public Task Task
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _taskCompletionSource.Task;
        }

        private readonly Program _program;
        private readonly IntPtr _handle;

        private readonly TaskCompletionSource<Program> _taskCompletionSource;

        private ProgramBuildEvent(ProgramBuildType buildType,
            Program program, IEnumerable<Device> devices, IntPtr handle)
        {
            _program = program;
            _handle = handle;

            BuildType = buildType;
            Devices = devices.ToDictionary(device => device, device => new DeviceBuildInfo(program, device));

            _taskCompletionSource = new TaskCompletionSource<Program>();
        }

        public TaskAwaiter<Program> GetAwaiter()
        {
            return _taskCompletionSource.Task.GetAwaiter();
        }

        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
        private void Update()
        {
            var buildStatus = BuildStatus.BuildSuccess;
            var failedDevices = new List<Device>();

            foreach (var buildInfo in Devices.Values)
            {
                switch (buildInfo.BuildStatus)
                {
                    case BuildStatus.BuildError:
                        buildStatus = BuildStatus.BuildError;
                        failedDevices.Add(buildInfo.Device);
                        break;

                    case BuildStatus.BuildNone:
                    case BuildStatus.BuildInProgress:
                        buildStatus = BuildStatus.BuildInProgress;
                        break;
                }
            }

            lock (_taskCompletionSource)
            {
                if (BuildStatus == BuildStatus.BuildSuccess || BuildStatus == BuildStatus.BuildError)
                    return;

                BuildStatus = buildStatus;

                if (buildStatus != BuildStatus.BuildSuccess && buildStatus != BuildStatus.BuildError)
                    return;
            }

            DisposeHandle(_handle);

            if (buildStatus == BuildStatus.BuildSuccess)
            {
                _taskCompletionSource.SetResult(_program);
            }
            else
            {
                var exception = ProgramBuildException.Create(GetErrorCode(), _program, failedDevices);
                _taskCompletionSource.SetException(exception);
            }
        }

        private ErrorCode GetErrorCode()
        {
            switch (BuildType)
            {
                case ProgramBuildType.Build:
                    return ErrorCode.BuildProgramFailure;

                case ProgramBuildType.Compile:
                    return ErrorCode.CompileProgramFailure;

                case ProgramBuildType.Link:
                    return ErrorCode.LinkProgramFailure;

                default:
                    return ErrorCode.Success;
            }
        }

        internal static ProgramBuildEvent Create(ProgramBuildType buildType,
            Program program, IEnumerable<Device> devices, IntPtr eventHandle)
        {
            var @event = new ProgramBuildEvent(buildType, program, devices, eventHandle);

            RegisterHandle(eventHandle, @event);
            @event.Update();

            return @event;
        }

        public readonly struct DeviceBuildInfo
        {
            public Program Program { get; }
            public Device Device { get; }

            public BuildStatus BuildStatus => LoadValue<BuildStatus>(ProgramBuildInfo.ProgramBuildStatus);
            public ProgramBinaryType BinaryType => LoadValue<ProgramBinaryType>(ProgramBuildInfo.ProgramBinaryType);
            public string Options => Utils.GetProgramBuildOptions(Program, Device);
            public string BuildLog => Utils.GetProgramBuildLog(Program, Device);

            public DeviceBuildInfo(Program program, Device device)
            {
                Program = program;
                Device = device;
            }

            private unsafe T LoadValue<T>(ProgramBuildInfo info) where T : unmanaged
            {
                T value;

                Program.Library.clGetProgramBuildInfoUnsafe(
                    Program, Device, info, (UIntPtr) sizeof(T), &value, null).HandleError();

                return value;
            }
        }
    }
}
