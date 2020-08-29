using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XeroTechnicalTest.Domain.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        [JsonIgnore] 
        public bool IsNew { get; }

        public Product()
        {
            Id = Guid.NewGuid();
            IsNew = true;
        }
    }
    
    public class ProductList
    {
        public List<Product> Products { get; set; }
    }
}