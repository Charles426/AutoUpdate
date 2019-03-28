using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAutoUpdate.Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdate.exe";
            //同时启动自动更新程序
            if (File.Exists(path))
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo()
                {
                    FileName = "AutoUpdate.exe",
                    Arguments = "应用程序名称"
                };
                Process proc = Process.Start(processStartInfo);
                if (proc != null)
                {
                    proc.WaitForExit();
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
