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
        static void Main() {

            DateTime myDate = DateTime.Now;
            //string myDateString = myDate.ToString("yyyyMMdd");
            string myDateString = "20190222";
            string Today = DateTime.Now.ToString("yyyy/MM/dd tt hh:mm:ss");
            string TodayEn = String.Empty;

            if (DateTime.Now.ToString("tt").ToString() == "上午")
            {
                TodayEn = Today.Replace("上午","AM");
            }
            else if (DateTime.Now.ToString("tt").ToString() == "下午")
            {
                TodayEn = Today.Replace("下午", "PM");
            }

            string csvName = "C:\\Users\\user\\TWSE\\" + myDateString + "stock.csv";
            string line = String.Empty;
            //string temp;
            string[] sptemp;
            string temp;
            string NumberOf_T;
            string[] stockinfo;


            using (StreamReader fileReader = new StreamReader(csvName))
            {
                if (null != (line = fileReader.ReadLine())) // skip header
                {
                    while ((line = fileReader.ReadLine()) != null)
                    {
                        sptemp = line.Split('\"');
                        NumberOf_T = sptemp[3].ToString().Replace(",","");
                        temp = sptemp[0] + NumberOf_T + sptemp[6];
                        stockinfo = temp.Split(',');

                        DbConnection.NonQuery("insert into Stock (Stock_Id, Stock_Name, NumberOf_T, Opening_P, Highest_P, Lowest_P, Closing_P, Range, Difference, DateTime) values ('" + stockinfo[1] + "',N'" + stockinfo[2] + "'," + stockinfo[3] + "," + stockinfo[4] + "," + stockinfo[5] + "," + stockinfo[6] + "," + stockinfo[7] + ",'" + stockinfo[8] + "'," + stockinfo[9] + ",'" + TodayEn + "')");
                    }  
                }
                fileReader.Close();
            }
            
        }
    }
}
