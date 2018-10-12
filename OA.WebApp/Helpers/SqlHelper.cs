using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OA.WebApp.Helpers
{
    static class SqlHelper
    {
        //static void Main(string[] args)
        //{
        //    String connectionString = "data source=.;initial catalog=oa;persist security info=True;user id=sa;password=sasasa;MultipleActiveResultSets=True;App=EntityFramework";
        //}

        // 执行命令，如 TRANSACT-SQL INSERT、 DELETE、 UPDATE 和 SET 语句。  
        public static Int32 ExecuteNonQuery(DbContext dbcontext, String commandText, 
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
                    return cmd.ExecuteNonQuery();
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
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static SqlDataReader ExecuteReader(DbContext dbcontext, String commandText,
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
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }

        static void CountCourses(DbContext dbcontext, Int32 year)
        {
            String commandText = "Select Count([CourseID]) FROM [MySchool].[dbo].[Course] Where Year=@Year";
            SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
            parameterYear.Value = year;

            Object oValue = SqlHelper.ExecuteScalar(dbcontext, commandText, CommandType.Text, parameterYear);
            Int32 count;
            if (Int32.TryParse(oValue.ToString(), out count))
                Console.WriteLine("There {0} {1} course{2} in {3}.", count > 1 ? "are" : "is", count, count > 1 ? "s" : null, year);
        }
    }
}
