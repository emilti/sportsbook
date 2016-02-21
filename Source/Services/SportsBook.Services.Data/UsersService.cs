namespace SportsBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using SportsBook.Data.Common;
    using SportsBook.Data.Models;
    using SportsBook.Services.Data.Contracts;

    public class UsersService : IUsersService
    {
        private readonly IRepository<AppUser> users;

        public UsersService(
            IRepository<AppUser> usersRepo)
        {
            this.users = usersRepo;
        }

        public AppUser GetUserDetails(string id)
        {
            return this.users.GetById(id);
        }

        public void UpdateUser(AppUser foundUser)
        {
            this.users.SaveChanges();
        }

        public IQueryable<Facility> GetFacilitiesForUser(AppUser user)
        {
            return user.FavoriteFacilities.AsQueryable();
        }

        public IQueryable<AppUser> All()
        {
            return this.users.All();
        }

        public void Remove(AppUser appUser)
        {
            this.users.Delete(appUser.Id);
            this.users.SaveChanges();
        }

        public void SaveChanges()
        {
            this.users.SaveChanges();
        }

        public void Dispose()
        {
            this.users.Dispose();
        }
    }
}
