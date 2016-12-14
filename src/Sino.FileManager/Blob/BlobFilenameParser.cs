using Sino.FileManager.Core;
using System;
using System.IO;

namespace Sino.FileManager
{
    public class BlobFilenameParser : IFilenameParser
    {
        public string Parse(string filename)
        {
            string ext = Path.GetExtension(filename);
            DateTime now = DateTime.Now;
            string path = string.Format("{0}\\{1}\\{2}\\{3}{4}{5}{6}{7}", now.Year, now.Month, now.Day,
                now.Hour, now.Minute, now.Second, now.Millisecond, ext);
            return path;
        }
    }
}
