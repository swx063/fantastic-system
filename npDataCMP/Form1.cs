using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace npDataCMP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Str_sqlcon1 = "server=(local);Initial Catalog=D_total;Integrated Security=True";
            string Str_sqlcon2 = "Data Source=.;Database=D_total;Userid=sa;PWD=Abcd1234*";
            string Str_sqlcon3 = "Data Source =192.168.1.200;Initial Catalog=D_total ;User Id=sa;Password=123";

            SqlSvrCon mySqlSvr = new SqlSvrCon();
            this.richTextBox1.AppendText("Helllow");//
            mySqlSvr.getcom(Str_sqlcon1);
            //this.richTextBox1.AppendText("Helllow");//


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comb = (ComboBox)sender;
            if (comb.SelectedItem.ToString().Equals("sa")) {
                this.textBoxPassword.Enabled=true;
            }
            else {
                this.textBoxPassword.Enabled = false;
            }
        }
    }
}
