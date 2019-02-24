using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insertstocl
{
    public class Stock
    {
        public Stock(string[] sr,string date) {
            StockId = sr[1];
            Stock_Name = sr[2];
            NumberOf_T = sr[3];
            Opening_P = sr[4];
            Highest_P = sr[5];
            Lowest_P = sr[6];
            Closing_P = sr[7];
            Range = sr[8];
            Difference = sr[9];
            DateTime = date;
        }

        public string StockId { get; set; }
        public string Stock_Name { get; set; }
        public string NumberOf_T { get; set; }
        public string Opening_P { get; set; }
        public string Highest_P { get; set; }
        public string Lowest_P { get; set; }
        public string Closing_P { get; set; }
        public string Range { get; set; }
        public string Difference { get; set; }
        public string DateTime { get; set; }

        public static DataTable StockTable() {
            DataTable table = new DataTable();
            table.Columns.Add("StockId", typeof(string));
            table.Columns.Add("Stock_Name", typeof(string));
            table.Columns.Add("NumberOf_T", typeof(string));
            table.Columns.Add("Opening_P", typeof(string));
            table.Columns.Add("Highest_P", typeof(string));
            table.Columns.Add("Lowest_P", typeof(string));
            table.Columns.Add("Closing_P", typeof(string));
            table.Columns.Add("Range", typeof(string));
            table.Columns.Add("Difference", typeof(string));
            table.Columns.Add("DateTime", typeof(string));
            return table;
        }
        public static DataTable SetStockTable(List<Stock> stocks) {
            DataTable table = StockTable();
            foreach (Stock s in stocks) {
                DataRow newRow = table.NewRow();
                newRow["StockId"] = s.StockId;
                newRow["Stock_Name"] = s.Stock_Name;
                newRow["NumberOf_T"] = s.NumberOf_T;
                newRow["Opening_P"] = s.Opening_P;
                newRow["Highest_P"] = s.Highest_P;
                newRow["Lowest_P"] = s.Lowest_P;
                newRow["Closing_P"] = s.Closing_P;
                newRow["Range"] = s.Range;
                newRow["Difference"] = s.Difference;
                newRow["DateTime"] = s.DateTime;
                table.Rows.Add(newRow);
            }
            return table;
        }
    }
}
