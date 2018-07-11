using OCL.Net.Native.Enums;

namespace OCL.Net.Native
{
    public static class Extensions
    {
        public static void HandleError(this ErrorCode errorCode)
        {
            if (errorCode < ErrorCode.Success)
                throw new OpenClException(errorCode);
        }
    }
}
