using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace npDataCMP
{
       public partial class SqlSvrCon : Form
        {
            #region 全局变量
            //定义一个SqlConnection类型的静态公共变量My_con，用于判断数据库是否连接成功
            public static SqlConnection My_con;
        //定义数据库连接字符串
        //public static string Str_sqlcon1 = "server=(local);Initial Catalog=D_total;Integrated Security=True";
        public static string Str_sqlcon1 = "server=LAPTOP-33II715B;Initial Catalog=NP001;Integrated Security=True";
        public static string Str_sqlcon2 = "Data Source=.;Database=D_total;Userid=sa;PWD=123";
            public static string Str_sqlcon3 = "Data Source =192.168.1.200;Initial Catalog=D_total ;User Id=sa;Password=123";
            #endregion
            /// <summary>
            /// 建立数据库连接
            /// </summary>
            /// <returns></returns>
            public static SqlConnection getcon()
            {
                My_con = new SqlConnection(Str_sqlcon1);//用SqlConnection对象与指定的数据库相连接  
                My_con.Open();//打开数据库连接
                return My_con;//返回SqlConnection对象信息
            }
            /// <summary>
            /// 关闭数据库连接
            /// </summary>
            public void con_close()
            {
                if (My_con.State == ConnectionState.Open)//判断是否打开与数据库的连接
                {
                    My_con.Close();//关闭数据库的连接
                    My_con.Dispose();//释放My_con变量的所有空间
                }
            }
            /// <summary>
            /// 以只读方式读取数据库信息
            /// </summary>
            /// <param name="SQLstr">表示传递的SQL语句</param>
            /// <returns></returns>
            public SqlDataReader getcom(string SQLstr)
            {
                getcon();//打开数据库连接
                SqlCommand My_com = My_con.CreateCommand();//创建SqlConnection对象，用于执行SQL语句
                My_com.CommandText = SQLstr;//获取指定的SQL语句
                SqlDataReader My_read = My_com.ExecuteReader();//执行SQL语句，生成一个SqlDataReader对象
                return My_read;
            }
            /// <summary>
            /// 通过SqlCommand对象执行数据库中的添加、修改和删除操作
            /// </summary>
            /// <param name="SQLstr">表示传递的SQL语句</param>
            public void getsqlcom(string SQLstr)
            {
                getcon();//打开数据库连接
                SqlCommand SQLcom = new SqlCommand(SQLstr, My_con);//创建SqlConnection对象，用于执行SQL语句
                SQLcom.ExecuteNonQuery();//执行SQL语句
                SQLcom.Dispose();//释放所有空间
                con_close();//关闭数据库连接
            }
            /// <summary>
            /// 通过SqlCommand对象执行数据库中的添加、修改和删除操作
            /// </summary>
            /// <param name="SQLstr">传递的SQL语句</param>
            /// <param name="tableName"></param>
            /// <returns>DataSet</returns>
            public DataSet getDataSet(string SQLstr, string tableName)
            {
                getcon();//打开数据库连接
                SqlDataAdapter SQLda = new SqlDataAdapter(SQLstr, My_con);
                DataSet ds = new DataSet();//创建DataSet对象
                SQLda.Fill(ds, tableName);
                con_close();//关闭数据库连接
                return ds;//返回DataSet对象信息
            }
            /// <summary>
            /// 通过SqlCommand对象执行数据库中的添加、修改和删除操作
            /// </summary>
            /// <param name="SQLstr">传递的SQL语句</param>
            /// <param name="tableName"></param>
            /// <returns>DataTable</returns>
            public DataTable getDataTable(string SQLstr, string tableName)
            {
                DataTable dt = new DataTable();
                getcon();//打开数据库连接
                SqlDataAdapter SQLda = new SqlDataAdapter(SQLstr, My_con);
                DataSet ds = new DataSet();
                SQLda.Fill(ds, tableName);
                dt = ds.Tables[tableName];
                con_close();//关闭数据库连接
                return dt;//返回DataSet对象信息
            }
        }

    }
