using System.Data;
using MySql.Data.MySqlClient;

namespace OA.WebApp.Data
{
    public class T1Context
    {
        public string ConnectionString { get; set; }

        public T1Context(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public IDataReader GetDataReader(string sql)
        {
            MySqlDataReader reader;
            //连接数据库
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(sql, connection))
                {

                    reader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt.CreateDataReader();
                }
            }
        }
    }
}
