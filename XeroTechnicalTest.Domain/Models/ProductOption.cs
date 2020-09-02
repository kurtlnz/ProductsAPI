using System;
using System.Text.Json.Serialization;

namespace XeroTechnicalTest.Domain.Models
{
    public class ProductOption : BaseModel
    {
        [JsonIgnore]
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}