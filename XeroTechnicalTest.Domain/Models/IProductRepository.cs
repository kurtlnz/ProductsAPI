using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeroTechnicalTest.Domain.Models;

namespace XeroTechnicalTest.Persistence.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(Guid id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetAllProductsByNameAsync(string name);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid id);
        Task<List<ProductOption>> GetAllProductOptionsAsync(Guid id);
        Task<ProductOption> GetProductOptionAsync(Guid productId, Guid optionId);
        Task<ProductOption> CreateProductOptionAsync(ProductOption option);
        Task<ProductOption> UpdateProductOptionAsync(ProductOption option);
        Task<bool> DeleteProductOptionAsync(Guid id);
    }
}