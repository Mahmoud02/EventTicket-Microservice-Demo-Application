using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventTicket.Gateway.Web.Models;
using Gateway.Shared.Event;

namespace EventTicket.Gateway.Web.Services
{
    public interface ICatalogService
    {
        Task<List<EventDto>> GetEventsPerCategory(Guid categoryId);
        Task<EventDto> GetEventById(Guid eventId);

        Task<List<CategoryDto>> GetAllCategories();
        Task<List<EventDto>> GetAllEvents();
    }
}