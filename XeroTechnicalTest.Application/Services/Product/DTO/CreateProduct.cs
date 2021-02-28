namespace XeroTechnicalTest.Domain.Services.DTO
{
    public class CreateProduct
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public Models.Product ToProduct()
        {
            return new Models.Product()
            {
                Name = Name,
                Description = Description,
                Price = Price,
                DeliveryPrice = DeliveryPrice
            }; 
        }
    }
}