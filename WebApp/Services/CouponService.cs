using WebApp.Models.Dto;
using WebApp.Services.IService;
using WebApp.Utility;

namespace WebApp.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public  CouponService(IBaseService baseService)
        {
            _baseService=  baseService;
        }
        public async Task<ResponseDto> CreateCouponAsync(CouponDto coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.Post,
                Data = coupon,
                Url = SD.CouponApiBaseUrl + "/api/coupon" 
            });
        }

        public async Task<ResponseDto> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.Delete,
                Url = SD.CouponApiBaseUrl + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDto> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType=SD.ApiType.Get,
                Url = SD.CouponApiBaseUrl+ "/api/coupon"
            });
        }

        public async Task<ResponseDto> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.CouponApiBaseUrl + "/api/coupon/GetByCode/" + couponCode
            });
        }

        public async Task<ResponseDto> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.CouponApiBaseUrl + "/api/coupon" + couponId
            });
        }

        public async Task<ResponseDto> UpdateCouponAsync(CouponDto coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.Put,
                Data = coupon,
                Url = SD.CouponApiBaseUrl + "/api/coupon"
            });
        }
    }
}
