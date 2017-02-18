namespace SportsBook.Web.Areas.Facilities.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Facilities;
    using ViewModels.PageableFacilityList;
    using Web.Controllers;

    public class FacilitiesPublicController : BaseController
    {
        private const int ItemsPerPage = 6;
        private readonly IFacilitiesService facilities;
        private readonly IUsersService users;
        private readonly ICitiesService cities;
        private readonly ISportCategoriesService sportCategories;

        public FacilitiesPublicController(IFacilitiesService facilitiesService, IUsersService usersService, ICitiesService citiesService, ISportCategoriesService sportCategories)
        {
            this.facilities = facilitiesService;
            this.users = usersService;
            this.cities = citiesService;
            this.sportCategories = sportCategories;
        }

        [HttpGet]
        public ActionResult FacilityDetails(int id)
        {
            Facility foundFacility = this.facilities.GetFacilityDetails(id);
            var facilityForView = AutoMapperConfig.Configuration.CreateMapper().Map<FacilityDetailedViewModel>(foundFacility);
            return this.View(facilityForView);
        }

        [HttpGet]
        public string GetFacilityName(int id)
        {
            Facility foundFacility = this.facilities.GetFacilityDetails(id);
            return foundFacility.Name;
        }

        [HttpGet]
        public ActionResult SearchFacilities(FacilitiesListViewModel model, int id = 0)
        {
           if (model.CurrentPage == 0)
            {
                model.CurrentPage = 1;
            }

            var sportCategory = this.sportCategories.All().FirstOrDefault(x => x.Id == id);

            var sportCategories = this.sportCategories.All();
            model.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            var foundFacilities = new List<Facility>();

            if (id > 0)
                {
                foundFacilities = this.facilities.All().Where(y => y.SportCategories.Where(x => x.Id == id).Count() > 0).ToList();
            }
            else
            {
                foundFacilities = this.facilities.All().ToList();
            }

            var allItemsCount = foundFacilities.Count();
            var foundFacilitiesAfterSearch = foundFacilities.ToList();
            if (!string.IsNullOrEmpty(model.Search))
            {
                foundFacilitiesAfterSearch = foundFacilities.Where(a => a.Name.ToUpper().Contains(model.Search.ToUpper())).ToList();
                allItemsCount = foundFacilitiesAfterSearch.Count();
            }

            var page = model.CurrentPage;

            var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;
            var foundFacilitiestoView =
                foundFacilitiesAfterSearch
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .Skip(itemsToSkip).Take(ItemsPerPage)
               .ToList();
            var facilitiesToView = AutoMapperConfig.Configuration.CreateMapper().Map<List<FacilityViewModel>>(foundFacilitiestoView);

            var viewModel = new FacilitiesListViewModel()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Facilities = facilitiesToView
            };

            return this.View(viewModel);
        }

        private IEnumerable<SelectListItem> GetSelectListSportCategories(IEnumerable<SportCategory> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                });
            }

            return selectList;
        }
    }
}