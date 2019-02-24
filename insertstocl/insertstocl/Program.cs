using System;
using System.Collections.Generic;
using System.Data;
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

            DataTable table = DbConnection.GetData("SELECT @@VERSION");

                

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
            List<Stock> stocks = new List<Stock>();

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
                        stocks.Add(new Stock(stockinfo, TodayEn)); 
                    }  
                }
                fileReader.Close();
            }
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("StockTable", Stock.SetStockTable(stocks) ));
            DbConnection.ExecuteProc("[dbo].[InsertStockData]",parameters);
        }
    }
}
