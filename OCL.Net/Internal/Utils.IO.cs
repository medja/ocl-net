using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OCL.Net.Internal
{
    public static partial class Utils
    {
        public static async Task<byte[]> ReadFileAsync(string filePath, CancellationToken cancellationToken)
        {
            const int blockSize = 4096;

            var fileInfo = new FileInfo(filePath);
            var buffer = new byte[fileInfo.Length];
            var offset = 0;

            using (var reader = fileInfo.OpenRead())
            {
                while (true)
                {
                    var count = Math.Min(blockSize, buffer.Length - offset);
                    var read = await reader.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);

                    offset += read;

                    if (read != 0)
                        continue;

                    if (offset != buffer.Length)
                        throw new IOException($"Length of file {filePath} changed while it was being read");

                    return buffer;
                }
            }
        }
    }
}
