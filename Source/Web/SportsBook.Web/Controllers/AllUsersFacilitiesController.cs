namespace SportsBook.Web.Controllers
{
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Facilities;
    using ViewModels.PageableFacilityList;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class AllUsersFacilitiesController : BaseController
    {
        const int ItemsPerPage = 6;
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

        [HttpGet]
        public ActionResult SearchFacilities(FacilitiesListViewModel model, int id = 1)
        {
            var page = id;
            var allItemsCount = this.facilities.All().Count();
            var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;
            var foundFacilities =
                this.facilities.All()
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .Skip(itemsToSkip).Take(ItemsPerPage)
               .ToList();
            var facilitiesToView = AutoMapperConfig.Configuration.CreateMapper().Map<List<FacilityViewModel>>(foundFacilities);

            var viewModel = new FacilitiesListViewModel()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Facilities = facilitiesToView
            };

            return this.View(viewModel);
        }
    }
}