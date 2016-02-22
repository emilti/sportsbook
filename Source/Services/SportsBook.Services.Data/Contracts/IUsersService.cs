namespace SportsBook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsBook.Data.Models;

    public interface IUsersService
    {
        IQueryable<AppUser> All();

        AppUser GetUserDetails(string id);

        void UpdateUser(AppUser user);

        IQueryable<Facility> GetFacilitiesForUser(AppUser user);

        IQueryable<Event> GetEventsForUser(AppUser user);

        void Remove(AppUser appUser);

        void SaveChanges();

        void Dispose();
    }
}
