using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Helper
{
    public class ProcessHelper
    {
        /// <summary>
        /// 进程是否存在
        /// </summary>
        /// <param name="ProgramName"></param>
        /// <returns></returns>
        public static bool ProcessExists(string programName)
        {
            return Process.GetProcessesByName(programName).Length > 0 ? true : false;
        }

        /// <summary>
        /// 杀掉当前运行的进程
        /// </summary>
        public static void KillProgressIfExists(string programName)
        {
            Process[] processes = Process.GetProcessesByName(programName);
            foreach (Process process in processes)
            {
                process.Kill();
                process.Close();
            }
        }

        public static void StartProgress(string fileName)
        {
            Process.Start(fileName);
        }

    }
}
