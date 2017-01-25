using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sino.FileManager.Core
{
    /// <summary>
    /// 提供文件存储
    /// </summary>
    public interface IFileStorage
    {
        Task Init();

        Task<IFileEntry> GetEntryAsync(string filename);

        Task<IFileEntry> GetEntryAsync(string filename, long pos, long length);

        Task<IEnumerable<IFileEntry>> GetEntriesAsync(IEnumerable<IFileEntry> filenames);

        Task<string> SaveEntryAsync(Stream stream, string filename);
    }
}
