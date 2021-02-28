using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XeroTechnicalTest.Domain.Exceptions;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.Product.DTO;

namespace XeroTechnicalTest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
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
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts([FromQuery] string name)
        {
            _logger.LogInformation($"[GET] /products");
        
            var result = await _productService.GetAllProductsAsync(name);
            return Ok(result);
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct(CreateProduct request)
        {
            _logger.LogInformation($"[POST] /products");
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var success = await _productService.CreateProductAsync(request);
            
            return Ok(success);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProduct request)
        {
            try
            {
                _logger.LogInformation($"[PUT] /products/{id}");
                
                var success = await _productService.UpdateProductAsync(id, request);

                return Ok(success);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                _logger.LogInformation($"[DELETE] /products/{id}");

                var success = await _productService.DeleteProductAsync(id);

                return Ok(success);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound();
            }
        }
        
        #endregion

        #region ProductOptions
        
        [HttpGet("{id}/options")]
        [ProducesResponseType(typeof(IEnumerable<ProductOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOptions(Guid id)
        {
            try
            {
                _logger.LogInformation($"[PUT] /products/{id}/options");

                var result = await _productService.GetAllProductOptionsAsync(id);

                return Ok(result);
            }
            catch (ProductNotFoundException ex)
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
            catch (ProductOptionNotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/options")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOption(Guid id, CreateProductOption request)
        {
            _logger.LogInformation($"[POST] /products/{id}/options");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            try
            {
                var success = await _productService.CreateProductOptionAsync(id, request);
                
                return Ok(success);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}/options/{optionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOption(Guid id, Guid optionId, UpdateProductOption request)
        {
            try
            {
                _logger.LogInformation($"[PUT] /products/{id}/options/{optionId}");
                
                var success = await _productService.UpdateProductOptionAsync(id, optionId, request);
                
                return Ok(success);
            }
            catch (ProductOptionNotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/options/{optionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOption(Guid id, Guid optionId)
        {
            try
            {
                _logger.LogInformation($"[DELETE] /products/{id}/options/{optionId}");

                var success = await _productService.DeleteProductOptionAsync(id, optionId);

                return Ok(success);
            }
            catch (ProductOptionNotFoundException ex)
            {
                return NotFound();
            }
        }
        
        #endregion
    }
}