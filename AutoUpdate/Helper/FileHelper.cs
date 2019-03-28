using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Helper
{
    public class FileHelper
    {

        /// <summary>
        /// 文件拷贝
        /// </summary>
        /// <param name="srcDir">源目录</param>
        /// <param name="desDir">目标目录</param>
        /// <param name="overwrite">是否覆写</param>
        public static void CopyDirectory(string srcDir,string desDir,bool overwrite = true)
        {
            //目标目录不存在，则创建
            if (!Directory.Exists(desDir))
            {
                Directory.CreateDirectory(desDir);
            }

            //返回指定路径中的所有文件和子目录的名称
            string[] fileNames = Directory.GetFileSystemEntries(srcDir);

            foreach (string fileName in fileNames)
            {
                string desFileName = Path.Combine(desDir, new DirectoryInfo(fileName).Name);

                //如果是目录，则递归Copy目录下的文件
                if (Directory.Exists(fileName))
                {
                    //递归
                    CopyDirectory(fileName, desFileName, overwrite);
                }
                //Copy文件
                else
                {
                    File.Copy(fileName, desFileName, overwrite);
                }

            }

        }



        public static void CopyDirectory(string srcDir, string desDir, IList<string> ignoreFileNames, bool overwrite = true)
        {
            //目标目录不存在，则创建
            if (!Directory.Exists(desDir))
            {
                Directory.CreateDirectory(desDir);
            }

            //返回指定路径中的所有文件和子目录的名称
            string[] fileNames = Directory.GetFileSystemEntries(srcDir);

            foreach (string fileName in fileNames)
            {
                string name = new DirectoryInfo(fileName).Name;
                string desFileName = Path.Combine(desDir, name);

                //如果是目录，则递归Copy目录下的文件
                if (Directory.Exists(fileName))
                {
                    //递归
                    CopyDirectory(fileName, desFileName, ignoreFileNames, overwrite);
                }
                //Copy文件
                else
                {
                    if (!ignoreFileNames.Contains(name))
                        File.Copy(fileName, desFileName, overwrite);
                }

            }

        }



        /// <summary>
        /// 删除文件如果存在
        /// </summary>
        /// <param name="fileName"></param>
        public static void DeleteFileIfExists(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileInfo file = new FileInfo(fileName);
                file.Delete();
            }
        }

        /// <summary>
        /// 删除文件夹如果存在
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectoryIfExists(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                //递归删除文件及文件夹
                directoryInfo.Delete(true);
            }
        }

        public static void CreateDirectoryIfNotExists(string path)
        {
            //目标目录不存在，则创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

    }
}
