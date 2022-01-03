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
    public partial class viz_joc : UserControl
    {
        public viz_joc()
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
            comboBox6.Items.Clear();
            while (dr.Read())
            {
                comboBox6.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }

        private void incarca_genul()
        {
            string q = "SELECT * FROM Gen ORDER BY Genul";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            comboBox5.Items.Clear();
            while (dr.Read())
            {
                comboBox5.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }

        private void incarca_produse(string dev, string gen, string sp, string mp, string pl)
        {
            string q = "SELECT * FROM jocuri ";
            if (dev != "" && gen != "" && sp != "" && mp != "" && pl != "") q = q + "WHERE Developers='" + dev + "' AND Gen ='" + gen + "' AND Singleplayer = " + sp + " AND Multiplayer = " + mp + " AND Platforma like '%" + pl + "%'";
            else if (dev != "") q = q + "WHERE Developers='" + dev + "'";
            else if (gen != "") q = q + "WHERE Gen = '" + gen + "'";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            listBox1.Items.Clear();
            while (dr.Read())
                listBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            dr.Close();
        }


        private void viz_joc_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=jocuri.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_developer();
            incarca_genul();
            incarca_produse("", "", "", "", "");
            fimagini = new List<string>();
            pimagini = new List<PictureBox>();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.Text != "")
                incarca_produse(comboBox6.Text, "", "", "", "");
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "")
                incarca_produse("", comboBox5.Text, "", "", "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sp = "", mp = "", pl="";
            if (checkBox10.Checked) sp = "true";
            else sp = "false";
            if (checkBox9.Checked) mp = "true";
            else mp = "false";
            if (checkBox8.Checked) pl = pl + "PC ";
            if (checkBox7.Checked) pl = pl + "Playstation";
            if (checkBox6.Checked) pl = pl + "Xbox";
            if (comboBox6.Text != "" && comboBox5.Text != "")
                incarca_produse(comboBox6.Text, comboBox5.Text, sp, mp, pl);
            else MessageBox.Show("Completeaza toate campurile!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            incarca_produse("", "", "", "", "");
            comboBox6.Text = "";
            comboBox5.Text = "";
            checkBox10.Checked = false;
            checkBox9.Checked = false;
            checkBox8.Checked = false;
            checkBox7.Checked = false;
            checkBox6.Checked = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int idp = int.Parse(S[0]);
                groupBox1.Text = idp.ToString();
                string q = "SELECT * FROM jocuri WHERE ID=" + idp;
                OleDbCommand c = new OleDbCommand(q, conn);
                OleDbDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {
                    textBox4.Text = dr[2].ToString();
                    textBox5.Text = dr[3].ToString();
                    textBox6.Text = dr[4].ToString();
                    textBox1.Text = dr[1].ToString();
                    textBox2.Text = dr[9].ToString();
                    textBox3.Text = dr[10].ToString();
                    textBox7.Text = dr[5].ToString();
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
    }
}
