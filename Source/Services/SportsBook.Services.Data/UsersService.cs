namespace SportsBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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
    }
}
