using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AdvancedDLSupport;
using OCL.Net.Native.Resolvers;

namespace OCL.Net.Native
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ValueParameterNotUsed")]
    public static class OpenCl
    {
        private static IResolver _resolver = new DefaultResolver();
        private static IReadOnlyList<IOpenCl> _libraries;
        private static readonly object SyncLock = new object();

        public static bool LoadMultipleLibraries { get; set; } = false;

        public static IResolver Resolver
        {
            get
            {
                lock (SyncLock)
                    return _resolver;
            }
            set
            {
                lock (SyncLock)
                    _resolver = Resolver;
            }
        }

        public static IReadOnlyList<IOpenCl> Libraries
        {
            get
            {
                if (_libraries == null)
                {
                    lock (SyncLock)
                    {
                        if (_libraries == null)
                            _libraries = Load(Resolver.FindPaths(), Resolver).ToList();
                    }
                }

                return _libraries;
            }
            set
            {
                lock (SyncLock)
                    _libraries = value;
            }
        }

        public static IEnumerable<IOpenCl> Load(IEnumerable<string> paths, ILibraryPathResolver resolver)
        {
            var builder = new NativeLibraryBuilder(pathResolver: resolver);
            var libraries = paths.Select(path => builder.ActivateInterface<IOpenCl>(path));

            return LoadMultipleLibraries ? libraries : libraries.Take(1);
        }
    }
}
