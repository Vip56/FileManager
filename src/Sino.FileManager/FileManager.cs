using Sino.FileManager.Core;
using System;
using System.Threading.Tasks;
using System.IO;

namespace Sino.FileManager
{
    public class FileManager : IFileManager
    {
        public IFilenameParser FilenameParser { get; set; }

        public IFileStorage FileStorage { get; set; }

        public FileManager(IFileStorage filestorage, IFilenameParser filenameparser)
        {
            if (filestorage == null)
            {
                throw new ArgumentNullException("filestorage");
            }
            if (filenameparser == null)
            {
                throw new ArgumentNullException("filenameparser");
            }

            FilenameParser = filenameparser;
            FileStorage = filestorage;

            FileStorage.Init();
        }

        public IFileEntry Get(string arg)
        {
            return Get(arg, 0, -1);
        }

        public IFileEntry Get(string arg, long pos, long length)
        {
            if (FileStorage == null)
            {
                throw new NullReferenceException();
            }
            return FileStorage.GetEntry(arg, pos, length);
        }

        public Task<IFileEntry> GetAsync(string arg)
        {
            return Task<IFileEntry>.Factory.StartNew(() =>
            {
                return Get(arg);
            });
        }

        public Task<IFileEntry> GetAsync(string arg, long pos, long length)
        {
            return Task<IFileEntry>.Factory.StartNew(() =>
            {
                return Get(arg, pos, length);
            });
        }

        public string Save(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            if (File.Exists(path))
            {
                string filename = Path.GetFileName(path);
                using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return Save(fs, filename);
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public string Save(Stream stream, string filename)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("filename");
            }

            if (FileStorage == null)
            {
                throw new NullReferenceException();
            }
            if (FilenameParser == null)
            {
                throw new NullReferenceException();
            }

            stream.Position = 0;
            string savepath = FilenameParser.Parse(filename);
            return FileStorage.SaveEntry(stream, savepath);
        }

        public Task<string> SaveAsync(string path)
        {
            return Task<string>.Factory.StartNew(() =>
            {
                return Save(path);
            });
        }

        public Task<string> SaveAsync(Stream stream, string filename)
        {
            return Task<string>.Factory.StartNew(() =>
            {
                return Save(stream, filename);
            });
        }
    }
}
