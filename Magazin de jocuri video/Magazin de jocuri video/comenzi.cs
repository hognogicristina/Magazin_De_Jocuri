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
    public partial class comenzi : Form
    {
        public comenzi()
        {
            InitializeComponent();
        }

        OleDbConnection conn;

        private void incarca_comenzi()
        {
            string q = "SELECT * FROM Comenzi WHERE Stare = 'finalizata'"; 
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(c);
            DataTable t = new DataTable();
            da.Fill(t);
            dataGridView1.DataSource = t;
            q = "SELECT * FROM Comenzi WHERE Stare = 'plasata'";
            c = new OleDbCommand(q, conn);
            OleDbDataReader dr = c.ExecuteReader();
            listBox1.Items.Clear();
            while (dr.Read())
            {
                string[] d = dr[3].ToString().Split();
                listBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString() + " - " + dr[2].ToString() + " - " + d[0]+" - "+dr[6].ToString()+" lei");
            }
            dr.Close();
        }

        private void comenzi_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            string cs = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = jocuri.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
            incarca_comenzi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.Text.Split();
                int idc = int.Parse(S[0]);
                string dexp = DateTime.Now.ToShortDateString();
                string q = "UPDATE Comenzi SET Data_exp ='" + dexp + "', Stare = 'finalizata' WHERE ID = " + idc;
                OleDbCommand c = new OleDbCommand(q, conn);
                c.ExecuteNonQuery();
                //update stoc
                S = listBox1.Text.Split('-');
                string[] ids = S[2].Split();
                foreach(string s in ids)
                {
                    if (s != "")
                    {
                        q = "UPDATE Jocuri SET Exemplare = Exemplare-1 WHERE ID=" + s;
                        c = new OleDbCommand(q, conn);
                        c.ExecuteNonQuery();
                    }
                }
                incarca_comenzi();
                dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.Text.Split('-');
                string idc = S[1];
                string[] PM = S[2].Split();
                int i = 0;
                string a = "";
                string q;
                OleDbCommand c;
                OleDbDataReader dr;
                foreach (string s in PM)
                {
                    if (s != "")
                    {
                        i++;
                        if (i % 2 == 1)
                        {
                            q = "SELECT * FROM Jocuri WHERE ID=" + s;
                            c = new OleDbCommand(q, conn);
                            dr = c.ExecuteReader();
                            while (dr.Read())
                            {
                                a = a + dr[0].ToString() + " - " + dr[1].ToString() + " " + dr[2].ToString() + " ";
                            }
                        }
                    }
                }
            }
        }
    }
}
