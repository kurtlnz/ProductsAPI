using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.ProductOption;

namespace XeroTechnicalTest.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        #region Product
        public async Task CreateProductAsync(Product product)
        {
            await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            return await _dataContext.Products.SingleOrDefaultAsync(_ => _.Id == id);
        }
        
        public async Task<List<Product>> GetAllProductsAsync(string name = null)
        {
            var query = _dataContext.Products
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(_ => _.Name == name);
            }
                
            return await query.ToListAsync();
        }

        public async Task UpdateProductAsync(Guid id, UpdateProduct dto)
        {
            var product = await _dataContext.Products.SingleOrDefaultAsync(_ => _.Id == id);
            if (product == null)
                return;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.DeliveryPrice;
            product.DeliveryPrice = dto.DeliveryPrice;

            await _dataContext.SaveChangesAsync();
            
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _dataContext.Products.SingleOrDefaultAsync(_ => _.Id == id);
            if (product == null)
                return;
            
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
        }
        
        #endregion
        
        #region ProductOptions
        
        public async Task CreateProductOptionAsync(Guid productId, Models.ProductOption option)
        {
            await _dataContext.ProductOptions.AddAsync(option);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Models.ProductOption> GetProductOptionAsync(Guid productId, Guid optionId)
        {
            return await _dataContext.ProductOptions.SingleOrDefaultAsync(_ => _.ProductId == productId && _.Id == optionId);
        }
        
        public async Task<List<Models.ProductOption>> GetAllProductOptionsAsync(Guid productId)
        {
            return await _dataContext.ProductOptions
                .Where(_ => _.ProductId == productId)
                .ToListAsync();
        }

        public async Task UpdateProductOptionAsync(Guid id, Guid optionId, UpdateProductOption dto)
        {
            var option = await _dataContext.ProductOptions
                .SingleOrDefaultAsync(_ => _.ProductId == id && _.Id == optionId);
            if (option == null)
                return;

            option.Name = dto.Name;
            option.Description = dto.Description;

            await _dataContext.SaveChangesAsync();
            
        }

        public async Task DeleteProductOptionAsync(Guid id, Guid optionId)
        {
            var option = await _dataContext.ProductOptions
                .SingleOrDefaultAsync(_ => _.ProductId == id && _.Id == optionId);
            if (option == null)
                return;
            
            _dataContext.ProductOptions.Remove(option);
            await _dataContext.SaveChangesAsync();
        }
        
        #endregion
    }
}