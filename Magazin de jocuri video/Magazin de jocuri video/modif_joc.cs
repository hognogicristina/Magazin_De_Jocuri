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
    public partial class modif_joc : UserControl
    {
        public modif_joc()
        {
            InitializeComponent();
        }

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

        private void modif_joc_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=jocuri.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_developer();
            incarca_genul();
            incarca_anul();
            incarca_produse("", "", "");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox1.Text != "")
            //incarca_produse(comboBox1.Text, "", "");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox2.Text != "")
            //incarca_produse("", comboBox2.Text, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int idj = int.Parse(S[0]);
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
                    string q = "UPDATE jocuri set Denumire='" + denumire + "', Developers='" + developer + "', Gen='" + gen + "', An_aparitie=" + an_aparitie + ", Exemplare=" + stoc + ", Platforma='" + platform + "', Singleplayer=" + sp + ", Multiplayer=" + mp + ", Gameplay='" + gameplay + "', Pret=" + pret + " WHERE ID =" +idj;
                    OleDbCommand c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                    incarca_produse("", "", "");
                    q = "DELETE FROM Fotografii WHERE ID_produs=" + idj;
                    c = new OleDbCommand(q, conn);
                    c.ExecuteNonQuery();
                    foreach (string s in fimagini)
                    {
                        q = "INSERT INTO Fotografii (ID_produs, Fotografie) VALUES (" + idj + ",'" + s + "')";
                        c = new OleDbCommand(q, conn);
                        c.ExecuteNonQuery();
                    }
                }
                else MessageBox.Show("Alegeti developerul, genul, anul si platforma!");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox3.Text != "")
            //incarca_produse("", "", comboBox3.Text);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int idp = int.Parse(S[0]);
                groupBox1.Text = idp.ToString();
                string q = "SELECT * FROM jocuri WHERE ID=" + idp;
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Text = dr[2].ToString();
                    comboBox2.Text = dr[3].ToString();
                    comboBox3.Text = dr[4].ToString();
                    textBox1.Text = dr[1].ToString();
                    textBox2.Text = dr[9].ToString();
                    textBox3.Text = dr[10].ToString();
                    numericUpDown1.Value = int.Parse(dr[5].ToString());
                    bool sp = bool.Parse(dr[7].ToString());
                    bool mp = bool.Parse(dr[8].ToString());
                    if (sp) checkBox1.Checked = true;
                    else checkBox1.Checked = false;
                    if (mp) checkBox2.Checked = true;
                    else checkBox2.Checked = false;
                    string platforma = dr[6].ToString();
                    if (platforma.IndexOf("PC") != -1) checkBox3.Checked = true;
                    else checkBox3.Checked = false;
                    if (platforma.IndexOf("PlayStation") != -1) checkBox4.Checked = true;
                    else checkBox4.Checked = false;
                    if (platforma.IndexOf("Xbox") != -1) checkBox5.Checked = true;
                    else checkBox5.Checked = false;
                }
                dr.Close();
                fimagini = new List<string>();
                pimagini = new List<PictureBox>();
                flowLayoutPanel1.Controls.Clear();
                q = "SELECT * FROM Fotografii WHERE ID_produs=" + idp;
                c = new OleDbCommand(q, conn);
                dr = c.ExecuteReader();
                while (dr.Read())
                {
                    string imagine = dr[2].ToString();
                    string fisier = Application.StartupPath + "/imagini/" + imagine;
                    PictureBox p = new PictureBox();
                    p.Load(fisier);
                    p.Width = flowLayoutPanel1.Width - 3;
                    p.Height = 2 * p.Width;
                    //p.SizeMode = PictureBoxSizeMode.StretchImage;
                    flowLayoutPanel1.Controls.Add(p);
                    pimagini.Add(p);
                    fimagini.Add(imagine);
                }
                dr.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                string q = "DELETE FROM jocuri WHERE ID=" + id;
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                q = "DELETE FROM Fotografii WHERE ID_produs=" + id;
                c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                if (listBox1.SelectedIndex == -1)
                    listBox1.SelectedIndex = 0;
            }
        }

        string nume_fisier(string s)
        {
            int i = s.Length - 1;
            while (s[i] != '\\') i--;
            return s.Substring(i + 1);
        }

        private void button3_Click(object sender, EventArgs e)
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
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                string q = "DELETE FROM Fotografii WHERE ID_produs=" + id;
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                flowLayoutPanel1.Controls.Clear();
            }
        }
    }
}
