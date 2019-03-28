using AutoUpdate.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate
{
    public class UpdateWorker
    {
        public delegate void UpdateProgress(double data);

        public UpdateProgress UpdateProgressDelegate;

        public event UpdateProgress OnUpdateProgress
        {
            add
            {
                UpdateProgressDelegate += value;
            }
            remove
            {
                UpdateProgressDelegate -= value;
            }
        }

        //临时目录(WIN7以及以上系统在C盘只有对temp目录有操作权限)
        private readonly string tempPath = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), @"AutoUpdate\temp\");
        private readonly string bakPath = Path.Combine(Environment.GetEnvironmentVariable("TEMP"),@"AutoUpdate\bak\");
        private readonly string zipPath = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), @"AutoUpdate\zip\");
        private readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private readonly IList<string> ignoreFileNames = new List<string>();
        public string ErrorMessage;

        public UpdateWorker()
        {
            LoadXml();
            //ignoreFileNames.Add(XmlHelper.ServerField.ApplicationStart);
            ignoreFileNames.Add("AutoUpdate.exe");
            ignoreFileNames.Add("AutoUpdate.pdb");
            FileHelper.CreateDirectoryIfNotExists(tempPath);
            FileHelper.CreateDirectoryIfNotExists(bakPath);
            FileHelper.CreateDirectoryIfNotExists(zipPath);
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        private void LoadXml()
        {
            XmlHelper.LoadXml();
        }


        /// <summary>
        /// 是否需要更新
        /// </summary>
        /// <returns></returns>
        public bool IsNeedUpdate()
        {
            string[] localVersions = XmlHelper.LocalField.LocalVersion.Split('.');
            string[] releaseVersions = XmlHelper.ServerField.ReleaseVersion.Split('.');

            for (int i = 0; i < localVersions.Length; i++)
            {
                if (int.Parse(localVersions[i]) < int.Parse(releaseVersions[i]))
                    return true;
            }

            return false;

        }


        public bool Do()
        {
            try
            {
                Bak().DownLoad().ZipExtraToDirectory().UpdateProgram().DeleteTempDirectory(true).UpdateLoaclXml();
                return true;
            }
            catch(Exception e)
            {
                string stackTrace = e.StackTrace;
                ErrorMessage = e.Message;
                RollBack().DeleteTempDirectory(false);
                return false;
            }
        }


        /// <summary>
        /// 备份当前的程序目录信息
        /// </summary>
        /// <returns></returns>
        private UpdateWorker Bak()
        {
            FileHelper.CopyDirectory(baseDirectory, bakPath, ignoreFileNames);
            UpdateProgressDelegate?.Invoke(15);
            return this;
        }

        private UpdateWorker ZipExtraToDirectory()
        {
            ZipHelper.ExtraToDirectory(Path.Combine(tempPath, $"{XmlHelper.ServerField.ReleaseVersion}.zip"),zipPath);
            UpdateProgressDelegate?.Invoke(50);
            return this;
        }
        
        /// <summary>
        /// 下载指定的URI资源到本地文件
        /// </summary>
        /// <returns></returns>
        private UpdateWorker DownLoad()
        {
            using (WebClient webClient = new WebClient())
            {
                string fileName = Path.Combine(tempPath, $"{XmlHelper.ServerField.ReleaseVersion}.zip");
                webClient.DownloadFile(XmlHelper.ServerField.ReleaseUrl, fileName);
            }
            UpdateProgressDelegate?.Invoke(35);
            return this;
        }


        /// <summary>
        /// 更新程序
        /// </summary>
        /// <returns></returns>
        private UpdateWorker UpdateProgram()
        {
            FileHelper.CopyDirectory(zipPath, baseDirectory, ignoreFileNames);
            UpdateProgressDelegate?.Invoke(70);
            return this;
        }

        /// <summary>
        /// 更新本地xml文件
        /// </summary>
        /// <returns></returns>
        private UpdateWorker UpdateLoaclXml()
        {
            XmlHelper.LocalField.LastUpdate = XmlHelper.ServerField.ReleaseDate;
            XmlHelper.LocalField.LocalVersion = XmlHelper.ServerField.ReleaseVersion;
            XmlHelper.SaveLocalXml();
            UpdateProgressDelegate?.Invoke(100);
            return this;
        }



        private UpdateWorker DeleteTempDirectory(bool updateProgress = true)
        {
            FileHelper.DeleteDirectoryIfExists(Path.Combine(Environment.GetEnvironmentVariable("TEMP"), @"AutoUpdate"));
            if(updateProgress)
                UpdateProgressDelegate?.Invoke(85);

            return this;
        }


        /// <summary>
        /// 更新失败的情况下，回滚当前更新
        /// </summary>
        /// <returns></returns>
        private UpdateWorker RollBack()
        {
            //FileHelper.DeleteDirectoryIfExists(baseDirectory);
            FileHelper.CopyDirectory(bakPath, baseDirectory, ignoreFileNames);
            return this;
        }




    }
}
