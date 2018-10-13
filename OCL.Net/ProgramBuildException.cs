using System.Collections.Generic;
using System.Linq;
using System.Text;
using OCL.Net.Internal;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    public class ProgramBuildException : OpenClException
    {
        public Program Program { get; }
        public IReadOnlyDictionary<Device, string> ErrorLogs { get; }

        private ProgramBuildException(
            ErrorCode errorCode, Program program, IReadOnlyDictionary<Device, string> errorLogs, string message)
            : base(errorCode, message)
        {
            Program = program;
            ErrorLogs = errorLogs;
        }

        public static ProgramBuildException Create(ErrorCode errorCode, Program program, IEnumerable<Device> devices)
        {
            var errorLogs = devices.ToDictionary(device => device, device => Utils.GetProgramBuildLog(program, device));

            return Create(errorCode, program, errorLogs);
        }

        public static ProgramBuildException Create(ErrorCode errorCode,
            Program program, IReadOnlyDictionary<Device, string> errorLogs)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Program build failed:");

            foreach (var errorLog in errorLogs)
            {
                builder.AppendLine();
                builder.Append(errorLog.Key.Name);
                builder.AppendLine(":");
                builder.AppendLine(errorLog.Value);
            }

            return new ProgramBuildException(errorCode, program, errorLogs, builder.ToString());
        }
    }
}
