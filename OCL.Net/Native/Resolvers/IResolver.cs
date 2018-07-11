using System.Collections.Generic;
using AdvancedDLSupport;

namespace OCL.Net.Native.Resolvers
{
    public interface IResolver : ILibraryPathResolver
    {
        IReadOnlyList<string> SearchPaths { get; set; }
        IReadOnlyList<string> LibraryNames { get; set; }

        IEnumerable<string> FindPaths();
    }
}
