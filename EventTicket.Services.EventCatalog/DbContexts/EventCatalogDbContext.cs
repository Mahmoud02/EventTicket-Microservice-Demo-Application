using System;
using EventTicket.Services.EventCatalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.Services.EventCatalog.DbContexts
{
    public class EventCatalogDbContext : DbContext
    {
        public EventCatalogDbContext(DbContextOptions<EventCatalogDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var concertGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var soccerGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            var conferenceGuid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = concertGuid,
                Name = "Concerts"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = soccerGuid,
                Name = "Soccer"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = conferenceGuid,
                Name = "Conferences"
            });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{EE272F8B-6096-4CB6-8625-BB4BB2D89E8B}"),
                Name = "Tamer Hosny Live",
                Price = 65,
                Artist = "Tamer Hosny",
                Date = DateTime.Now.AddMonths(6),
                Description = "Tamer Hosny will be performing  at Must Theater and at the Football Yard of Misr University for science and technology",
                ImageUrl = "https://www.ticketsmarche.com/mm/TAMER%20SLIDER12.jpg",
                CategoryId = concertGuid
            });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{3448D5A4-0F72-4DD7-BF15-C14A46B26C00}"),
                Name = "Omar Khairat Live!",
                Price = 85,
                Artist = "Omar Khairat",
                Date = DateTime.Now.AddMonths(9),
                Description = "Event House proudly presents the legendary music composer Omar Khairat at The Marquee Theater for an exquisite musical experience that will enchant our audience in an exclusive concertConcert by: Cairo Festival CityOrganized by: Event House",
                ImageUrl = "https://www.ticketsmarche.com/mm/okdec2021slider.jpg",
                CategoryId = concertGuid
            });
        }
    }
}
