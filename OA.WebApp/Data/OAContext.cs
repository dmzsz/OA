using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OA.WebApp.Data
{
    public class OAContext : OABaseContext
    {
        public OAContext (DbContextOptions<OAContext> options)
            : base(options)
        {
        }

        public DbSet<OA.WebApp.Models.Employee> Employees { get; set; }

        public DbSet<OA.WebApp.Models.User> Users { get; set; }
        
        public DbSet<OA.WebApp.Models.Company> Companies { get; set; }

        public DbSet<OA.WebApp.Models.Department> Departments { get; set; }

        public DbSet<OA.WebApp.Models.Role> Roles { get; set; }

        public DbSet<OA.WebApp.Models.Privilege> Privileges { get; set; }

        public DbSet<OA.WebApp.Models.UserPrivilege> UserPrivileges { get; set; }

    }
}
