using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XeroTechnicalTest.Domain.Services.DTO;

namespace XeroTechnicalTest.Domain.Models
{
    public class Product : BaseModel
    {
        [MaxLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        ///     Price (in cents)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Delivery price (in cents)
        /// </summary>
        public decimal DeliveryPrice { get; set; }

        public ICollection<ProductOption> ProductOptions { get; set; }

        public Product Update(UpdateProduct dto)
        {
            Name = dto.Name;
            Description = dto.Description;
            Price = dto.Price;
            DeliveryPrice = dto.DeliveryPrice;
            return this;
        }
    }
    
}