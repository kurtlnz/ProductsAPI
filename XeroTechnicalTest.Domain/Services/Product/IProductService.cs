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
        /// <summary>
        ///     
        /// </summary>
        Task<Models.Product> GetProductAsync(Guid id);
        
        /// <summary>
        /// 
        /// </summary>
        Task<List<Models.Product>> GetAllProductsAsync(string name);
        
        /// <summary>
        /// 
        /// </summary>
        Task CreateProductAsync(CreateProduct dto);

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
        Task<ProductOption> GetProductOptionAsync(Guid productId, Guid optionId);
        
        /// <summary>
        /// 
        /// </summary>
        Task<List<ProductOption>> GetAllProductOptionsAsync(Guid productId);
        
        /// <summary>
        /// 
        /// </summary>
        Task CreateProductOptionAsync(Guid productId, CreateProductOption dto);
        
        /// <summary>
        /// 
        /// </summary>
        Task UpdateProductOptionAsync(Guid productId, Guid optionId, UpdateProductOption dto);

        /// <summary>
        /// 
        /// </summary>
        Task DeleteProductOptionAsync(Guid productId, Guid optionId);
    }
}