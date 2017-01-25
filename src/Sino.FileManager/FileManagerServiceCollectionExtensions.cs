using System;
using Sino.FileManager;
using Sino.FileManager.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FileManagerServiceCollectionExtensions
    {
        /// <summary>
        /// 增加文件管理
        /// </summary>
        /// <param name="connectionString">Blob的连接字符串</param>
        /// <param name="containerString">默认容器名称</param>
        public static IServiceCollection AddFileManager(this IServiceCollection services, string connectionString, string containerString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(containerString))
                throw new ArgumentNullException(nameof(containerString));

            var storage = new BlobFileStorage(connectionString);
            storage.DefaultContainer = containerString;

            var parser = new BlobFilenameParser();

            services.AddSingleton<IFileManager>(new FileManager(storage, parser));

            return services;
        }
    }
}
