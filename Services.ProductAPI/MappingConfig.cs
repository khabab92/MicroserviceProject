using AutoMapper;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.Dto;

namespace Services.Coupon
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps() { 
        var mappingConfig = new MapperConfiguration(config=>
        {
            config.CreateMap<ProductDto,Product>().ReverseMap();
        }
        );
            return mappingConfig;
        }
    }
}
