using Sino.FileManager;
using Sino.FileManager.Oss;
using System;
using System.IO;
using System.Text;

namespace FileManagerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var storage = new OssFileStorage("");
            storage.DefaultBucket = "";

            var parser = new OssFilenameParser();

            var fm = new FileManager(storage, parser);

            using (MemoryStream ms = new MemoryStream())
            {
                var txt = Encoding.UTF8.GetBytes("测试数据");
                ms.Write(txt, 0, txt.Length);

                var filename = fm.SaveAsync(ms, "test.txt").Result;
                var isexists = fm.ExistsAsync(filename).Result;
                var obj = fm.GetAsync(filename).Result;
            }
            Console.WriteLine("Hello World!");
        }
    }
}
