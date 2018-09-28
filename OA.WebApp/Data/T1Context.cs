using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MySql.Data.MySqlClient;
using OA.WebApp.Models;
using OA.WebApp.ViewModels;

namespace OA.WebApp.Data
{
    public class T1Context
    {
        public string ConnectionString { get; set; }

        public T1Context(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public MySqlDataReader GetDataReader(string sql)
        {
            MySqlDataReader reader;
            //连接数据库
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            ////查找数据库里面的表
            MySqlCommand mscommand = new MySqlCommand(sql, connection);
            reader = mscommand.ExecuteReader();
            connection.Close();

            return reader;
        }
    }
}
