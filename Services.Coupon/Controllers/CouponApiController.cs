using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Coupon.Data;
using Services.Coupon.Models;
using Services.Coupon.Models.Dto;

namespace Services.Coupon.Controllers
{
    [ApiController]
    public class CouponApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;
        public CouponApiController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper =  mapper;
            _responseDto = new ResponseDto();
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Models.Coupon> coupons = _db.Coupons.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
                
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return _responseDto;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Models.Coupon coupon = _db.Coupons.First(u=> u.CouponId == id);
                _responseDto.Result = _mapper.Map<CouponDto>(coupon); 
                
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message=ex.Message.ToString();
            }
            return _responseDto;
        }
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Models.Coupon coupon = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _responseDto.Result = _mapper.Map<CouponDto>(coupon); 

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return _responseDto;
        }
        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {

                Models.Coupon coupon = _mapper.Map<Models.Coupon>(couponDto);
                _db.Coupons.Add(coupon);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(coupon); 
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return _responseDto;
        }
        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {

                Models.Coupon coupon = _mapper.Map<Models.Coupon>(couponDto);
                _db.Coupons.Update(coupon);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return _responseDto;
        }
        [HttpDelete]
        [Route("{couponId:int}")]
        public ResponseDto Delete(int couponId)
        {
            try
            {
                Models.Coupon coupon = _db.Coupons.First(u => u.CouponId == couponId);
                _db.Coupons.Remove(coupon);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return _responseDto;
        }
    }
}
