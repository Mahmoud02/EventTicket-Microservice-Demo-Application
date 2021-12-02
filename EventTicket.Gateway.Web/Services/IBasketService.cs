using Gateway.Shared.Basket;
using System;
using System.Threading.Tasks;

namespace EventTicket.Gateway.Web.Services
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasket(Guid basketId);
    }
}