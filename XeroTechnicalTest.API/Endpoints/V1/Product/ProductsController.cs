using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XeroTechnicalTest.Domain.Exceptions;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.Product.DTO;

namespace XeroTechnicalTest.Endpoints.V1.Product
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        #region Product
        
        // GET /products?name={name}
        [HttpGet]
        [ProducesResponseType(typeof(Products), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts([FromQuery] string name)
        {
            var result = await _productService.GetAllProductsAsync(name);
            
            return Ok(result);
        }

        // GET: /products/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {
                var result = await _productService.GetProductAsync(id);
                
                return Ok(result);
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
            
        }

        // POST: /products
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dto = _mapper.Map<CreateProduct>(request);
            
            await _productService.CreateProductAsync(dto);
            
            return Ok();
        }

        // PUT: /products/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request)
        {
            try
            {
                var dto = _mapper.Map<UpdateProduct>(request);
                
                await _productService.UpdateProductAsync(id, dto);

                return Ok();
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: /products/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);

                return Ok();
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }
        
        #endregion

        // GET /products/{id}/options
        [HttpGet("{id}/options")]
        [ProducesResponseType(typeof(ProductOptions), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOptions(Guid id)
        {
            try
            {
                var result = await _productService.GetAllProductOptionsAsync(id);

                return Ok(result);
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: /products/{id}/options/{optionId}
        [HttpGet("{id}/options/{optionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOption(Guid id, Guid optionId)
        {
            try
            {
                var result = await _productService.GetProductOptionAsync(id, optionId);

                return Ok(result);
            }
            catch (ProductOptionNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: /products/{id}/options
        [HttpPost("{id}/options")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOption(Guid id, CreateOptionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dto = _mapper.Map<CreateProductOption>(request);
            
            await _productService.CreateProductOptionAsync(id, dto);
            
            return Ok();
        }

        // PUT: /products/{id}/options/{optionId}
        [HttpPut("{id}/options/{optionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOption(Guid id, Guid optionId, UpdateOptionRequest request)
        {
            try
            {
                var dto = _mapper.Map<UpdateProductOption>(request);
                
                await _productService.UpdateProductOptionAsync(id, optionId, dto);

                return Ok();
            }
            catch (ProductOptionNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: /products/{id}/options/{optionId}
        [HttpDelete("{id}/options/{optionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOption(Guid id, Guid optionId)
        {
            try
            {
                await _productService.DeleteProductOptionAsync(id, optionId);

                return Ok();
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }
    }
}