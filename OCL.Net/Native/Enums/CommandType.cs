namespace OCL.Net.Native.Enums
{
    public enum CommandType : uint
    {
        CommandNdRangeKernel     = 0x11F0,
        CommandTask              = 0x11F1,
        CommandNativeKernel      = 0x11F2,
        CommandReadBuffer        = 0x11F3,
        CommandWriteBuffer       = 0x11F4,
        CommandCopyBuffer        = 0x11F5,
        CommandReadImage         = 0x11F6,
        CommandWriteImage        = 0x11F7,
        CommandCopyImage         = 0x11F8,
        CommandCopyImageToBuffer = 0x11F9,
        CommandCopyBufferToImage = 0x11FA,
        CommandMapBuffer         = 0x11FB,
        CommandMapImage          = 0x11FC,
        CommandUnmapMemObject    = 0x11FD,
        CommandMarker            = 0x11FE,
        CommandAcquireGlObjects  = 0x11FF,
        CommandReleaseGlObjects  = 0x1200,
        CommandReadBufferRect    = 0x1201,
        CommandWriteBufferRect   = 0x1202,
        CommandCopyBufferRect    = 0x1203,
        CommandUser              = 0x1204,
        CommandBarrier           = 0x1205,
        CommandMigrateMemObjects = 0x1206,
        CommandFillBuffer        = 0x1207,
        CommandFillImage         = 0x1208
    }
}
