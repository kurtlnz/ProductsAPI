using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XeroTechnicalTest.Domain.Models;

namespace XeroTechnicalTest.Domain
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data source=App_Data/products.db");

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            this.SetUpdatedAtTime();
            
            return await base.SaveChangesAsync
            (
                acceptAllChangesOnSuccess,
                cancellationToken
            );
        }

        /// <summary>
        ///     Automatically updates the "UpdatedAt" property on all entities.
        /// </summary>
        /// <see>
        ///     http://stackoverflow.com/questions/34951613/entity-framework-7-savechanges
        /// </see>
        private void SetUpdatedAtTime()
        {
            this.ChangeTracker.Entries()
                .Where(_ => _.State != EntityState.Unchanged)
                .Where(_ => _.State != EntityState.Detached)
                .Select(_ => _.Entity).OfType<BaseModel>().ToList()
                .ForEach(_ => _.UpdatedAt = DateTime.UtcNow);
        }
    }
} 