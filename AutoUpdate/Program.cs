using AutoUpdate.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoUpdate
{
    static class Program
    {

        //[STAThread]
        //static void Main()
        //{
        //    //UpdateWorker updateWorker = new UpdateWorker(@"D:\Test");
        //    UpdateWorker updateWorker = new UpdateWorker();
        //    if (updateWorker.IsNeedUpdate())
        //    {
        //        ProcessHelper.KillProgressIfExists("应用程序名称");
        //        updateWorker.Do();
        //    }
        //    else
        //    {
        //        return;
        //    }

        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Form1());
        //}



        [STAThread]
        static void Main(string[] args)
        {
            string processName = args[0];
            //string processName = "应用程序名称";
            UpdateWorker updateWorker = new UpdateWorker();
            if (updateWorker.IsNeedUpdate())
            {
                ProcessHelper.KillProgressIfExists(processName);
                //updateWorker.Do();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                UpdateForm updateForm = new UpdateForm(updateWorker);
                updateForm.ShowDialog();
                if(updateForm.DialogResult == DialogResult.OK)
                    ProcessHelper.StartProgress(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, processName));
                //Application.Run(new UpdateForm(updateWorker));
            }

        }
    }
}
