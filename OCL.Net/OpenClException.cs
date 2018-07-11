using System;
using System.Diagnostics.CodeAnalysis;
using OCL.Net.Native.Enums;

namespace OCL.Net
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class OpenClException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public OpenClException(ErrorCode errorCode) : this(errorCode, errorCode.ToString())
        { }

        public OpenClException(ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
