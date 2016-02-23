namespace SportsBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Account;

    public class UsersController :Controller
    {
        private readonly IUsersService users;

        public UsersController(IUsersService usersService)
        {
            this.users = usersService;
        }

        [ValidateAntiForgeryToken]
        public ActionResult SearchUsers(string search)
        {
            var foundUsersIndata = this.users.All().Where(a => a.UserName.ToUpper().Contains(search.ToUpper())).OrderBy(u => u.UserName).ToList();
            var foundUsersToView = AutoMapperConfig.Configuration.CreateMapper().Map<List<AppUserViewModel>>(foundUsersIndata);
            return this.View(foundUsersToView);
        }
    }
}