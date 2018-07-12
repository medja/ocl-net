using System;

namespace OCL.Net
{
    public class RuntimeException : Exception
    {
        public RuntimeException(string error) : base(error)
        { }
    }
}
