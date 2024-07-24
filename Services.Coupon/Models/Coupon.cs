using System.ComponentModel.DataAnnotations;

namespace Services.Coupon.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        [Required]
        public double DiscountAmout { get; set; }
        public int MinAmount { get; set; }
    }
}
