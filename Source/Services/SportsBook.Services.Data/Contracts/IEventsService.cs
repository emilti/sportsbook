namespace SportsBook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsBook.Data.Models;

    public interface IEventsService
    {
        IQueryable<Event> GetTopEvents();

        IQueryable<Event> All(int page = 1, int pageSize = 10);

        Event GetEventDetails(int id);

        void UpdateEvent(int id, Event sportEvent);

        void Add(Event sportEvent);

        void Remove(Event sportEvent);

        void Save();

        void Dispose();
    }
}
