using EventTicket.Web.Extensions;
using EventTicket.Web.Models;
using EventTicket.Web.Models.Api;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EventTicket.Web.Services
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly HttpClient client;
        private readonly Settings settings;
        private readonly IHttpContextAccessor httpContextAccessor;


        public ShoppingBasketService(HttpClient client, Settings settings, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.settings = settings;
            this.httpContextAccessor = httpContextAccessor;

        }

        public async Task<BasketLine> AddToBasket(Guid basketId, BasketLineForCreation basketLine)
        {
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            if (basketId == Guid.Empty)
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));

                var basketResponse = await client.PostAsJson("api/baskets", new BasketForCreation { UserId = settings.UserId });
                var basket = await basketResponse.ReadContentAs<Basket>();
                basketId = basket.BasketId;
            }

            var response = await client.PostAsJson($"api/baskets/{basketId}/basketlines", basketLine);
            return await response.ReadContentAs<BasketLine>();
        }

        public async Task<Basket> GetBasket(Guid basketId)
        {
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            if (basketId == Guid.Empty)
                return null;
            var response = await client.GetAsync($"api/baskets/{basketId}");
            return await response.ReadContentAs<Basket>();
        }

        public async Task<IEnumerable<BasketLine>> GetLinesForBasket(Guid basketId)
        {
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            if (basketId == Guid.Empty)
                return new BasketLine[0];
            var response = await client.GetAsync($"api/baskets/{basketId}/basketLines");
            return await response.ReadContentAs<BasketLine[]>();

        }

        public async Task UpdateLine(Guid basketId, BasketLineForUpdate basketLineForUpdate)
        {
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            await client.PutAsJson($"api/baskets/{basketId}/basketLines/{basketLineForUpdate.LineId}", basketLineForUpdate);
        }

        public async Task RemoveLine(Guid basketId, Guid lineId)
        {
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            await client.DeleteAsync($"api/baskets/{basketId}/basketLines/{lineId}");
        }

        public async Task ApplyCouponToBasket(Guid basketId, CouponForUpdate couponForUpdate)
        {
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.PutAsJson($"api/baskets/{basketId}/coupon", couponForUpdate);
            //return await response.ReadContentAs<Coupon>();
        }

        public async Task<BasketForCheckout> Checkout(Guid basketId, BasketForCheckout basketForCheckout)
        {

            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.PostAsJson($"api/baskets/checkout", basketForCheckout);
            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<BasketForCheckout>();
            else
            {
                throw new Exception("Something went wrong placing your order. Please try again.");
            }
        }
    }
}
