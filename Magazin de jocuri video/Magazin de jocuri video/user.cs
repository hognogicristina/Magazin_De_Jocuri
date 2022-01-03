using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazin_de_jocuri_video
{
    public partial class user : Form
    {
        public user()
        {
            InitializeComponent();
        }

        public string idu = "";

        private void button1_Click(object sender, EventArgs e)
        {
            viz_joc i1 = new viz_joc();
            panel1.Controls.Add(i1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cump_joc c = new cump_joc();
            c.idu = idu;
            c.ShowDialog();
        }

        private void user_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            panel1.Top = 100;
            panel1.Height = this.Height - 100;
            panel1.Width = this.Width;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
            //this.Close();
        }
    }
}
