using Microsoft.EntityFrameworkCore;
using XeroTechnicalTest.Domain.Models;

namespace XeroTechnicalTest.Domain
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data source=products.db");
    }
}