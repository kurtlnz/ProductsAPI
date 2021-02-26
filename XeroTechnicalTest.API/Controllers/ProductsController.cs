using System;
using System.Collections.Generic;
using System.Net.Http;
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
using XeroTechnicalTest.Endpoints.V1;
using XeroTechnicalTest.Endpoints.V1.Product;

namespace XeroTechnicalTest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService, IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
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
            try
            {
                _logger.LogInformation($"[GET] /products/{id}");
                
                var result = await _productService.GetProductAsync(id);

                return Ok(result);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound( new V1Response { Message = ex.Message });
            }
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            _logger.LogInformation($"[POST] /products");
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dto = _mapper.Map<CreateProduct>(request);
            
            await _productService.CreateProductAsync(dto);
            
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
        {
            try
            {
                _logger.LogInformation($"[PUT] /products/{id}");
                
                var dto = _mapper.Map<UpdateProduct>(request);
                
                await _productService.UpdateProductAsync(id, dto);

                return Ok();
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound( new V1Response { Message = ex.Message });
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

                await _productService.DeleteProductAsync(id);

                return Ok();
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound( new V1Response { Message = ex.Message });
            }
        }
        
        #endregion

        #region ProductOptions
        
        [HttpGet("{id}/options")]
        [ProducesResponseType(typeof(GetProductOptionsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOptions(Guid id)
        {
            try
            {
                _logger.LogInformation($"[PUT] /products/{id}/options");

                var result = await _productService.GetAllProductOptionsAsync(id);

                return Ok( new GetProductOptionsResponse { Items = result });
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound( new V1Response { Message = ex.Message } );
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

                return Ok(result);
            }
            catch (ProductOptionNotFoundException ex)
            {
                return NotFound( new V1Response { Message = ex.Message } );
            }
        }

        [HttpPost("{id}/options")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOption(Guid id, CreateOptionRequest request)
        {
            _logger.LogInformation($"[POST] /products/{id}/options");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dto = _mapper.Map<CreateProductOption>(request);
            
            await _productService.CreateProductOptionAsync(id, dto);
            
            return Ok();
        }

        [HttpPut("{id}/options/{optionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOption(Guid id, Guid optionId, UpdateOptionRequest request)
        {
            try
            {
                _logger.LogInformation($"[PUT] /products/{id}/options/{optionId}");
                
                var dto = _mapper.Map<UpdateProductOption>(request);
                
                await _productService.UpdateProductOptionAsync(id, optionId, dto);

                return Ok();
            }
            catch (ProductOptionNotFoundException ex)
            {
                return NotFound( new V1Response { Message = ex.Message } );
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

                await _productService.DeleteProductOptionAsync(id, optionId);

                return Ok();
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound( new V1Response { Message = ex.Message } );
            }
        }
        
        #endregion
}