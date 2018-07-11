using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdvancedDLSupport;

namespace OCL.Net.Native.Resolvers
{
    public abstract class ResolverBase : IResolver
    {
        public abstract IReadOnlyList<string> SearchPaths { get; set; }
        public abstract IReadOnlyList<string> LibraryNames { get; set; }

        public IEnumerable<string> FindPaths()
        {
            return LibraryNames.SelectMany(ResolveAll);
        }

        public ResolvePathResult Resolve(string libraryName)
        {
            var path = ResolveAll(libraryName).FirstOrDefault();

            if (path != null)
                return ResolvePathResult.FromSuccess(path);

            return ResolvePathResult.FromError(
                new FileNotFoundException("OpenCL was not found in any of the search paths"));
        }

        private IEnumerable<string> ResolveAll(string libraryName)
        {
            if (!Path.IsPathRooted(libraryName))
            {
                return SearchPaths
                    .Select(searchPath => ResolvePath(searchPath, libraryName))
                    .Where(p => p != null)
                    .ToList();
            }

            var path = ResolvePath(Path.GetDirectoryName(libraryName), Path.GetFileName(libraryName));

            return path != null ? new[] {path} : Enumerable.Empty<string>();
        }

        private static string ResolvePath(string directory, string libraryName)
        {
            if (directory == null || !Directory.Exists(directory))
                return null;

            try
            {
                return Directory
                    .EnumerateFiles(directory, $"{libraryName}*", SearchOption.AllDirectories)
                    .FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}
