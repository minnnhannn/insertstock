﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace insertstocl {
    public static class DbConnection {
       
        public static void NonQuery(string sqlString) {
            using (SqlConnection connection = new SqlConnection(sqlString))
            {
                SqlCommand command = new SqlCommand(string.Format("Data Source=140.120.53.200;Persist Security Info=True;User ID=ClassManager;Password=12345678;Initial Catalog=StockManage_2018"));
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
        
    }
}
