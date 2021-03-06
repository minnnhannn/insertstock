﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace insertstocl {
    public static class DbConnection {
        
        public static DataTable GetData( string sqlstring)
        {
            try
            {
                var table = new DataTable();
                using (SqlConnection sqlConn = new SqlConnection("Data Source=140.120.53.200;Persist Security Info=True;User ID=ClassManager;Password=12345678;Initial Catalog=StockManage_2018"))
                {
                    sqlConn.Open();
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = sqlConn;
                    sqlComm.CommandTimeout = 3000;
                    sqlComm.CommandText = sqlstring;

                    var sqlDapter = new SqlDataAdapter(sqlComm);
                    table.Locale = CultureInfo.InvariantCulture;
                    sqlDapter.Fill(table);
                    return table;
                }
            }
            catch (SqlException e)
            {
                return null;
            }
        }
        public static DataTable ExecuteProc(string procName, List<SqlParameter> parameterList = null)
        {
            var table = new DataTable();
           
                SqlConnection sqlConn = new SqlConnection("Data Source=140.120.53.200;Persist Security Info=True;User ID=ClassManager;Password=12345678;Initial Catalog=StockManage_2018";
                var myCommand = new SqlCommand("[StockManage_2018]." + procName, sqlConn);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = 120;
                if (parameterList != null)
                    foreach (var param in parameterList)
                    {
                        myCommand.Parameters.Add(param);
                    }

                var sqlDapter = new SqlDataAdapter(myCommand);
                table.Locale = CultureInfo.InvariantCulture;
                sqlDapter.Fill(table);
            return table;
        }
    }
}
