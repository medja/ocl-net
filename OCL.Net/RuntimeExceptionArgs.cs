using System;

namespace OCL.Net
{
    public class RuntimeExceptionArgs : EventArgs
    {
        public RuntimeException Exception { get; }

        public RuntimeExceptionArgs(RuntimeException exception)
        {
            Exception = exception;
        }
    }
}
