using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoUpdate
{
    public partial class UpdateForm : Form
    {
        public UpdateWorker UpdateWorker;


        public UpdateForm()
        {
            InitializeComponent();
        }


        public UpdateForm(UpdateWorker updateWorker)
        {
            this.UpdateWorker = updateWorker;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            updateWorker.OnUpdateProgress += (obj) =>
            {
                this.Invoke(new Action(() =>
                {
                    progressBar1.Value = (int)obj;
                }));
                
            };

        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            new Task(() =>
            {
                try
                {
                    if (!UpdateWorker.Do())
                    {
                        this.Invoke(new Action(() =>
                        {
                            label1.Text = $"升级失败:{UpdateWorker.ErrorMessage}";
                            
                        }));
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        this.DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            label1.Text = "升级完成";
                            
                        }));
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        this.DialogResult = DialogResult.OK;
                    }

                    
                }
                catch (Exception e2)
                {
                    this.Invoke(new Action(() =>
                    {
                        label1.Text = $"升级失败:{e2.Message}";
                    }));
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    this.DialogResult = DialogResult.Cancel;
                    //MessageBox.Show(e2.Message);
                }

            }).Start();
        }
    }
}
