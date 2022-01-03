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
    public partial class cump_joc : Form
    {
        public cump_joc()
        {
            InitializeComponent();
        }

        public string idu = "";
        int pret;

        OleDbConnection conn;

        private void incarca_produse(string dev, string gen)
        {
            string q = "SELECT * FROM jocuri WHERE Exemplare >0";
            /*if (dev != "" && gen != "") q = q + "WHERE Developers='" + dev + "' AND Gen ='" + gen + "'";
            else if (dev != "") q = q + "WHERE Developers='" + dev + "'";
            else if (gen != "") q = q + "WHERE Gen = '" + gen + "'";*/
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            listBox1.Items.Clear();
            while (dr.Read())
                listBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString() + " - " + dr[10].ToString() + " lei");
            dr.Close();
        }

        private void cump_joc_Shown(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=jocuri.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_produse("", "");
            this.WindowState = FormWindowState.Maximized;
            textBox1.Text = idu;
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                /*string[] S = listBox1.SelectedItem.ToString().Split();
                int idp = int.Parse(S[0]);
                string detalii = S[2];
                for (int i = 3; i < S.Length; i++)
                    if (S[i] != "") detalii = detalii + " " + S[i];
                listBox2.Items.Add(idp + " - " + detalii);*/
                listBox2.Items.Add(listBox1.SelectedItem);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "") 
            {
                button4_Click(sender, e);
                string idu = textBox1.Text;
                string q;
                string id_pm = "";
                foreach (string s in listBox2.Items)
                {
                    string[] S = s.Split();
                    id_pm = id_pm + " " + S[0];
                }
                string dad = DateTime.Now.ToShortDateString();
                q = "insert into Comenzi(ID_Client, ID_PM, Data_ad, Stare, Suma_incasata) VALUES ('" + idu + "', '" + id_pm + "', '" + dad + "', 'plasata',"+pret+")";
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                listBox2.Items.Clear();
                label6.Text = "";
                MessageBox.Show("Multumim ca ati cumparat de la noi! ^_^");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pret = 0;
            for(int i=0; i<listBox2.Items.Count;i++)
            {
                string[] S = listBox2.Items[i].ToString().Split();
                pret = pret + int.Parse(S[S.Length - 2]);
            }
            label6.Text = label6.Text + " " + pret + " lei";
        }
    }
}

