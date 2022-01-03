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
using System.IO;

namespace Magazin_de_jocuri_video
{
    public partial class joc : UserControl
    {
        public joc()
        {
            InitializeComponent();
        }

        //adauga joc

        List<string> fimagini;
        List<PictureBox> pimagini;

        OleDbConnection conn;

        private void incarca_developer()
        {
            string q = "SELECT * FROM Developers ORDER BY Developer";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }

        private void incarca_genul()
        {
            string q = "SELECT * FROM Gen ORDER BY Genul";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            comboBox2.Items.Clear();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }

        private void incarca_anul()
        {
            string q = "SELECT * FROM An ORDER BY An_aparitie";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            comboBox3.Items.Clear();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }

        private void incarca_produse(string dev, string gen, string an)
        {
            string q = "SELECT * FROM jocuri ";
            if (dev != "" && gen != "" && an != "") q = q + "WHERE Developers='" + dev + "' AND Gen ='" + gen + "' AND An_aparitie ='" + an + "'";
            else if (dev != "") q = q + "WHERE Developers='" + dev + "'";
            else if (gen != "") q = q + "WHERE Gen = '" + gen + "'";
            else if (an != "") q = q + "WHERE An_aparitie = '" + an + "'";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            listBox1.Items.Clear();
            while (dr.Read())
                listBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            dr.Close();
        }

        private void joc_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=jocuri.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_developer();
            incarca_genul();
            incarca_anul();
            incarca_produse("", "", "");
            fimagini = new List<string>();
            pimagini = new List<PictureBox>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string idp = "";
            string developer = comboBox1.Text;
            string gen = comboBox2.Text;
            string an_aparitie = comboBox3.Text;
            string sp = "false";
            if (checkBox1.Checked) sp = "true";
            string mp = "false";
            if (checkBox2.Checked) mp = "true";
            string platform = "";
            if (checkBox3.Checked) platform = platform + "PC" + " ";
            if (checkBox4.Checked) platform = platform + "PlayStation" + " ";
            if (checkBox5.Checked) platform = platform + "Xbox" + " ";
            string denumire = textBox1.Text;
            string gameplay = textBox2.Text;
            string pret = textBox3.Text;
            int stoc = (int)numericUpDown1.Value;
            if (denumire != "" && gameplay != "" && developer != "" && gen != "" && an_aparitie != "" && pret != "")
            {
                string q = "insert into jocuri(Denumire, Developers, Gen, An_aparitie, Exemplare, Platforma, Singleplayer, Multiplayer, Gameplay, Pret)";
                q = q + "values ('" + denumire + "', '" + developer + "', '" + gen + "', " + an_aparitie + ", " + stoc + ", '" + platform + "', " + sp + "," + mp + ", '" + gameplay + "', " + pret + ")";
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                q = "SELECT * FROM Jocuri";
                c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                while (dr.Read())
                    idp = dr[0].ToString();
                foreach (string s in fimagini)
                {
                    q = "insert into Fotografii(ID_produs, Fotografie) VALUES (" + idp + ",'" + s + "')";
                    c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                }
                incarca_produse("", "", "");
            }
            else MessageBox.Show("Completeaza toate campurile!");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            numericUpDown1.Value = 0;
            flowLayoutPanel1.Controls.Clear();
        }

        string nume_fisier(string s)
        {
            int i = s.Length - 1;
            while (s[i] != '\\') i--;
            return s.Substring(i + 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fisier = openFileDialog1.FileName;
                string imagine = nume_fisier(openFileDialog1.FileName);
                string fisier2 = Application.StartupPath + "/imagini/" + nume_fisier(openFileDialog1.FileName);
                File.Copy(fisier, fisier2, true);
                PictureBox p = new PictureBox();
                p.Load(fisier);
                p.Width = flowLayoutPanel1.Width - 3;
                p.Height = 2 * p.Width;
                //p.SizeMode = PictureBoxSizeMode.StretchImage;
                flowLayoutPanel1.Controls.Add(p);
                pimagini.Add(p);
                fimagini.Add(imagine);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
        }
    }
}
