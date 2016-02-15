namespace SportsBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using SportsBook.Services.Data.Contracts;
    using ViewModels.Facilities;

    public class FacilitiesController : BaseController
    {
        private readonly IFacilitiesService facilities;
        private readonly IUsersService users;

        public FacilitiesController(IFacilitiesService facilitiesService, IUsersService usersService)
        {
            this.facilities = facilitiesService;
            this.users = usersService;
        }

        public ActionResult FacilityDetails(int id)
        {
            Facility foundFacility = this.facilities.GetFacilityDetails(id);
            var facilityForView = AutoMapperConfig.Configuration.CreateMapper().Map<FacilityDetailedViewModel>(foundFacility);
            return this.View(facilityForView);
        }
    }
}
