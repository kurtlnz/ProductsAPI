using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XeroTechnicalTest.Domain;
using XeroTechnicalTest.Domain.Models;

namespace XeroTechnicalTest.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly DataContext _dataContext;

        public ProductRepository(ILogger<ProductRepository> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }
        
        public async Task<Product> GetProductAsync(Guid id)
        {
            _logger.LogInformation($"Getting product with id `{id}` from database");

            var product = await _dataContext.Products
                    .AsNoTracking()
                    .SingleOrDefaultAsync(_ => _.Id == id);

            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            _logger.LogInformation($"Getting all products from database");

            var products = await _dataContext.Products.ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetAllProductsByNameAsync(string name)
        {
            _logger.LogInformation($"Getting all products from database with name `{name}`");

            var products = await _dataContext.Products
                    .Where(_ => _.Name.ToLower() == name.ToLower())
                    .ToListAsync();

            return products;
        }
        
        public async Task<Product> CreateProductAsync(Product product)
        {
            _logger.LogInformation($"Creating product in database");
            
            await _dataContext.Products.AddAsync(product);
            
            await _dataContext.SaveChangesAsync();
            
            return product;
        }
        
        public async Task<Product> UpdateProductAsync(Product product)
        {
            _logger.LogInformation($"Updating product with `{product.Id}` in database");
            
            _dataContext.Update(product);
            
            await _dataContext.SaveChangesAsync();

            return product;
        }
        
        public async Task<bool> DeleteProductAsync(Guid id)
        {
            _logger.LogInformation($"Deleting product from database with id `{id}`");
            
            _dataContext.Products.Remove(new Product { Id = id });

            return await _dataContext.SaveChangesAsync() > 0;;
        }

        public async Task<List<ProductOption>> GetAllProductOptionsAsync(Guid productId)
        {
            _logger.LogInformation($"Getting all options for product with id `{productId}` from database");

            var options = await _dataContext.ProductOptions
                    .Where(_ => _.ProductId == productId)
                    .ToListAsync();

            return options;
        }

        public async Task<ProductOption> GetProductOptionAsync(Guid productId, Guid optionId)
        {
            _logger.LogInformation($"Getting option with id `{optionId}` on product with id `{productId}` from database");
            
            var option = await _dataContext.ProductOptions
                .AsNoTracking()
                .SingleOrDefaultAsync(_ => _.Id == optionId && _.ProductId == productId);

            return option;
        }

        public async Task<ProductOption> CreateProductOptionAsync(ProductOption option)
        {
            _logger.LogInformation($"Creating option on product with id `{option.ProductId}` in database");
            
            await _dataContext.ProductOptions.AddAsync(option);
            
            await _dataContext.SaveChangesAsync();

            return option;
        }

        public async Task<ProductOption> UpdateProductOptionAsync(ProductOption option)
        {
            _logger.LogInformation($"Updating option with id `{option.Id}` on product with `{option.ProductId}` in database");
            
            _dataContext.ProductOptions.Update(option);
            
            await _dataContext.SaveChangesAsync();

            return option;
        }
        
        public async Task<bool> DeleteProductOptionAsync(Guid id)
        {
            _logger.LogInformation($"Deleting option with id `{id}` from database");
            
            _dataContext.ProductOptions.Remove(new ProductOption{ Id = id });

            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}