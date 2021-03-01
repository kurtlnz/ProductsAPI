using System;

namespace XeroTechnicalTest.Domain.Services.DTO
{
    public class UpdateProduct
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
        
        public Models.Product ToProduct(Guid id)
        {
            return new Models.Product()
            {
                Id = id,
                Name = Name,
                Description = Description,
                Price = Price,
                DeliveryPrice = DeliveryPrice
            }; 
        }
    }
}