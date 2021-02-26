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
        /// Gets product matching the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Models.Product> GetProductAsync(Guid id);
        
        /// <summary>
        /// Gets all products in db.
        /// Optional parameter to only retrieve products matching the name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<Models.Product>> GetAllProductsAsync(string name);
        
        /// <summary>
        /// Create new product.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> CreateProductAsync(CreateProduct dto);

        /// <summary>
        /// Update values on existing product in db.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateProductAsync(Guid id, UpdateProduct dto);

        /// <summary>
        /// Delete product from db.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteProductAsync(Guid id);
        
        /// <summary>
        /// Get product option matching productId and optionId.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionId"></param>
        /// <returns></returns>
        Task<ProductOption> GetProductOptionAsync(Guid productId, Guid optionId);
        
        /// <summary>
        /// Get all product options on product matching productId.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<List<ProductOption>> GetAllProductOptionsAsync(Guid productId);
        
        /// <summary>
        /// Create new product option on an existing product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> CreateProductOptionAsync(Guid productId, CreateProductOption dto);
        
        /// <summary>
        /// Update values on product option matching productId and optionId.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateProductOptionAsync(Guid productId, Guid optionId, UpdateProductOption dto);

        /// <summary>
        /// Delete product option from product matching optionId
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionId"></param>
        /// <returns></returns>
        Task<bool> DeleteProductOptionAsync(Guid productId, Guid optionId);
    }
}