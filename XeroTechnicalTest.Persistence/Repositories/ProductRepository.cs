using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XeroTechnicalTest.Domain;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.Product.DTO;

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

            var product = new Product();
            
            try
            {
                product = await _dataContext.Products.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred! Error: `{ex.Message}`");
            }

            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync(string name)
        {
            _logger.LogInformation($"Getting all products from database");

            var products = new List<Product>();
            
            try
            {
                var query = _dataContext.Products.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(_ => _.Name == name);    
                }

                products = await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred! Error: `{ex.Message}`");
            }

            return products;
        }
        
        public async Task<bool> CreateProductAsync(Product product)
        {
            _logger.LogInformation($"Creating product in database");
            
            try
            {
                await _dataContext.Products.AddAsync(product);
                
                var result = await _dataContext.SaveChangesAsync() > 0;
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred! Error: `{ex.Message}`");
                
                return false;
            }
        }
        
        public async Task<bool> UpdateProductAsync(Product product)
        {
            _logger.LogInformation($"Updating product with `{product.Id}` in database");
            
            try
            {
                _dataContext.Update(product);

                var result = await _dataContext.SaveChangesAsync() > 0;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred! Error: `{ex.Message}`");
                
                return false;
            }
        }
        
        public async Task<bool> DeleteProductAsync(Guid id)
        {
            _logger.LogInformation($"Deleting product from database with id `{id}`");
            
            try
            {
                var product = new Product { Id = id };
                _dataContext.Products.Remove(product);

                var result = await _dataContext.SaveChangesAsync() > 0;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred! Error: `{ex.Message}`");
                
                return false;
            }
        }

        public async Task<List<ProductOption>> GetAllProductOptionsAsync(Guid productId)
        {
            _logger.LogInformation($"Getting all options for product with id `{productId}` from database");

            var options = new List<ProductOption>();
            
            try
            {
                options = await _dataContext.ProductOptions
                    .Where(_ => _.ProductId == productId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred! Error: `{ex.Message}`");
            }

            return options;
        }

        public async Task<ProductOption> GetProductOptionAsync(Guid id)
        {
            _logger.LogInformation($"Getting option with id `{id}` from database");
            
            var option = new ProductOption();
            
            try
            {
                option = await _dataContext.ProductOptions.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred! Error: `{ex.Message}`");
            }

            return option;
        }

        public async Task<bool> CreateProductOptionAsync(ProductOption option)
        {
            _logger.LogInformation($"Creating option on product with id `{option.ProductId}` in database");
            
            try
            {
                await _dataContext.ProductOptions.AddAsync(option);

                var result = await _dataContext.SaveChangesAsync() > 0;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred! Error: `{ex.Message}`");
                
                return false;
            }
            
        }

        public async Task<bool> UpdateProductOptionAsync(ProductOption option)
        {
            _logger.LogInformation($"Updating option with id `{option.Id}` in database");
            
            try
            {
                _dataContext.ProductOptions.Update(option);

                var result = await _dataContext.SaveChangesAsync() > 0;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred! Error: `{ex.Message}`");

                return false;
            }
        }
        
        public async Task<bool> DeleteProductOptionAsync(Guid id)
        {
            _logger.LogInformation($"Deleting option with id `{id}` from database");
            
            try
            {
                var option = new ProductOption{ Id = id };
                _dataContext.ProductOptions.Remove(option);
                
                var result = await _dataContext.SaveChangesAsync() > 0;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred! Error: `{ex.Message}`");

                return false;
            }
            
        }
    }
}