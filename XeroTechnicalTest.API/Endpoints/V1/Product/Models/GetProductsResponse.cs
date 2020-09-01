using System.Collections.Generic;

namespace XeroTechnicalTest.Endpoints.V1.Product
{
    public class GetProductsResponse
    {
        public List<Domain.Models.Product> Items { get; set; }
    }
}