namespace SportsBook.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Areas.Facilities.ViewModels.Facilities;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;

    public class HomeController : BaseController
    {
        private readonly IFacilitiesService facilities;

        public HomeController(IFacilitiesService facilitiesService)
        {
            this.facilities = facilitiesService;
        }

        public ActionResult Index()
        {
            List<Facility> foundFacilities = new List<Facility>();
            List<FacilityViewModel> foundFacilitiesToView = new List<FacilityViewModel>();
            // foundFacilities =
            //    this.Cache.Get(
            //         "newFacilities",
            //         () => this.facilities.GetTopFacilities().ToList(),
            //         5 * 60);
            foundFacilities = this.facilities.GetTopFacilities().ToList();
            foundFacilitiesToView = AutoMapperConfig.Configuration.CreateMapper().Map<List<FacilityViewModel>>(foundFacilities);
            return this.View(foundFacilitiesToView);
        }
    }
}
