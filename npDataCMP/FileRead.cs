using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace npDataComp
{
    public partial class FormDataCmp : Form
    {
        public static Hashtable tablesHashtable = new Hashtable(); //承载表格名称和字段（列值）
        public static Hashtable tablesHashtableContents = new Hashtable();//承载表格所有的内容（行值）
        public void processFileContent() {
            //FileStream file = new FileStream("RedoScript-11-30-updated.sql", FileMode.Open, FileAccess.Read);
            string strPath = "D:\\RedoScript-11-30-updated.sql"; // 替换为你的文件路径  
            try
            {
                //StreamReader sr = new StreamReader(strPath);
                //string strLine;
                //while ((strLine = sr.ReadLine()) != null)
                string[] strLines = File.ReadAllLines(strPath);
                
                for(int iLineNumber =0; iLineNumber < strLines.Length; iLineNumber++)               
                {
                    
                    String strSingleLine = strLines[iLineNumber];
                    if (0 == strSingleLine.Length) { continue; }
                    //Console.WriteLine(strLine); // 输出读取的每一行
                    if (strSingleLine.StartsWith("INSERT INTO ")){
                        //[dbo].[GuoBang]
                        string strTableName = getMyTableName(strSingleLine);
                        //判断是否该表已有记录存在
                        if (tablesHashtable.ContainsKey(strTableName))
                        {
                            //表格已经存在
                            //获取；表格的字段，跟现有的字段进行对比，是否都对得上，以免影响每列数据的对应关系
                            string[] strFields = getMyTableFields(strSingleLine);
                            string [] existingField = (string[])(tablesHashtable[strTableName]);
                            if (isSameFiledsAndOrder(strTableName, strFields, existingField))
                            {
                                this.richTextBoxLog.AppendText("Same fields for table " + strTableName + " entry.\n");
                            }
                            else {
                                this.richTextBoxLog.AppendText("ERROR: DIFF fields for table " + strTableName + " entry.\n");
                            }

                        }
                        else {
                            //表格还没存在，建立表格名与其列名称
                            string[] strFields = getMyTableFields(strSingleLine);
                            tablesHashtable.Add(strTableName, strFields);//把表格和字段压入保存
                            tablesHashtableContents.Add(strTableName, null);//把表格名压入保存


                            this.richTextBoxLog.AppendText("Adding table:" + strTableName + "...\n"); // 输出读取的每一行
                            this.richTextBoxLog.AppendText("Adding files for table:" + strTableName + "\n" + strFields[0] + " — " + strFields[strFields.Length -1] +  ", TOTAL " + strFields.Length  + " fields.\n"); // 输出读取的每一行
                        }
    }
                    else if (strSingleLine.StartsWith("UPDATE "))
                    {

                    }
                    else if (strSingleLine.StartsWith("DELETE FROM "))
                    {

                    }
                    else
                    {
                        String[] strWords = strSingleLine.Split(' ');
                        Console.WriteLine("Line " +  iLineNumber + "begins with unkonw key operation" + strWords[0] + " " + strWords[1]); // 输出读取的每一行
                    }                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to read file " + strPath + ", exception:" + ex.Message);
            }
        }
        //从一行中获取该操作所作的表名
        public string getMyTableName(string strLine)
        {
            int indexOfFangKuohao = strLine.IndexOf("[");
            int indexOfArchSign = strLine.IndexOf("(");
            if ((indexOfFangKuohao <= 0)||(indexOfArchSign <=0) || (indexOfArchSign <= indexOfFangKuohao))
            {
                return null;
            }
            string strTabString = strLine.Substring(indexOfFangKuohao, indexOfArchSign - indexOfFangKuohao);
            return strTabString;
        }

        //读取一条SQL语句中包含的所有字段
        public string[] getMyTableFields(string strLine)
        {
            //int indexOfFangKuohao = strLine.IndexOf("[");
            int indexOfArchSign = strLine.IndexOf("(");
            int indexOfEndArchSign = strLine.IndexOf(")");
            if ((indexOfArchSign <= 0) || (indexOfEndArchSign <= 0) || (indexOfEndArchSign <= indexOfArchSign))
            {
                return null;
            }
            //创建字段列表，作为返回值供后面使用
            ArrayList myFields = new ArrayList();
            string strFieldsAll = strLine.Substring(indexOfArchSign + 1, indexOfEndArchSign - indexOfArchSign);
            if (strFieldsAll.Length <= 0) { return null; }
            string[] arrStrfileds = strFieldsAll.Split(',');
            for (int i = 0; i < arrStrfileds.Length; i++)
            {
                arrStrfileds[i].TrimStart().TrimEnd().Trim();
            }
            return arrStrfileds;
        }

        public bool isSameFiledsAndOrder(string tabName, string[] arrStrExistingFields, string[] arrStrfields2Cmp)
        {
            if(arrStrExistingFields.Length != arrStrfields2Cmp.Length)
            {
                return false;
            }
            for (int i = 0; i < arrStrExistingFields.Length; i++)
            {
                string strExistingField = arrStrExistingFields[i];
                string strFieldCmp = arrStrfields2Cmp[i];
                if (!strExistingField.Equals(strFieldCmp))
                {
                    this.richTextBoxLog.AppendText("ERROR: table " + tabName + " has diffrent field at filed #" + (i + 1) + ", existing field: " + strExistingField + ", field2Cmp " + arrStrfields2Cmp);
                    return false;
                }
            }

            return true;
        }

    }

}
