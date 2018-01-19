using Aliyun.OSS;
using Sino.FileManager.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace Sino.FileManager.Oss
{
    public class OssFileStorage : IFileStorage
    {
        public const string DefaultConnectionString = "StorageConnectionString";
        public const string DefaultBucketString = "DefaultBucket";

        public OssClient Client { get; private set; }

        public string DefaultBucket { get; set; }

        protected string EndPoint { get; set; }

        protected string AccessKeyId { get; set; }

        protected string AccessKeySecret { get; set; }

        public OssFileStorage(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            var argu = connectionString.Split(';');
            if (argu == null || argu.Length < 3)
            {
                throw new ArgumentOutOfRangeException(nameof(connectionString));
            }
            DefaultBucket = DefaultBucketString;
            EndPoint = argu[0];
            AccessKeyId = argu[1];
            AccessKeySecret = argu[2];
        }

        protected OssObject GetBlockBlob(string filenames)
        {
            var obj = Client.GetObject(DefaultBucket, filenames);
            return obj;
        }

        #region IFileStorage Impl

        public Task Init()
        {
            Client = new OssClient(EndPoint, AccessKeyId, AccessKeySecret);
            if (!Client.DoesBucketExist(DefaultBucket))
            {
                Client.CreateBucket(DefaultBucket);
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<IFileEntry>> GetEntriesAsync(IEnumerable<IFileEntry> filenames)
        {
            var list = new List<IFileEntry>();
            if (filenames != null && filenames.Count() > 0)
            {
                foreach (var item in filenames)
                {
                    list.Add(await GetEntryAsync(item.FileName, item.StartPosition, item.Length));
                }
            }
            return list;
        }

        public async Task<IFileEntry> GetEntryAsync(string filename)
        {
            return await GetEntryAsync(filename, 0, -1);
        }

        public Task<bool> ExistsAsync(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }
            return Task.FromResult(Client.DoesObjectExist(DefaultBucket, filename));
        }

        public async Task<IFileEntry> GetEntryAsync(string filename, long pos, long length)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("filename");
            }
            if (pos < 0)
            {
                throw new ArgumentOutOfRangeException("pos");
            }
            if (length < -1)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            DefaultFileEntry entry = new DefaultFileEntry
            {
                FileName = filename,
                StartPosition = pos,
                Length = length,
                Stream = new MemoryStream()
            };


            if (Client.DoesObjectExist(DefaultBucket, filename))
            {
                var obj = GetBlockBlob(filename);
                await obj.Content.CopyToAsync(entry.Stream);
            }
            else
            {
                entry.Stream = null;
            }
            return entry;
        }

        public Task<string> SaveEntryAsync(Stream stream, string filename)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("filename");
            }
            Client.PutObject(DefaultBucket, filename, stream);
            return Task.FromResult(filename);
        }

        #endregion
    }
}
