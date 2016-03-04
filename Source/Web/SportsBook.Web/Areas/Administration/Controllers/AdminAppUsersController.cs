namespace SportsBook.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SportsBook.Data.Models;
    using SportsBook.Services.Data.Contracts;
    using ViewModels;

    [Authorize(Roles ="Admin")]
    public class AdminAppUsersController : Controller
    {
        private SportsBookDbContext context;

        private readonly IUsersService users;

        public AdminAppUsersController(IUsersService usersService)
        {
            this.users = usersService;
            this.context = new SportsBookDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AppUsers_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.users.All()
                 .To<AppUserGridViewModel>()
                 .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AppUsers_Create([DataSourceRequest]DataSourceRequest request, AppUserGridViewModel appUser)
        {
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(this.context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this.context));

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "User" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var newId = string.Empty;
            if (this.ModelState.IsValid)
            {
                AppUser userToCreate = new AppUser
                {
                    Avatar = appUser.Avatar,
                    Email = appUser.Email,
                    UserName = appUser.UserName,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName
                };

                userManager.Create(userToCreate, appUser.Password);
                userManager.AddToRole(userToCreate.Id, "User");
                newId = userToCreate.Id;
            }


            var userToDisplay =
             this.users.All()
             .To<AppUserGridViewModel>()
             .FirstOrDefault(x => x.Id == newId);

            return this.Json(new[] { userToDisplay
    }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AppUsers_Update([DataSourceRequest]DataSourceRequest request, AppUserGridViewModel appUser)
        {
            if (this.ModelState.IsValid)
            {
                AppUser userToEdit = this.users.GetUserDetails(appUser.Id);
                userToEdit.Avatar = appUser.Avatar;
                userToEdit.Email = appUser.Email;
                userToEdit.UserName = appUser.UserName;
                userToEdit.FirstName = appUser.FirstName;
                userToEdit.LastName = appUser.LastName;
                this.users.UpdateUser(userToEdit);
            }

            var userToDisplay =
          this.users.All()
          .To<AppUserGridViewModel>()
          .FirstOrDefault(x => x.Id == appUser.Id);


            return this.Json(new[] { userToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AppUsers_Destroy([DataSourceRequest]DataSourceRequest request, AppUser appUser)
        {

            this.users.Remove(appUser);
            this.users.SaveChanges();

            return this.Json(new[] { appUser }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            this.users.Dispose();
            base.Dispose(disposing);
        }
    }
}
