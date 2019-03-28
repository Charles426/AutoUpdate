using AutoUpdate.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate
{
    public class TestClass
    {
        void Test()
        {
            
            XmlHelper.LoadLocalXml();

            XmlHelper.LocalField.LastUpdate = DateTime.Now.ToString();
            XmlHelper.SaveLocalXml();


            XmlHelper.LoadServerXml();
        }

        void Test1()
        {
            //FileHelper.CopyDirectory(@"C:\Users\XJH001\Desktop\新建文件夹\JHZG", @"D:\Test\JHZG");
            //FileHelper.DeleteDirectoryIfExists(@"D:\Test\JHZG");
        }

        void Test2()
        {
            //UpdateWorker updateWorker = new UpdateWorker();
            //updateWorker.Bak();
        }

        void Test3()
        {
            ZipHelper.ExtraToDirectory(@"C:\Users\XJH001\AppData\Local\Temp\MAutoUpdate\temp\1.1.0.0.zip", @"D:\Test");
            ZipHelper.CreateFromDirectory(@"C:\Users\XJH001\AppData\Local\Temp\MAutoUpdate\bak", @"D:\Test\1.1.0.0.zip");
        }

    }
}
