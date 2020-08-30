namespace XeroTechnicalTest.Endpoints.V1.Product
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}