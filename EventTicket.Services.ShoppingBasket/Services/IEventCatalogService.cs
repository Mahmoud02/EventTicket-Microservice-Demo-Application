using EventTicket.Services.ShoppingBasket.Entities;
using System;
using System.Threading.Tasks;

namespace EventTicket.Services.ShoppingBasket.Services
{
    public interface IEventCatalogService
    {
        Task<Event> GetEvent(Guid id);
    }
}