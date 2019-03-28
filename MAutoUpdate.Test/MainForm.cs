using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAutoUpdate.Test
{
    public partial class MainForm : Form
    {
        //
        public delegate void UpdateUI(int step);


        public MainForm()
        {
            InitializeComponent();
        }
    }
}
