using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OA.WebApp.Data;

namespace OA.WebApp.Helpers
{
    static class SqlHelper
    {
       
        // 执行命令，如 TRANSACT-SQL INSERT、 DELETE、 UPDATE 和 SET 语句。  
        public static Task<Int32> ExecuteNonQueryAsync(DbContext dbcontext, String commandText, 
            CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(
                dbcontext.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect   
                    // type is only for OLE DB.    
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQueryAsync();
                }
            }
        }


        /// 从数据库中检索单个值 （例如，一个聚合值)  
        public static Object ExecuteScalar(DbContext dbcontext, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(
                dbcontext.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalarAsync();
                }
            }
        }

        public static Task<SqlDataReader> ExecuteReaderAsync(DbContext dbcontext, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(
                dbcontext.Database.GetDbConnection().ConnectionString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the   
                // IDataReader is closed.
                return cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
        }

        public static async Task<IEnumerable<T>> ReaderToListAsync<T>(DbDataReader reader) where T : new()
        {
            IList<T> list = new List<T>();
            string tempName = string.Empty;

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    T t = new T();

                    PropertyInfo[] propertys = t.GetType().GetProperties();

                    foreach (PropertyInfo pi in propertys)
                    {
                        tempName = pi.Name;

                        if (!pi.CanWrite)
                        {
                            continue;
                        }
                        var value = reader[tempName];

                        if (value != DBNull.Value)
                        {
                            pi.SetValue(t, value, null);
                        }
                    }
                    list.Add(t);
                }
            }
            return list;
        }

        public static SqlParameter[] setParamsFrom(object obj)
        {
            //得到对象的类型
            Type type = obj.GetType();
            //得到字段的值,只能得到public类型的字典的值
            PropertyInfo[] propertys = type.GetProperties();
            int length = propertys.Length;
            SqlParameter[] sqlParameter = new SqlParameter[propertys.Length];

            //SqlParameter parameterAmount = new SqlParameter("@Amount", SqlDbType.Int);
            //parameterAmount.Value = journal.Amount;

            for (int i = 0; i < length; i++)
            {
                //字段名称
                string fieldName = propertys[i].Name;
                //字段类型
                Type fieldType = propertys[i].GetType();
                //字段的值
                object fieldValue = propertys[i].GetValue(obj); // == null  ? "" : propertys[i].GetValue(journal).ToString();

                Type dateTimeType = new DateTime().GetType();
                if (fieldValue == null) fieldValue = DBNull.Value;
                sqlParameter[i] = new SqlParameter("@" + fieldName, fieldValue);
            }
            return sqlParameter;
        }
    }
}
