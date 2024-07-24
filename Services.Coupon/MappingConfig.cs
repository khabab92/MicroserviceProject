﻿using AutoMapper;
using Services.Coupon.Models.Dto;

namespace Services.Coupon
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps() { 
        var mappingConfig = new MapperConfiguration(config=>
        {
            config.CreateMap<CouponDto,Models.Coupon>().ReverseMap();
        }
        );
            return mappingConfig;
        }
    }
}
