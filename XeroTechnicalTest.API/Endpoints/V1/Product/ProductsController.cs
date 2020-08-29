using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.ProductOption;

namespace XeroTechnicalTest.Endpoints.V1.Product
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService , IMapper mapper)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        // GET: /products
        [HttpGet]
        [ProducesResponseType(typeof(ProductList), 200)]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }
        
        // GET /products?name={name}
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(ProductList), 200)]
        public async Task<IActionResult> GetProducts(string name)
        {
            return Ok(await _productService.GetAllProductsAsync(name));
        }

        // GET: /products/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
                return NotFound();
            
            return Ok(product);
        }

        // POST: /products
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateProduct(Domain.Models.Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _productService.CreateProductAsync(product);
            
            return Ok();
        }

        // PUT: /products/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update(Guid id, UpdateRequest request)
        {
            var dto = _mapper.Map<UpdateProduct>(request);
            await _productService.UpdateProductAsync(id, dto);

            return Ok();
        }

        // DELETE: /products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }

        // GET /products/{id}/options
        [HttpGet("{id}/options")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetOptions(Guid id)
        {
            return Ok(await _productService.GetAllProductOptionsAsync(id));
        }

        // GET: /products/{id}/options/{optionId}
        [HttpGet("{id}/options/{optionId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetOption(Guid id, Guid optionId)
        {
            return Ok(await _productService.GetProductOptionAsync(id, optionId));
        }

        // POST: /products/{id}/options
        [HttpPost("{id}/options")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateOption(Guid productId, ProductOption option)
        {
            await _productService.CreateProductOptionAsync(productId, option);
            
            return Ok();
        }

        // PUT: /products/{id}/options/{optionId}
        [HttpPut("{id}/options/{optionId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateOption(Guid id, Guid optionId, ProductOption option)
        {
            var dto = _mapper.Map<UpdateProductOption>(option);
            await _productService.UpdateProductOptionAsync(id, optionId, dto);

            return Ok();
        }

        // DELETE: /products/{id}/options/{optionId}
        [HttpDelete("{id}/options/{optionId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteOption(Guid id, Guid optionId)
        {
            await _productService.DeleteProductOptionAsync(id, optionId);

            return Ok();
        }
    }
}