using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OCL.Net.Native.Resolvers
{
    public class MacOsResolver : ResolverBase
    {
        private const string FrameworkPath = "/System/Library/Frameworks/OpenCL.framework";

        private static readonly string[] FileNames =
        {
            "OpenCL",
            "OpenCL.dylib",
            "OpenCL.so"
        };

        private static readonly string[] EnvironmentVariables =
        {
            "DYLD_FRAMEWORK_PATH",
            "DYLD_LIBRARY_PATH",
            "DYLD_FALLBACK_FRAMEWORK_PATH",
            "DYLD_FALLBACK_LIBRARY_PATH"
        };

        public override IReadOnlyList<string> SearchPaths { get; set; } = GetSearchPaths().ToList();
        public override IReadOnlyList<string> LibraryNames { get; set; } = FileNames;

        private static IEnumerable<string> GetSearchPaths()
        {
            if (Directory.Exists(FrameworkPath))
                yield return FrameworkPath;

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
        }
    }
}
