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

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="filename">文件名称/路径</param>
        /// <param name="generateFilename">是否自动生成文件名，默认生成</param>
        Task<string> SaveAsync(Stream stream, string filename, bool generateFilename = true);

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">文件本地路径</param>
        Task<string> SaveAsync(string path);

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="arg">文件路径</param>
        Task<IFileEntry> GetAsync(string arg);

        /// <summary>
        /// 分部获取文件
        /// </summary>
        /// <param name="arg">文件路径</param>
        /// <param name="pos">获取文件的起始字节</param>
        /// <param name="length">获取文件的字节数</param>
        Task<IFileEntry> GetAsync(string arg, long pos, long length);

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <returns>False表示不存在,True表示存在</returns>
        Task<bool> ExistsAsync(string filename);
    }
}
