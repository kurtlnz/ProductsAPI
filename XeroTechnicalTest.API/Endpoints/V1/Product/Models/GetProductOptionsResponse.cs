using System.Collections.Generic;
using XeroTechnicalTest.Domain.Models;

namespace XeroTechnicalTest.Endpoints.V1.Product
{
    public class GetProductOptionsResponse
    {
        public List<ProductOption> Items { get; set; }
    }
}