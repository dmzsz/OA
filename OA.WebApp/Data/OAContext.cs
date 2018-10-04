using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OA.WebApp.Models;

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
                .ConfigureWarnings(warnings => warnings.Throw(CoreEventId.IncludeIgnoredWarning));
        }

        public DbSet<OA.WebApp.Models.Employee> Employees { get; set; }

        public DbSet<OA.WebApp.Models.User> Users { get; set; }
        
        public DbSet<OA.WebApp.Models.Company> Companies { get; set; }

        public DbSet<OA.WebApp.Models.Department> Departments { get; set; }

        public DbSet<OA.WebApp.Models.Role> Roles { get; set; }

        public DbSet<OA.WebApp.Models.Privilege> Privileges { get; set; }

        public DbSet<OA.WebApp.Models.UserPrivilege> UserPrivileges { get; set; }

        public DbSet<OA.WebApp.Models.Vessel> Vessels { get; set; }

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
