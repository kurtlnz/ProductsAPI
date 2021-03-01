using System;

namespace XeroTechnicalTest.Domain.Services.Product.DTO
{
    public class UpdateProductOption
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        public Models.ProductOption ToProductOption(Guid productId, Guid optionId)
        {
            return new Models.ProductOption()
            {
                Id = optionId,
                ProductId = productId,
                Name = Name,
                Description = Description
            }; 
        }
    }
}