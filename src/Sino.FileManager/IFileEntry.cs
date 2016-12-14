using System.IO;

namespace Sino.FileManager.Core
{
    /// <summary>
    /// 文件实体对象
    /// </summary>
    public interface IFileEntry
    {
        string FileName { get; set; }
        long StartPosition { get; set; }
        long Length { get; set; }
        Stream Stream { get; set; }
    }
}
