using System;

namespace XeroTechnicalTest.Domain.Services.Product.DTO
{
    public class CreateProductOption
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        public Models.ProductOption ToProductOption(Guid productId)
        {
            return new Models.ProductOption()
            {
                ProductId = productId,
                Name = Name,
                Description = Description
            }; 
        }
    }
}