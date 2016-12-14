using System.Collections.Generic;
using System.IO;

namespace Sino.FileManager.Core
{
    /// <summary>
    /// 提供文件存储
    /// </summary>
    public interface IFileStorage
    {
        void Init();

        IFileEntry GetEntry(string filename);

        IFileEntry GetEntry(string filename, long pos, long length);

        IEnumerable<IFileEntry> GetEntries(IEnumerable<IFileEntry> filenames);

        string SaveEntry(Stream stream, string filename);
    }
}
