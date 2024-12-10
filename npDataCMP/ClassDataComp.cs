using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace npDataComp
{
    public partial class FormDataCmp : Form
    {
        public void CompProcessor()
        {
            if ((null == dbConnectionSrc) || (null == dbConnectionDest))
            {
                return;
            }

            this.richTextBoxLog.AppendText("开始比较数据……\n");
            const string queryString = "SELECT * FROM [dbo].[GuoBang] order by DATE_HP desc";
            // Create the command.
            DbCommand command = dbConnectionSrc.CreateCommand();
            command.CommandText = queryString;
            command.CommandType = CommandType.Text;

            // Retrieve the data.
            DbDataReader reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read() && (i < 100))
            {                
                this.richTextBoxLog.AppendText(reader[0] + "," + reader[1] + "," + reader[2] + "," + reader[3] + "\n");
                i++;
            }
            processFileContent();
            if (tablesHashtable.Count <=0)
            {
                MessageBox.Show("获取文件中所含表格名称失败，即不含有任何表格名");
            }

        }


}
}
