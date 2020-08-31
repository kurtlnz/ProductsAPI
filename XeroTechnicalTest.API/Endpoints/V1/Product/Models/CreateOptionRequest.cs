using System.ComponentModel.DataAnnotations;

namespace XeroTechnicalTest.Endpoints.V1.Product
{
    public class CreateOptionRequest
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}