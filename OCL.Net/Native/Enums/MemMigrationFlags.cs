using System;

namespace OCL.Net.Native.Enums
{
    [Flags]
    public enum MemMigrationFlags : ulong
    {
        MigrateMemObjectHost             = 1 << 0,
        MigrateMemObjectContentUndefined = 1 << 1
    }
}
