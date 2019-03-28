using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Helper
{
    public class ZipHelper
    {

        /// <summary>
        /// 将指定zip存档中的所有文件都压缩到文件系统的一个目录下
        /// </summary>
        /// <param name="sourceArchiveFileName"></param>
        /// <param name="destinationDirectoryName"></param>
        public static void ExtraToDirectory(string sourceArchiveFileName,string destinationDirectoryName)
        {
            //若文件已存在，则会报文件已存在错误
            FileHelper.DeleteDirectoryIfExists(destinationDirectoryName);
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
        }


        /// <summary>
        /// 创建zip存档，该存档包含指定目录的文件和目录
        /// </summary>
        /// <param name="sourceDirectoryName"></param>
        /// <param name="destinationArchiveFileName"></param>
        public static void CreateFromDirectory(string sourceDirectoryName,string destinationArchiveFileName)
        {
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName);
        }
    }
}
