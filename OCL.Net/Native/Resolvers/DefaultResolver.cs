using System.Collections.Generic;
using System.Runtime.InteropServices;
using AdvancedDLSupport;

namespace OCL.Net.Native.Resolvers
{
    public sealed class DefaultResolver : IResolver
    {
        private readonly IResolver _resolver;

        public DefaultResolver()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _resolver = new WindowsResolver();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                _resolver = new MacOsResolver();
            }
            else
            {
                _resolver = new LinuxResolver();
            }
        }

        public IReadOnlyList<string> SearchPaths
        {
            get => _resolver.SearchPaths;
            set => _resolver.SearchPaths = value;
        }

        public IReadOnlyList<string> LibraryNames
        {
            get => _resolver.SearchPaths;
            set => _resolver.SearchPaths = value;
        }

        public IEnumerable<string> FindPaths()
        {
            return _resolver.FindPaths();
        }

        public ResolvePathResult Resolve(string path)
        {
            return _resolver.Resolve(path);
        }
    }
}
