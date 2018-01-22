using System;
using Sino.FileManager;
using Sino.FileManager.Core;
using Sino.FileManager.Oss;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FileManagerServiceCollectionExtensions
    {
        /// <summary>
        /// 增加文件管理
        /// </summary>
        /// <param name="connectionString">Blob的连接字符串</param>
        /// <param name="containerString">默认容器名称</param>
        /// <param name="rootDirectory">根文件夹路径</param>
        public static IServiceCollection AddFileManager(this IServiceCollection services, string connectionString, string containerString, string rootDirectory = "upload")
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(containerString))
                throw new ArgumentNullException(nameof(containerString));

            var storage = new OssFileStorage(connectionString);
            storage.DefaultBucket = containerString;
            storage.RootDirectory = rootDirectory;

            var parser = new OssFilenameParser();

            services.AddSingleton<IFileManager>(new FileManager(storage, parser));

            return services;
        }
    }
}
