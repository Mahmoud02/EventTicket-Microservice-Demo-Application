using AutoMapper;
using EventTicket.Services.Discount.Entities;
using EventTicket.Services.Discount.Models;

namespace EventTicket.Services.Discount.Profiles
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
    }
}
