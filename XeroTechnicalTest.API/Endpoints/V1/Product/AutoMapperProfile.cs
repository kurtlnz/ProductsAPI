using AutoMapper;
using XeroTechnicalTest.Domain.Services.DTO;

namespace XeroTechnicalTest.Endpoints.V1.Product
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateProductRequest, CreateProduct>();
            CreateMap<UpdateProductRequest, UpdateProduct>();
        }
    }
}