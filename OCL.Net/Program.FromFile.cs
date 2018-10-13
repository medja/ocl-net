using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using OCL.Net.Internal;

namespace OCL.Net
{
    public sealed partial class Program
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Program> FromFile(Context context, params string[] filePaths)
        {
            return FromFile(context, (IEnumerable<string>) filePaths);
        }

        public static async Task<Program> FromFile(Context context,
            IEnumerable<string> filePaths, CancellationToken cancellationToken = default)
        {
            if (filePaths == null)
                throw new ArgumentNullException(nameof(filePaths));

            var tasks = filePaths.Select(filePath => Utils.ReadFileAsync(filePath, cancellationToken));
            var sources = await Task.WhenAll(tasks);

            return FromSource(context, sources);
        }
    }
}
