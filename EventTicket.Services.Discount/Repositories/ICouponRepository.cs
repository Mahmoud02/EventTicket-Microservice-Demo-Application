using System;
using EventTicket.Services.Discount.Entities;
using System.Threading.Tasks;

namespace EventTicket.Services.Discount.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCouponByCode(string couponCode);
        Task UseCoupon(Guid couponId);
        Task<Coupon> GetCouponById(Guid couponId);
    }
}
