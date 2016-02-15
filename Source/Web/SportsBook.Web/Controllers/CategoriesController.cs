namespace SportsBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using SportsBook.Services.Data.Contracts;

    public class CategoriesController : BaseController
    {
        private readonly ICommentsService comments;
        private readonly IFacilitiesService facilities;
        private readonly IUsersService users;

        public CategoriesController(ICommentsService commentsService, IFacilitiesService facilitiesService, IUsersService usersService)
        {
            this.comments = commentsService;
            this.facilities = facilitiesService;
            this.users = usersService;
        }

      
    }
}