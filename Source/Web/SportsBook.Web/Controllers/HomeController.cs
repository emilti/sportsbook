namespace SportsBook.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Facilities;

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
            foundFacilities = this.facilities.GetTopFacilities().ToList();
            foundFacilitiesToView = AutoMapperConfig.Configuration.CreateMapper().Map<List<FacilityViewModel>>(foundFacilities);
            return this.View(foundFacilitiesToView);
        }
    }
}
