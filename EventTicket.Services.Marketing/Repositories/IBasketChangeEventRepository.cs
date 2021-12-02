using System.Threading.Tasks;
using EventTicket.Services.Marketing.Entities;

namespace EventTicket.Services.Marketing.Repositories
{
    public interface IBasketChangeEventRepository
    {
        Task AddBasketChangeEvent(BasketChangeEvent basketChangeEvent);
    }
}