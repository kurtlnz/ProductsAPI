using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XeroTechnicalTest.Domain.Exceptions;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.Product.DTO;

namespace XeroTechnicalTest.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        #region Products
        public async Task<Models.Product> GetProductAsync(Guid id)
        {
            var product = await _dataContext.Products.SingleOrDefaultAsync(_ => _.Id == id);
            
            if (product == null)
                throw new ProductNotFoundException($"Could not find product with id '{id}'.");
            
            return product;
        }
        
        public async Task<List<Models.Product>> GetAllProductsAsync(string name)
        {
            var query = _dataContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(_ => _.Name == name);    
            }
            
            return await query.ToListAsync();
        }
        
        public async Task CreateProductAsync(CreateProduct dto)
        {
            var product = new Models.Product()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DeliveryPrice = dto.DeliveryPrice
            };
            await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Guid id, UpdateProduct dto)
        {
            var product = await _dataContext.Products.SingleOrDefaultAsync(_ => _.Id == id);
            
            if (product == null)
                throw new ProductNotFoundException($"Could not find product with id '{id}'.");

            HandleProductUpdate(product, dto);

            await _dataContext.SaveChangesAsync();
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

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _dataContext.Products.SingleOrDefaultAsync(_ => _.Id == id);
            
            if (product == null)
                throw new ProductNotFoundException($"Could not find product with id '{id}'.");
            
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
        }
        
        #endregion
        
        #region ProductOptions

        public async Task<ProductOption> GetProductOptionAsync(Guid productId, Guid optionId)
        {
            var productOption = await _dataContext.ProductOptions
                .SingleOrDefaultAsync(_ => _.ProductId == productId && _.Id == optionId);
            
            if (productOption == null)
                throw new ProductOptionNotFoundException($"Could not find option with productId '{productId}' and optionId '{optionId}'.");
                
            return productOption;
        }
        
        public async Task<List<ProductOption>> GetAllProductOptionsAsync(Guid productId)
        {
            var product = await _dataContext.Products
                .Include(_ => _.Options)
                .SingleOrDefaultAsync(_ => _.Id == productId);
            
            if (product == null)
                throw new ProductNotFoundException($"Could not find product with id '{productId}'.");
            
            return product.Options.ToList();
        }
        
        public async Task CreateProductOptionAsync(Guid productId, CreateProductOption dto)
        {
            var option = new ProductOption()
            {
                ProductId = productId,
                Name = dto.Name,
                Description = dto.Description
            };
            await _dataContext.ProductOptions.AddAsync(option);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateProductOptionAsync(Guid productId, Guid optionId, UpdateProductOption dto)
        {
            var option = await _dataContext.ProductOptions
                .SingleOrDefaultAsync(_ => _.ProductId == productId && _.Id == optionId);
            
            if (option == null)
                throw new ProductOptionNotFoundException($"Could not find option with productId '{productId}' and optionId '{optionId}'.");

            HandleProductOptionUpdate(option, dto);

            await _dataContext.SaveChangesAsync();
        }

        private void HandleProductOptionUpdate(ProductOption option, UpdateProductOption dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name)) 
                option.Name = dto.Name;
            if (!string.IsNullOrWhiteSpace(dto.Description)) 
                option.Description = dto.Description;
        }

        public async Task DeleteProductOptionAsync(Guid productId, Guid optionId)
        {
            var option = await _dataContext.ProductOptions
                .SingleOrDefaultAsync(_ => _.ProductId == productId && _.Id == optionId);
            
            if (option == null)
                throw new ProductOptionNotFoundException($"Could not find option with productId '{productId}' and optionId '{optionId}'");
            
            _dataContext.ProductOptions.Remove(option);
            await _dataContext.SaveChangesAsync();
        }
        
        #endregion
    }
}