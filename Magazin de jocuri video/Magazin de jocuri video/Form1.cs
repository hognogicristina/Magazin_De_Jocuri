using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Magazin_de_jocuri_video
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        OleDbConnection conn;

        private void Form1_Shown(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=jocuri.accdb;"; 
            conn = new OleDbConnection(cs);
            conn.Open();
            this.WindowState = FormWindowState.Maximized;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string u = textBox1.Text;
            string p = textBox2.Text;
            string q = "SELECT * FROM Utilizatori WHERE Utilizatori='" + u + "' AND Parola='" + p + "'";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                string np = dr[2].ToString();
                string tip = dr[4].ToString();
                if (tip == "admin")
                {
                    admin f = new admin();
                    f.Text = "Bine ai venit " + np;
                    f.Show();
                    this.Hide();
                }
                else
                {
                    user f = new user();
                    f.Text = "Bine ai venit " + np;
                    f.idu = dr[1].ToString();
                    f.Show();
                    this.Hide();
                }
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else MessageBox.Show("Date de logare gresite!");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.UseSystemPasswordChar = true;
            else textBox2.UseSystemPasswordChar = false;
        }
    }
}
