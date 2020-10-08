using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleRTSPPlayerWinforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.videoControl1.StartPlay(this.txtUrl.Text);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.videoControl1.Stop();
        }
    }
}
