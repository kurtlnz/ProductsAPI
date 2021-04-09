using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.Product.DTO;

namespace XeroTechnicalTest.Domain.Services
{
    public interface IProductService
    {
        Task<Models.Product> GetProductAsync(Guid id);
        Task<List<Models.Product>> GetAllProductsAsync(string name);
        Task<Models.Product> CreateProductAsync(CreateProduct dto);
        Task<Models.Product> UpdateProductAsync(Guid id, UpdateProduct dto);
        Task<bool> DeleteProductAsync(Guid id);
        Task<ProductOption> GetProductOptionAsync(Guid productId, Guid optionId);
        Task<List<ProductOption>> GetAllProductOptionsAsync(Guid productId);
        Task<ProductOption> CreateProductOptionAsync(Guid productId, CreateProductOption dto);
        Task<ProductOption> UpdateProductOptionAsync(Guid productId, Guid optionId, UpdateProductOption dto);
        Task<bool> DeleteProductOptionAsync(Guid productId, Guid optionId);
    }
}