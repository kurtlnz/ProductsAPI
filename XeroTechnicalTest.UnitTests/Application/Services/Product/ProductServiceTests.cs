using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using XeroTechnicalTest.Domain.Exceptions;
using XeroTechnicalTest.Domain.Models;
using XeroTechnicalTest.Domain.Services;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.Product.DTO;
using XeroTechnicalTest.Persistence.Repositories;

namespace XeroTechnicalTest.UnitTests.Application.Services.Product
{
    public class ProductServiceTests
    {
        private ProductService _sut;
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<ILogger<ProductService>> _mockLogger;
        
        [SetUp]
        public void Setup()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _sut = new ProductService(_mockLogger.Object, _mockProductRepository.Object);
        }
        
        [Test]
        public async Task GetProductAsync_GivenProductId_ReturnsProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Domain.Models.Product
            {
                Id = productId,
                Name = "Product Name",
                Description = "Product Description",
                Price = 100,
                DeliveryPrice = 2 
            };

            _mockProductRepository
                .Setup(m => m.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(product);

            // Act
            var result = await _sut.GetProductAsync(productId);

            // Assert
            Assert.AreEqual(product.Name, result.Name);
            Assert.AreEqual(product.Description, result.Description);
            Assert.AreEqual(product.Price, result.Price);
            Assert.AreEqual(product.DeliveryPrice, result.DeliveryPrice);
            
        }
        
        [Test]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Domain.Models.Product>
            {
                new Domain.Models.Product()
                {

                    Id = Guid.NewGuid(),
                    Name = "Product Name 1",
                    Description = "Product Description 1",
                    Price = 100,
                    DeliveryPrice = 2
                },
                new Domain.Models.Product()
                {

                    Id = Guid.NewGuid(),
                    Name = "Product Name 2",
                    Description = "Product Description 2",
                    Price = 200,
                    DeliveryPrice = 3
                }
            };

            _mockProductRepository
                .Setup(m => m.GetAllProductsAsync())
                .ReturnsAsync(products);

            // Act
            var results = await _sut.GetAllProductsAsync(null);

            // Assert
            Assert.That(results.Count, Is.EqualTo(2));
            Assert.AreEqual(products[0].Name, results[0].Name);
            Assert.AreEqual(products[0].Description, results[0].Description);
            Assert.AreEqual(products[0].Price, results[0].Price);
            Assert.AreEqual(products[0].DeliveryPrice, results[0].DeliveryPrice);
            Assert.AreEqual(products[1].Name, results[1].Name);
            Assert.AreEqual(products[1].Description, results[1].Description);
            Assert.AreEqual(products[1].Price, results[1].Price);
            Assert.AreEqual(products[1].DeliveryPrice, results[1].DeliveryPrice);
        }
        
        [Test]
        public async Task GetAllProductsAsync_WithNameFilter_ReturnsProductsFilteredByName()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var products = new List<Domain.Models.Product>
            {
                new Domain.Models.Product()
                {

                    Id = productId,
                    Name = "Product Name 1",
                    Description = "Product Description 1",
                    Price = 100,
                    DeliveryPrice = 2
                }
            };

            _mockProductRepository
                .Setup(m => m.GetAllProductsByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(products);

            // Act
            var results = await _sut.GetAllProductsAsync("Product Name 1");

            // Assert
            Assert.AreEqual(results[0].Id, productId);
            Assert.AreEqual(results[0].Name, "Product Name 1");
            Assert.AreEqual(results[0].Description, "Product Description 1");
            Assert.AreEqual(results[0].Price, 100);
            Assert.AreEqual(results[0].DeliveryPrice, 2);
        }
        
        [Test]
        public async Task CreateProductAsync_IsSuccessful_ReturnsTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var dto = new CreateProduct()
            {
                Name = "Product Name",
                Description = "Product Description",
                Price = 100,
                DeliveryPrice = 2 
            };
            var product = new Domain.Models.Product()
            {
                Id = productId,
                Name = "Product Name",
                Description = "Product Description",
                Price = 100,
                DeliveryPrice = 2 
            };

            _mockProductRepository
                .Setup(m => m.CreateProductAsync(It.IsAny<Domain.Models.Product>()))
                .ReturnsAsync(product);
            
            // Act
            var result = await _sut.CreateProductAsync(dto);

            // Assert
            Assert.AreEqual(result.Id, productId);
            Assert.AreEqual(result.Name, "Product Name");
            Assert.AreEqual(result.Description, "Product Description");
            Assert.AreEqual(result.Price, 100);
            Assert.AreEqual(result.DeliveryPrice, 2);
        }
        
        [Test]
        public async Task UpdateProductAsync_IsSuccessful_ReturnsTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var dto = new UpdateProduct()
            {
                Name = "Product Name 1",
                Description = "Product Description 1",
                Price = 200,
                DeliveryPrice = 3
            };
            var product = new Domain.Models.Product()
            {
                Id = productId,
                Name = "Product Name",
                Description = "Product Description",
                Price = 100,
                DeliveryPrice = 2 
            };
            var updatedProduct = new Domain.Models.Product()
            {
                Id = productId,
                Name = "Product Name 1",
                Description = "Product Description 1",
                Price = 200,
                DeliveryPrice = 3
            };

            _mockProductRepository
                .Setup(m => m.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(product);
            
            _mockProductRepository
                .Setup(m => m.UpdateProductAsync(It.IsAny<Domain.Models.Product>()))
                .ReturnsAsync(updatedProduct);
            
            // Act
            var result = await _sut.UpdateProductAsync(productId, dto);

            // Assert
            Assert.AreEqual(result.Id, productId);
            Assert.AreEqual(result.Name, "Product Name 1");
            Assert.AreEqual(result.Description, "Product Description 1");
            Assert.AreEqual(result.Price, 200);
            Assert.AreEqual(result.DeliveryPrice, 3);
        }
        
        [Test]
        public async Task UpdateProductAsync_ProductNotFound_ThrowsProductNotFoundException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var dto = new UpdateProduct()
            {
                Name = "Product Name",
                Description = "Product Description",
                Price = 100,
                DeliveryPrice = 2 
            };

            _mockProductRepository
                .Setup(m => m.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as Domain.Models.Product);
            
            // Assert
            Assert.That(() => _sut.UpdateProductAsync(productId, dto), Throws.Exception.TypeOf<ProductNotFoundException>());

        }
        
        [Test]
        public async Task DeleteProductAsync_IsSuccessful_ReturnsTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Domain.Models.Product()
            {
                Id = productId,
                Name = "Product Name",
                Description = "Product Description",
                Price = 100,
                DeliveryPrice = 2 
            };

            _mockProductRepository
                .Setup(m => m.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(product);
            
            _mockProductRepository
                .Setup(m => m.DeleteProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            
            // Act
            var result = await _sut.DeleteProductAsync(productId);

            // Assert
            Assert.That(result, Is.True);

        }
        
        [Test]
        public async Task DeleteProductAsync_ProductNotFound_ThrowsProductNotFoundException()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _mockProductRepository
                .Setup(m => m.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as Domain.Models.Product);
            
            // Assert
            Assert.That(() => _sut.DeleteProductAsync(productId), Throws.Exception.TypeOf<ProductNotFoundException>());

        }
        
        [Test]
        public async Task GetProductOptionAsync_ReturnsProductOption()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var optionId = Guid.NewGuid();
            var option = new ProductOption()
            {
                Name = "Product Option Name 1",
                Description = "Product Option Description 1"
            };

            _mockProductRepository
                .Setup(m => m.GetProductOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(option);

            // Act
            var result = await _sut.GetProductOptionAsync(productId, optionId);

            // Assert
            Assert.AreEqual(option.Id, result.Id);
            Assert.AreEqual(option.Name, result.Name);
            Assert.AreEqual(option.Description, result.Description);
        }
        
        [Test]
        public async Task GetAllProductOptionsAsync_ReturnsAllOptionsOnProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var options = new List<ProductOption>
            {
                new ProductOption
                {
                    ProductId = productId,
                    Name = "Product Option Name 1",
                    Description = "Product Option Description 1"
                },
                new ProductOption
                {
                    ProductId = productId,
                    Name = "Product Option Name 2",
                    Description = "Product Option Description 2"
                }
            };
            var product = new Domain.Models.Product()
            {
                Id = productId,
                Name = "Product Name",
                Description = "Product Description",
                Price = 100,
                DeliveryPrice = 2 
            };

            _mockProductRepository
                .Setup(m => m.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(product);
            
            _mockProductRepository
                .Setup(m => m.GetAllProductOptionsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(options);

            // Act
            var results = await _sut.GetAllProductOptionsAsync(productId);

            // Assert
            Assert.That(results.Count, Is.EqualTo(2));
            Assert.AreEqual(options[0].Name, results[0].Name);
            Assert.AreEqual(options[0].Description, results[0].Description);
            Assert.AreEqual(options[1].Name, results[1].Name);
            Assert.AreEqual(options[1].Description, results[1].Description);
        }
        
        [Test]
        public async Task GetAllProductOptionsAsync_ProductNotFound_ThrowsProductNotFoundException()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _mockProductRepository
                .Setup(m => m.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as Domain.Models.Product);

            // Assert
            Assert.That(() => _sut.GetAllProductOptionsAsync(productId), Throws.Exception.TypeOf<ProductNotFoundException>());
        }
        
        [Test]
        public async Task CreateProductOptionAsync_IsSuccessful_ReturnsTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var dto = new CreateProductOption()
            {
                Name = "Product Option Name",
                Description = "Product Option Description"
            };
            var product = new Domain.Models.Product
            {
                Name = "Product Name 1",
                Description = "Product Description 1",
                Price = 100,
                DeliveryPrice = 2
            };
            var option = new ProductOption
            {
                Name = "Product Option Name 1",
                Description = "Product Option Description 1",
            };

            _mockProductRepository
                .Setup(m => m.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(product);

            _mockProductRepository
                .Setup(m => m.CreateProductOptionAsync(It.IsAny<ProductOption>()))
                .ReturnsAsync(option);
            
            // Act
            var result = await _sut.CreateProductOptionAsync(productId, dto);

            // Assert
            Assert.AreEqual(option.Name, result.Name);
            Assert.AreEqual(option.Description, result.Description);
        }
        
        [Test]
        public async Task CreateProductOptionAsync_ProductNotFound_ThrowsProductNotFoundException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var dto = new CreateProductOption()
            {
                Name = "Product Option Name 1",
                Description = "Product Option Description 1",
            };

            _mockProductRepository
                .Setup(m => m.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as Domain.Models.Product);

            // Assert
            Assert.That(() => _sut.CreateProductOptionAsync(productId, dto), Throws.Exception.TypeOf<ProductNotFoundException>());
        }
        
        [Test]
        public async Task UpdateProductOptionAsync_IsSuccessful_ReturnsTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var optionId = Guid.NewGuid();
            var dto = new UpdateProductOption()
            {
                Name = "Product Option Name 2",
                Description = "Product Option Description 2",
            };
            var option = new ProductOption
            {
                Name = "Product Option Name 1",
                Description = "Product Option Description 1"
            };
            var updatedOption = new ProductOption
            {
                Name = "Product Option Name 2",
                Description = "Product Option Description 2"
            };

            _mockProductRepository
                .Setup(m => m.GetProductOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(option);
            
            _mockProductRepository
                .Setup(m => m.UpdateProductOptionAsync(It.IsAny<ProductOption>()))
                .ReturnsAsync(updatedOption);
            
            // Act
            var result = await _sut.UpdateProductOptionAsync(productId, optionId, dto);

            // Assert
            Assert.AreEqual(updatedOption.Name, result.Name);
            Assert.AreEqual(updatedOption.Description, result.Description);
        }
        
        [Test]
        public async Task UpdateProductOptionAsync_OptionNotFound_ThrowsProductOptionNotFoundException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var optionId = Guid.NewGuid();
            var dto = new UpdateProductOption()
            {
                Name = "Product Option Name 2",
                Description = "Product Option Description 2",
            };

            _mockProductRepository
                .Setup(m => m.GetProductOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(null as ProductOption);

            // Assert
            Assert.That(() => _sut.UpdateProductOptionAsync(productId, optionId, dto), Throws.Exception.TypeOf<ProductOptionNotFoundException>());
        }
        
        [Test]
        public async Task DeleteProductOptionAsync_IsSuccessful_ReturnsTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var optionId = Guid.NewGuid();
            var dto = new UpdateProductOption()
            {
                Name = "Product Option Name 2",
                Description = "Product Option Description 2",
            };
            var option = new ProductOption
            {
                Name = "Product Option Name 1",
                Description = "Product Option Description 1"
            };

            _mockProductRepository
                .Setup(m => m.GetProductOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(option);
            
            _mockProductRepository
                .Setup(m => m.DeleteProductOptionAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            
            // Act
            var result = await _sut.DeleteProductOptionAsync(productId, optionId);

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public async Task DeleteProductOptionAsync_OptionNotFound_ThrowsProductOptionNotFoundException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var optionId = Guid.NewGuid();

            _mockProductRepository
                .Setup(m => m.GetProductOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(null as ProductOption);

            // Assert
            Assert.That(() => _sut.DeleteProductOptionAsync(productId, optionId), Throws.Exception.TypeOf<ProductOptionNotFoundException>());
        }
    }
}