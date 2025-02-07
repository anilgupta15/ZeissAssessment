using AutoMapper;
using Products.Domain.DTOs;
using Products.Domain.Entities;

namespace Products.Application.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Creates a bidirectional mapping between Product and ProductDTO
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();

        }
    }
}
