using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OA.WebApp.Models;
using System.Linq;

namespace OA.WebApp.Data
{
    public class OAContext : DbContext
    {
        public OAContext (DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings => 
                                   warnings.Throw(CoreEventId.IncludeIgnoredWarning));
        }

        public DbSet<OA.WebApp.Models.Employee> Employees { get; set; }

        public DbSet<OA.WebApp.Models.User> Users { get; set; }
        
        public DbSet<OA.WebApp.Models.Company> Companies { get; set; }

        public DbSet<OA.WebApp.Models.Department> Departments { get; set; }

        public DbSet<OA.WebApp.Models.Role> Roles { get; set; }

        public DbSet<OA.WebApp.Models.Privilege> Privileges { get; set; }

        public DbSet<OA.WebApp.Models.UserPrivilege> UserPrivileges { get; set; }

        public DbSet<OA.WebApp.Models.Vessel> Vessels { get; set; }
        /// <summary>
        /// 判断SqlDataReader是否存在某列
        /// </summary>
        /// <param name="dr">SqlDataReader</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        private bool readerExists(DbDataReader dr, string columnName)
        {

            dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";

            return (dr.GetSchemaTable().DefaultView.Count > 0);

        }

        ///<summary>
        ///利用反射和泛型将SqlDataReader转换成List模型
        ///</summary>
        ///<param name="sql">查询sql语句</param>
        ///<returns></returns>

        public async Task<IList<T>> ExecuteToListAsync<T>(
            string sql, List<SqlParameter> paramList = null) where T : new()
        {
            IList<T> list;

            Type type = typeof(T);

            string tempName = string.Empty;
            using (var connection = new SqlConnection(this.Database.GetDbConnection().ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                if (paramList != null)
                {
                    command.Parameters.AddRange(paramList.ToArray<SqlParameter>());
                }
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        list = new List<T>();
                        while (await reader.ReadAsync())
                        {
                            T t = new T();

                            PropertyInfo[] propertys = t.GetType().GetProperties();

                            foreach (PropertyInfo pi in propertys)
                            {
                                tempName = pi.Name;

                                if (readerExists(reader, tempName))
                                {
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
                            }
                            list.Add(t);
                        }
                        return list;
                    }
                }
                return null;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserID, ur.RoleID });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleID);


            modelBuilder.Entity<UserCompany>()
                .HasKey(ur => new { ur.UserID, ur.CompanyID });

            modelBuilder.Entity<UserCompany>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserCompanies)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserCompany>()
                .HasOne(uc => uc.Company)
                .WithMany(r => r.UserCompanies)
                .HasForeignKey(uc => uc.CompanyID);

            //modelBuilder.Entity<Company>()
            //    .HasOne(c => c.ParentCompany)
            //    .WithMany(c => c.ChildrenCompanies)
            //    .HasForeignKey(c => c.ParentCompanyID);



            modelBuilder.Entity<UserDepartment>()
                .HasKey(ur => new { ur.UserID, ur.DepartmentID });

            modelBuilder.Entity<UserDepartment>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserDepartments)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserDepartment>()
                .HasOne(uc => uc.Department)
                .WithMany(r => r.UserDepartments)
                .HasForeignKey(uc => uc.DepartmentID);

            //modelBuilder.Entity<Department>()
            //    .HasOne(c => c.ParentDepartment)
            //    .WithMany(c => c.ChildrenDepartments)
            //    .HasForeignKey(c => c.ParentDepartmentID);


            modelBuilder.Entity<RolePrivilege>()
                .HasKey(ur => new { ur.RoleID, ur.PrivilegeID });

            modelBuilder.Entity<RolePrivilege>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePrivileges)
                .HasForeignKey(rp => rp.RoleID);

            modelBuilder.Entity<RolePrivilege>()
                .HasOne(rp => rp.Privilege)
                .WithMany(p => p.RolePrivileges)
                .HasForeignKey(rp => rp.PrivilegeID);
        }
    }
}
