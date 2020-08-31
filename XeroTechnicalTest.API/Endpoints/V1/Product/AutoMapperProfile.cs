using AutoMapper;
using XeroTechnicalTest.Domain.Services.DTO;
using XeroTechnicalTest.Domain.Services.Product.DTO;

namespace XeroTechnicalTest.Endpoints.V1.Product
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateProductRequest, CreateProduct>();
            CreateMap<UpdateProductRequest, UpdateProduct>();
            
            CreateMap<CreateOptionRequest, CreateProductOption>();
            CreateMap<UpdateOptionRequest, UpdateProductOption>();
        }
    }
}