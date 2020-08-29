using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XeroTechnicalTest.Domain.Models
{
    public class ProductOption : BaseModel
    {
        // TODO: Add foreign key constraint
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore] 
        public bool IsNew { get; }

        public ProductOption()
        {
            Id = Guid.NewGuid();
            IsNew = true;
        }
    }
    
    public class ProductOptionsList
    {
        public List<Product> Products { get; set; }
    }
}