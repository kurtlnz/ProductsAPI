using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.Product.DTO;

namespace XeroTechnicalTest.Persistence.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(Guid id);
        Task<List<Product>> GetAllProductsAsync(string name);
        Task<bool> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid id);
        Task<List<ProductOption>> GetAllProductOptionsAsync(Guid id);
        Task<ProductOption> GetProductOptionAsync(Guid optionId);
        Task<bool> CreateProductOptionAsync(ProductOption option);
        Task<bool> UpdateProductOptionAsync(ProductOption option);
        Task<bool> DeleteProductOptionAsync(Guid id);
    }
}