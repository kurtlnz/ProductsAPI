using System.Collections.Generic;

namespace XeroTechnicalTest.Domain.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public ICollection<ProductOption> Options { get; set; }
        
    }
    
}