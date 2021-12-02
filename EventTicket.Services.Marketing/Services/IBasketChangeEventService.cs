using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventTicket.Services.Marketing.Models;

namespace EventTicket.Services.Marketing.Services
{
    public interface IBasketChangeEventService
    {
        Task<List<BasketChangeEvent>> GetBasketChangeEvents(DateTime startDate, int max);
    }
}