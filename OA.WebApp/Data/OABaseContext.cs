using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OA.WebApp.Data
{
    public class OABaseContext : DbContext
    {
        public OABaseContext(DbContextOptions<OAContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
                                    ? HttpContext.Current.User.Identity.Name
                                    : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).AddedAt = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).AddedBy = currentUsername;
                }

                ((BaseEntity)entity.Entity).ModifiedAt = DateTime.UtcNow;
                ((BaseEntity)entity.Entity).ModifiedBy = currentUsername;
            }
        }
    }
}
