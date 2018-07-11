using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OCL.Net.Native.Resolvers
{
    public class WindowsResolver : ResolverBase
    {
        private static readonly string[] FileNames =
        {
            "OpenCL.dll"
        };

        public override IReadOnlyList<string> SearchPaths { get; set; } = GetSearchPaths().ToList();
        public override IReadOnlyList<string> LibraryNames { get; set; } = FileNames;

        private static IEnumerable<string> GetSearchPaths()
        {
            var appPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            if (appPath != null)
                yield return appPath;

            yield return Environment.SystemDirectory;
        }
    }
}
