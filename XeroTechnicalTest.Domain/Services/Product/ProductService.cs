using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XeroTechnicalTest.Domain.Exceptions;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.Product.DTO;

namespace XeroTechnicalTest.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ProductService> _logger;

        public ProductService(DataContext dataContext, ILogger<ProductService> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        
        #region Products
        public async Task<Models.Product> GetProductAsync(Guid id)
        {
            _logger.LogInformation($"Getting product by id `{id}`");
            
            var product = await _dataContext.Products.SingleOrDefaultAsync(_ => _.Id == id);
            
            if (product == null)
                _logger.LogInformation($"Could not find product with id `{id}`.");
            
            return product;
        }
        
        public async Task<List<Models.Product>> GetAllProductsAsync(string name)
        {
            _logger.LogInformation($"Getting all products");
            
            var query = _dataContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(_ => _.Name == name);    
            }
            
            return await query.ToListAsync();
        }
        
        public async Task<bool> CreateProductAsync(CreateProduct dto)
        {
            // TODO: serialize json object into string
            _logger.LogInformation($"Creating product: ");
            
            var product = new Models.Product()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DeliveryPrice = dto.DeliveryPrice
            };
            await _dataContext.Products.AddAsync(product);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductAsync(Guid id, UpdateProduct dto)
        {
            _logger.LogInformation($"Updating product with id `{id}`");
            
            var product = await _dataContext.Products.SingleOrDefaultAsync(_ => _.Id == id);
            
            if (product == null)
                _logger.LogInformation($"Could not find product with id `{id}`.");

            HandleProductUpdate(product, dto);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        private void HandleProductUpdate(Models.Product product, UpdateProduct dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name)) 
                product.Name = dto.Name;
            if (!string.IsNullOrWhiteSpace(dto.Description)) 
                product.Description = dto.Description;
            if (dto.Price != null) 
                product.Price = dto.Price.Value;
            if (dto.DeliveryPrice != null) 
                product.DeliveryPrice = dto.DeliveryPrice.Value;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            _logger.LogInformation($"Deleting product with id `{id}`");
            
            var product = await _dataContext.Products.SingleOrDefaultAsync(_ => _.Id == id);
            
            if (product == null)
                _logger.LogInformation($"Could not find product with id `{id}`.");
            
            _dataContext.Products.Remove(product);
            return await _dataContext.SaveChangesAsync() > 0;
        }
        
        #endregion
        
        #region ProductOptions

        public async Task<ProductOption> GetProductOptionAsync(Guid productId, Guid optionId)
        {
            _logger.LogInformation($"Getting option with product id `{productId}` and option id `{optionId}`");

            var productOption = await _dataContext.ProductOptions
                .SingleOrDefaultAsync(_ => _.ProductId == productId && _.Id == optionId);
            
            if (productOption == null)
                _logger.LogInformation($"Could not find option with product id `{productId}` and option id `{optionId}`");
                
            return productOption;
        }
        
        public async Task<List<ProductOption>> GetAllProductOptionsAsync(Guid productId)
        {
            _logger.LogInformation($"Getting all options for product with id `{productId}`");

            var product = await _dataContext.Products
                .Include(_ => _.Options)
                .SingleOrDefaultAsync(_ => _.Id == productId);
            
            if (product == null)
                _logger.LogInformation($"Could not find product with id `{productId}`.");
            
            return product.Options.ToList();
        }
        
        public async Task<bool> CreateProductOptionAsync(Guid productId, CreateProductOption dto)
        {
            // TODO: serialize product option
            _logger.LogInformation($"Creating option for product with id `{productId}`");

            var option = new ProductOption()
            {
                ProductId = productId,
                Name = dto.Name,
                Description = dto.Description
            };
            await _dataContext.ProductOptions.AddAsync(option);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductOptionAsync(Guid productId, Guid optionId, UpdateProductOption dto)
        {
            _logger.LogInformation($"Updating product option for product with id `{productId}` and option id `{optionId}`");

            var option = await _dataContext.ProductOptions
                .SingleOrDefaultAsync(_ => _.ProductId == productId && _.Id == optionId);
            
            if (option == null)
                _logger.LogInformation($"Could not find option with productId `{productId}` and optionId `{optionId}`.");

            HandleProductOptionUpdate(option, dto);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        private void HandleProductOptionUpdate(ProductOption option, UpdateProductOption dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name)) 
                option.Name = dto.Name;
            if (!string.IsNullOrWhiteSpace(dto.Description)) 
                option.Description = dto.Description;
        }

        public async Task<bool> DeleteProductOptionAsync(Guid productId, Guid optionId)
        {
            _logger.LogInformation($"Deleting option for product with id `{productId}` and option id `{optionId}`");

            var option = await _dataContext.ProductOptions
                .SingleOrDefaultAsync(_ => _.ProductId == productId && _.Id == optionId);

            if (option == null)
            {
                _logger.LogInformation($"Could not find option with productId '{productId}' and optionId '{optionId}'");
                return false;
            }
            
            _dataContext.ProductOptions.Remove(option);
            return await _dataContext.SaveChangesAsync() > 0;
        }
        
        #endregion
    }
}