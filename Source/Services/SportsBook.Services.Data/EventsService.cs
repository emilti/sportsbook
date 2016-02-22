namespace SportsBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;
    using SportsBook.Data.Common;
    using SportsBook.Data.Models;

    public class EventsService : IEventsService
    {
        private readonly IDbRepository<Event> events;

        public EventsService(IDbRepository<Event> events)
        {
            this.events = events;
        }

        public Event Add(string content, string authorId, string username, Event commentedEvent)
        {
            // var newComment = new FacilityComment
            // {
            //     FacilityId = facilityId,
            //     Content = content,
            //     AuthorName = username,
            //     AuthorId = authorId,
            //     CreatedOn = DateTime.UtcNow,
            // }; 
            //
            return new Event();
        }

        public IQueryable<Event> All(int page = 1, int pageSize = 10)
        {
            return this.events.All();
        }

        public IQueryable<Event> GetTopEvents()
        {
            return this.events.All().OrderByDescending(x => x.CreatedOn).Take(6);
        }

        public Event GetEventDetails(int id)
        {
            return this.events.GetById(id);
        }

        public void UpdateEvent(int id, Event sportEvent)
        {
            Event eventToUpdate = this.events.GetById(id);
            eventToUpdate.CityId = sportEvent.CityId;
            eventToUpdate.Description = sportEvent.Description;
            eventToUpdate.Image = sportEvent.Image;
            eventToUpdate.Name = sportEvent.Name;
            eventToUpdate.SportCategories = sportEvent.SportCategories;
            eventToUpdate.Start = sportEvent.Start;
            this.events.Save();
        }

        public void Add(Event sportEvent)
        {
            this.events.Add(sportEvent);
            this.events.Save();
        }

        public void Save()
        {
            this.events.Save();
        }

        public void Remove(Event sportEvent)
        {
            this.events.HardDelete(sportEvent.Id);
        }

        public void Dispose()
        {
            this.events.Dispose();
        }
    }
}
