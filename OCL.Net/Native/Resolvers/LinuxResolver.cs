using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OCL.Net.Native.Resolvers
{
    public class LinuxResolver : ResolverBase
    {
        private static readonly string[] FileNames =
        {
            "libOpenCL.so"
        };

        private static readonly string[] LibraryPaths =
        {
            "/lib",
            "/usr/lib"
        };

        private static readonly string[] EnvironmentVariables =
        {
            "LD_LIBRARY_PATH"
        };

        public override IReadOnlyList<string> SearchPaths { get; set; } = GetSearchPaths().ToList();
        public override IReadOnlyList<string> LibraryNames { get; set; } = FileNames;

        private static IEnumerable<string> GetSearchPaths()
        {
            foreach (var environmentVariable in EnvironmentVariables)
            {
                var paths = Environment.GetEnvironmentVariable(environmentVariable)?.Split(':') ??
                            Enumerable.Empty<string>();

                foreach (var path in paths)
                {
                    if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
                        yield return path;
                }
            }

            foreach (var libraryPath in LibraryPaths)
            {
                if (Directory.Exists(libraryPath))
                    yield return libraryPath;
            }
        }
    }
}
