using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models.Dto;
using WebApp.Services.IService;

namespace WebApp.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService=couponService;
        }
        public async Task<IActionResult> Coupons()
        {
            List<CouponDto> lst = new();
            ResponseDto responseDto = await _couponService.GetAllCouponAsync();
            if(responseDto != null & responseDto.IsSuccess) {
                lst = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["Error"] = responseDto?.Message;
            }
            return View(lst);
        }
        public async Task<IActionResult> CreateCoupon(CouponDto model)
        {
            model.CouponCode = "400FF";
            model.DiscountAmout = 20;
            model.MinAmount = 50;
            
           if (model != null)
            {
                ResponseDto response = await _couponService.CreateCouponAsync(model);
                if (response != null)
                {
                    TempData["success"] = "Coupon Created Successfuly";
                    return RedirectToAction(nameof(Coupons));
                }
                else
                {
                    TempData["Error"] = response?.Message;
                }
            }
            return View(model);
        }
 
        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
        
            if (couponId != null)
            {
                ResponseDto response = await _couponService.DeleteCouponAsync(couponId);
                if (response != null)
                {
                    TempData["success"] = "Coupon Deleted Successfuly";
                    return RedirectToAction(nameof(Coupons));
                }
                else
                {
                    TempData["Error"] = response?.Message;
                }
            }
            return RedirectToAction(nameof(Coupons));
        }
    }
}
