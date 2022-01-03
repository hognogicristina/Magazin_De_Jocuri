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
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            modif_joc i1 = new modif_joc();
            panel1.Controls.Add(i1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            joc i1 = new joc();
            panel1.Controls.Add(i1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comenzi c = new comenzi();
            c.ShowDialog();
        }

        private void admin_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            panel1.Top = 150;
            panel1.Height = this.Height - 150;
            panel1.Width = this.Width;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
