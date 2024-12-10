using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace npDataComp
{
    public partial class FormDataCmp : Form
    {
        public static SqlConnection dbConnectionSrc = null;
        public static SqlConnection dbConnectionDest = null;
        public FormDataCmp()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((null == dbConnectionSrc) || (null == dbConnectionDest))
            {
                string strDbNameSrc = "npOld";
                string strDbNameDest = "npNew"; //目标数据库的名

                //修改按钮文字
                this.button1.Text = "终止";
                //源数据库连接
                System.Data.SqlClient.SqlConnectionStringBuilder builderSrc = new System.Data.SqlClient.SqlConnectionStringBuilder();
                builderSrc["Data Source"] = "(local)";
                builderSrc["integrated Security"] = true;
                builderSrc["Initial Catalog"] = strDbNameSrc;
                dbConnectionSrc = new SqlConnection(builderSrc.ConnectionString);
                dbConnectionSrc.Open();
                this.richTextBoxLog.AppendText("源数据库" + strDbNameDest + "已打开！\n");

                //源数据库连接
                System.Data.SqlClient.SqlConnectionStringBuilder builderDst = new System.Data.SqlClient.SqlConnectionStringBuilder();
                builderDst["Data Source"] = "(local)";
                builderDst["integrated Security"] = true;
                builderDst["Initial Catalog"] = strDbNameDest;
                dbConnectionDest = new SqlConnection(builderDst.ConnectionString);
                dbConnectionDest.Open();
                this.richTextBoxLog.AppendText("目标数据库" + strDbNameDest + "已打开！\n");
                CompProcessor();//进行比较处理
            }
            else {
                //修改按钮文字
                this.button1.Text = "运行";
                closeDbConnection();
            }   
        }

        public void closeDbConnection()
        {
            if(null != dbConnectionSrc) {
                dbConnectionSrc.Close();
                dbConnectionDest.Close();
                dbConnectionSrc = null;
                dbConnectionDest = null;
            }
        }
    }
}
