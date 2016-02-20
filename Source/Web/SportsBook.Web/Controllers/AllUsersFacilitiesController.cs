namespace SportsBook.Web.Controllers
{
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Facilities;

    public class AllUsersFacilitiesController : BaseController
    {
        private readonly IFacilitiesService facilities;
        private readonly IUsersService users;
        private readonly ICitiesService cities;
        private readonly ISportCategoriesService sportCategories;

        public AllUsersFacilitiesController(IFacilitiesService facilitiesService, IUsersService usersService, ICitiesService citiesService, ISportCategoriesService sportCategories)
        {
            this.facilities = facilitiesService;
            this.users = usersService;
            this.cities = citiesService;
            this.sportCategories = sportCategories;
        }

        public ActionResult FacilityDetails(int id)
        {
            Facility foundFacility = this.facilities.GetFacilityDetails(id);
            var facilityForView = AutoMapperConfig.Configuration.CreateMapper().Map<FacilityDetailedViewModel>(foundFacility);
            return this.View(facilityForView);
        }
    }
}