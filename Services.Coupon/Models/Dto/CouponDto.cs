﻿namespace Services.Coupon.Models.Dto
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmout { get; set; }
        public int MinAmount { get; set; }
    }
}
