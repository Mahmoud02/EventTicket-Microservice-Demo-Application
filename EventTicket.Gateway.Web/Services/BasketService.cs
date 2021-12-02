using EventTicket.Gateway.Web.Extensions;
using Gateway.Shared.Basket;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventTicket.Gateway.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient client;

        public BasketService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<BasketDto> GetBasket(Guid basketId)
        {
            if (basketId == Guid.Empty)
                return null;
            var response = await client.GetAsync($"/api/baskets/{basketId}");
            return await response.ReadContentAs<BasketDto>();
        }
    }
}
