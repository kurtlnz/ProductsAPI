using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(_ => _.Name)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data source=App_Data/products.db");

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            this.SetUpdatedAtTime();
            
            this.ValidateEntities();
            
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

        private void ValidateEntities()
        {
            var errors = string.Empty;
            foreach (var entry in ChangeTracker.Entries())
            {
                var ctx = new ValidationContext(entry.Entity);
                
                var results = new List<ValidationResult>();
                
                if (!Validator.TryValidateObject(entry.Entity, ctx, results, true))
                {
                    foreach (var result in results)
                    {
                        errors += result.ErrorMessage;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(errors))
                throw new ValidationException($"Failed to save changes to database - {errors}");
        }
    }
} 