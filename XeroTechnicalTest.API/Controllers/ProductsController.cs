using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XeroTechnicalTest.Domain.Exceptions;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.Product.DTO;
using XeroTechnicalTest.Filters;
using XeroTechnicalTest.Models;

namespace XeroTechnicalTest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ApiController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        
        #region Product
        
        [HttpGet]
        [ProducesResponseType(typeof(ListResponse<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts([FromQuery] string name)
        {
            _logger.LogInformation($"[GET] /products");

            var results = await _productService.GetAllProductsAsync(name);;
            
            return Ok(new ListResponse<Product>(results));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            _logger.LogInformation($"[GET] /products/{id}");
            
            var result = await _productService.GetProductAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);   
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProduct(CreateProduct request)
        {
            _logger.LogInformation($"[POST] /products");
            
            var result = await _productService.CreateProductAsync(request);
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProduct request)
        {
            try
            {
                _logger.LogInformation($"[PUT] /products/{id}");

                var result = await _productService.UpdateProductAsync(id, request);

                return Ok(result);
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                _logger.LogInformation($"[DELETE] /products/{id}");
                
                await _productService.DeleteProductAsync(id);

                return NoContent();
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }
        
        #endregion

        #region ProductOptions
        
        [HttpGet("{id}/options")]
        [ProducesResponseType(typeof(ListResponse<ProductOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOptions(Guid id)
        {
            try
            {
                _logger.LogInformation($"[PUT] /products/{id}/options");

                var results = await _productService.GetAllProductOptionsAsync(id);
                
                return Ok(new ListResponse<ProductOption>(results));
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/options/{optionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOption(Guid id, Guid optionId)
        {
            try
            {
                _logger.LogInformation($"[PUT] /products/{id}/options/{optionId}");

                var result = await _productService.GetProductOptionAsync(id, optionId);

                if (result == null)
                {
                    return NotFound();
                }
                
                return Ok(result);
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/options")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOption(Guid id, CreateProductOption request)
        {
            try
            {
                _logger.LogInformation($"[POST] /products/{id}/options");

                var result = await _productService.CreateProductOptionAsync(id, request);
                
                return Ok(result);
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}/options/{optionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOption(Guid id, Guid optionId, UpdateProductOption request)
        {
            try
            {
                _logger.LogInformation($"[PUT] /products/{id}/options/{optionId}");
                
                var success = await _productService.UpdateProductOptionAsync(id, optionId, request);
                
                return Ok(success);
            }
            catch (ProductOptionNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/options/{optionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOption(Guid id, Guid optionId)
        {
            try
            {
                _logger.LogInformation($"[DELETE] /products/{id}/options/{optionId}");

                await _productService.DeleteProductOptionAsync(id, optionId);

                return NoContent();
            }
            catch (ProductOptionNotFoundException)
            {
                return NotFound();
            }
        }
        
        #endregion
    }
}