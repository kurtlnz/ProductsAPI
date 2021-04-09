using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XeroTechnicalTest.Domain.Models
{
    public class ProductOption : BaseModel
    {
        [MaxLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }
        
        [JsonIgnore]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}