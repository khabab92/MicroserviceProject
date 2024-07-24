using Microsoft.EntityFrameworkCore;
using Services.Coupon.Models;

namespace Services.Coupon.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Models.Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.Coupon>().HasData(new Models.Coupon
            {
                CouponId = 1,
                CouponCode = "100FF",
                DiscountAmout = 10,
                MinAmount = 20
            });
            modelBuilder.Entity<Models.Coupon>().HasData(new Models.Coupon
            {
                CouponId = 2,
                CouponCode = "200FF",
                DiscountAmout = 20,
                MinAmount = 40
            });
        }
    }
}
