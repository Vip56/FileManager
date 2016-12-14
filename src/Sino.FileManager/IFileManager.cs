using System.IO;
using System.Threading.Tasks;

namespace Sino.FileManager.Core
{
    /// <summary>
    /// 提供统一管理
    /// </summary>
    public interface IFileManager
    {
        IFileStorage FileStorage { get; set; }

        IFilenameParser FilenameParser { get; set; }

        string Save(Stream stream, string filename);

        string Save(string path);

        Task<string> SaveAsync(Stream stream, string filename);

        Task<string> SaveAsync(string path);

        IFileEntry Get(string arg);

        IFileEntry Get(string arg, long pos, long length);

        Task<IFileEntry> GetAsync(string arg);

        Task<IFileEntry> GetAsync(string arg, long pos, long length);
    }
}
