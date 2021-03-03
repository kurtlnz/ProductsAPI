using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using XeroTechnicalTest.Domain.Exceptions;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.Product.DTO;
using XeroTechnicalTest.Persistence.Repositories;

namespace XeroTechnicalTest.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;

        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }
        
        #region Products
        public async Task<Models.Product> GetProductAsync(Guid id)
        {
            _logger.LogInformation($"Getting product by id `{id}`");
            
            var product = await _productRepository.GetProductAsync(id);
            
            if (product == null)
                _logger.LogInformation($"Could not find product with id `{id}`.");
            
            return product;
        }
        
        public async Task<List<Models.Product>> GetAllProductsAsync(string name)
        {
            _logger.LogInformation($"Getting all products");

            var products = new List<Models.Product>();
            
            if (!string.IsNullOrWhiteSpace(name))
            {
                products = await _productRepository.GetAllProductsByNameAsync(name);
            }
            else
            {
                products = await _productRepository.GetAllProductsAsync();
            }
            
            _logger.LogInformation($"Retrieved `{products.Count}` products.");
            
            return products;
        }
        
        public async Task<Models.Product> CreateProductAsync(CreateProduct dto)
        {
            _logger.LogInformation($"Creating product: {JsonConvert.SerializeObject(dto)}");
            
            return await _productRepository.CreateProductAsync(dto.ToProduct());
        }

        public async Task<Models.Product> UpdateProductAsync(Guid id, UpdateProduct dto)
        {
            _logger.LogInformation($"Updating product with id `{id}`");

            var product = await _productRepository.GetProductAsync(id);
            
            if (product == null)
                throw new ProductNotFoundException($"Could not find product with id `{id}`.");

            return await _productRepository.UpdateProductAsync(product.Update(dto));
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            _logger.LogInformation($"Deleting product with id `{id}`");

            var product = await _productRepository.GetProductAsync(id);
            if (product == null)
                throw new ProductNotFoundException($"Could not find product with id `{id}`.");
            
            var success = await _productRepository.DeleteProductAsync(id);
            if (!success)
                _logger.LogWarning($"Failed to delete product with id `{id}`.");
            
            return success;
        }
        
        #endregion
        
        #region ProductOptions

        public async Task<ProductOption> GetProductOptionAsync(Guid productId, Guid optionId)
        {
            _logger.LogInformation($"Getting option with product id `{productId}` and option id `{optionId}`");

            var productOption = await _productRepository.GetProductOptionAsync(productId, optionId);
            
            if (productOption == null)
                _logger.LogInformation($"Could not find option with product id `{productId}` and option id `{optionId}`");
                
            return productOption;
        }
        
        public async Task<List<ProductOption>> GetAllProductOptionsAsync(Guid productId)
        {
            _logger.LogInformation($"Getting all options for product with id `{productId}`");
            
            var product = await _productRepository.GetProductAsync(productId);
            if (product == null)
                throw new ProductNotFoundException($"Could not find product with id `{productId}`.");

            var options = await _productRepository.GetAllProductOptionsAsync(product.Id);
            
            _logger.LogInformation($"Retrieved `{options.Count}` options.");
            
            return options;
        }
        
        public async Task<ProductOption> CreateProductOptionAsync(Guid productId, CreateProductOption dto)
        {
            _logger.LogInformation($"Creating option {JsonConvert.SerializeObject(dto)} for product with id `{productId}`");

            var product = await _productRepository.GetProductAsync(productId);
            
            if (product == null)
                throw new ProductNotFoundException($"Could not find product with id `{productId}`.");

            return await _productRepository.CreateProductOptionAsync(dto.ToProductOption(productId));
        }

        public async Task<ProductOption> UpdateProductOptionAsync(Guid productId, Guid optionId, UpdateProductOption dto)
        {
            _logger.LogInformation($"Updating product option for product with id `{productId}` and option id `{optionId}`");
            
            var option = await _productRepository.GetProductOptionAsync(productId, optionId);
            
            if (option == null)
                throw new ProductOptionNotFoundException($"Could not find option with id `{optionId}` on product with id `{productId}`.");

            return await _productRepository.UpdateProductOptionAsync(dto.ToProductOption(productId, optionId));
        }

        public async Task<bool> DeleteProductOptionAsync(Guid productId, Guid optionId)
        {
            _logger.LogInformation($"Deleting option for product with id `{productId}` and option id `{optionId}`");

            var option = await _productRepository.GetProductOptionAsync(productId, optionId);
            if (option == null)
                throw new ProductOptionNotFoundException($"Could not find option with id `{optionId}` on product with id `{productId}`.");

            var success = await _productRepository.DeleteProductOptionAsync(optionId);
            if (!success)
                _logger.LogWarning($"Failed to delete option with id `{optionId}` on productId `{productId}`.");
            
            return success;
        }
        
        #endregion
    }
}