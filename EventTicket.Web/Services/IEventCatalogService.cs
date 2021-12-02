﻿using EventTicket.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTicket.Web.Services
{
    public interface IEventCatalogService
    {
        Task<IEnumerable<Event>> GetAll();
        Task<IEnumerable<Event>> GetByCategoryId(Guid categoryid);
        Task<Event> GetEvent(Guid id);
        Task<IEnumerable<Category>> GetCategories();
    }
}
