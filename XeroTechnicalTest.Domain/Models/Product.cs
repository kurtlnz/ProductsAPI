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
        public ICollection<ProductOption> Options { get; set; }
        
    }
    
}