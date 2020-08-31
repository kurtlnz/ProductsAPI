using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XeroTechnicalTest.Domain.Models
{
    public class ProductOption : BaseModel
    {
        // TODO: Add foreign key constraint
        [JsonIgnore]
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
    
    public class ProductOptions
    {
        public List<Product> Items { get; set; }
    }
}