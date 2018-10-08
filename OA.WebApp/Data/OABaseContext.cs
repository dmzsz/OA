using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OA.WebApp.Models;

namespace OA.WebApp.Data
{
    //public class OABaseContext : DbContext
    //{
    //    public OABaseContext(DbContextOptions options)
    //        : base(options)
    //    {
    //    }
        
    //    public override int SaveChanges()
    //    {
    //        AddTimestamps();
    //        return base.SaveChanges();
    //    }

    //    public override async Task<int> SaveChangesAsync(
    //        bool acceptAllChangesOnSuccess, 
    //        CancellationToken cancellationToken)
    //    {
    //        AddTimestamps();
    //        return await base.SaveChangesAsync();
    //    }

    //    private void AddTimestamps()
    //    {
    //        var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

    //        var currentUsername = "Anonymous";
    //            //!string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
    //            //                    ? HttpContext.Current.User.Identity.Name
    //            //                    : "Anonymous";

    //        foreach (var entity in entities)
    //        {
    //            if (entity.State == EntityState.Added)
    //            {
    //                ((BaseEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
    //                ((BaseEntity)entity.Entity).CreatedBy = currentUsername;
    //            }

    //            ((BaseEntity)entity.Entity).ModifiedAt = DateTime.UtcNow;
    //            ((BaseEntity)entity.Entity).ModifiedBy = currentUsername;
    //        }
    //    }
    //}
}
