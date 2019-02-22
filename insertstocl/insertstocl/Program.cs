using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace insertstocl
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime myDate = DateTime.Now;
            string myDateString = myDate.ToString("yyyyMMdd");

            string xlsPath = "C:\\Users\\user\\TWSE\\";
            //string xlsPath = "C:\\Users\\user\\TWSE\\"+ myDateString + "stock.xls";

            string Today = DateTime.Now.ToString("yyyy/MM/dd tt hh:mm:ss");

            string csvName = myDateString + "stock.csv";
            //string sheetName = "Sheet1";



            //Excel的連線字串

            //HDR(HeaDer Row):YES的表示第一列為標題列不讀取，NO則會讀取第一列

            //IMEX:讀寫的模式，0:Export Mode(寫),1:Import Mode(讀),2:Linked Mode(讀/寫)，一般設定1

            //xlsx格式不適用 
            using (OleDbConnection conn_excel = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + xlsPath + "';Extended Properties = 'Text;HDR=YES;FMT=Delimited;'"))
            //using (OleDbConnection conn_excel = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + xlsPath + "';Extended Properties = 'Excel 8.0;HDR=YES;IMEX=1;'"))
            {

                conn_excel.Open();

                //OleDbCommand cmd_excel = new OleDbCommand("SELECT [Stock_Id],[Stock_Name],[Trading_Volume],[NumberOf_T],[Turnover],[Opening_P],[Highest_P],[Lowest_P],[Closing_P],[Range],[Difference] FROM [" + csvName + "];", conn_excel);
                //OleDbCommand cmd_excel = new OleDbCommand("SELECT [證券代號],[證券名稱],[成交股數],[成交筆數],[成交金額],[開盤價],[最高價],[最低價],[收盤價],[漲跌(+/-)],[漲跌價差] FROM [" + csvName + "];", conn_excel);
                OleDbCommand cmd_excel = new OleDbCommand("SELECT * FROM [" + csvName + "];", conn_excel);
                //OleDbCommand cmd_excel = new OleDbCommand("SELECT [EmployeeNo],[Cname],[Ename],[Travel],[Health] FROM [" + sheetName + "$];", conn_excel);

                OleDbDataReader reader_excel = cmd_excel.ExecuteReader();



                //SQL連線字串

                using (SqlConnection cn_sql = new SqlConnection(@"server=140.120.53.200;uid=ClassManager;pwd=12345678;database=StockManage_2018"))

                {

                    cn_sql.Open();

                    //宣告Transaction

                    SqlTransaction stran = cn_sql.BeginTransaction();

                    try

                    {

                        while (reader_excel.Read())

                        {

                            SqlCommand cmd_sql = new SqlCommand("insert into testtbl (Stock_Id, Stock_Name, Trading_Volume, NumberOf_T, Turnover, Opening_P, Highest_P, Lowest_P, Closing_P, Range, Difference, DateTime) values ('" + reader_excel[1] + "','" + reader_excel[2] + "','" + reader_excel[3] + "','" + reader_excel[4] + "','" + reader_excel[5] + "','" + reader_excel[6] + "','" + reader_excel[7] + "','" + reader_excel[8] + "','" + reader_excel[9] + "','" + reader_excel[10] + "','" + reader_excel[11] + "','" + Today + "')", cn_sql);

                            cmd_sql.Transaction = stran;

                            cmd_sql.ExecuteNonQuery();

                        }

                        //迴圈跑完並一次Insert

                        stran.Commit();

                    }

                    catch (SqlException ex)

                    {

                        Console.Write(ex.Message);

                        Console.Write(ex.Number);

                        stran.Rollback();

                    }

                    catch (OleDbException ex)

                    {

                        Console.Write(ex.Message);

                        stran.Rollback();

                    }

                    catch (Exception ex)

                    {

                        Console.Write(ex.Message);

                        stran.Rollback();

                    }

                    finally

                    {

                        cn_sql.Close();

                        conn_excel.Close();

                        reader_excel.Close();

                    }

                }

            }

        }
    }
}
