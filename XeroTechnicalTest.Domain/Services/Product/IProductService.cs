using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.ProductOption;

namespace XeroTechnicalTest.Domain.Services
{
    public interface IProductService
    {
        /// <summary>
        /// 
        /// </summary>
        Task CreateProductAsync(CreateProduct dto);
        
        /// <summary>
        /// 
        /// </summary>
        Task<Product> GetProductAsync(Guid id);
        
        /// <summary>
        /// 
        /// </summary>
        Task<List<Product>> GetAllProductsAsync(string name = null);
        
        /// <summary>
        /// 
        /// </summary>
        Task UpdateProductAsync(Guid id, UpdateProduct dto);

        /// <summary>
        /// 
        /// </summary>
        Task DeleteProductAsync(Guid id);
        
        /// <summary>
        /// 
        /// </summary>
        Task CreateProductOptionAsync(Guid productId, Models.ProductOption option);
        
        /// <summary>
        /// 
        /// </summary>
        Task<Models.ProductOption> GetProductOptionAsync(Guid productId, Guid optionId);
        
        /// <summary>
        /// 
        /// </summary>
        Task<List<Models.ProductOption>> GetAllProductOptionsAsync(Guid productId);
        
        /// <summary>
        /// 
        /// </summary>
        Task UpdateProductOptionAsync(Guid id, Guid optionId, UpdateProductOption dto);

        /// <summary>
        /// 
        /// </summary>
        Task DeleteProductOptionAsync(Guid id, Guid optionId);
    }
}