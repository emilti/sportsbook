namespace SportsBook.Web.Areas.Facilities.Controllers
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
    using Web.Controllers;

    public class FacilitiesPrivateController : BaseController
    {
        private readonly IFacilitiesService facilities;
        private readonly IUsersService users;
        private readonly ICitiesService cities;
        private readonly ISportCategoriesService sportCategories;

        public FacilitiesPrivateController(IFacilitiesService facilitiesService, IUsersService usersService, ICitiesService citiesService, ISportCategoriesService sportCategories)
        {
            this.facilities = facilitiesService;
            this.users = usersService;
            this.cities = citiesService;
            this.sportCategories = sportCategories;
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddFacility()
        {
            FacilityChangeViewModel model = new FacilityChangeViewModel();
            var cities = this.cities.All();
            model.CitiesDropDown = this.GetSelectListCities(cities);
            var sportCategories = this.sportCategories.All();
            model.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddFacility(FacilityChangeViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Facility mappedFacility = AutoMapperConfig.Configuration.CreateMapper().Map<Facility>(model);
                foreach (var categoryId in model.SportCategoriesIds)
                {
                    SportCategory currentCategory = this.sportCategories.GetById(categoryId);
                    mappedFacility.SportCategories.Add(currentCategory);
                }

                mappedFacility.AuthorId = this.User.Identity.GetUserId();

                this.facilities.Add(mappedFacility);
                return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = mappedFacility.Id, area = "Facilities" });
            }

            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditFacility(int Id)
        {
            Facility editedFacilty = this.facilities.GetFacilityDetails(Id);
            FacilityChangeViewModel mappedFacility = AutoMapperConfig.Configuration.CreateMapper().Map<FacilityChangeViewModel>(editedFacilty);
            var cities = this.cities.All();
            mappedFacility.CitiesDropDown = this.GetSelectListCities(cities);
            var sportCategories = this.sportCategories.All();
            mappedFacility.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            return this.View(mappedFacility);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditFacility(int id, FacilityChangeViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Facility mappedFacility = AutoMapperConfig.Configuration.CreateMapper().Map<Facility>(model);

                Facility currentFacility = this.facilities.GetFacilityDetails(id);
                foreach (var category in currentFacility.SportCategories)
                {
                    category.Facilities.Remove(currentFacility);
                }

                currentFacility.SportCategories.Clear();

                this.facilities.Save();
                foreach (var categoryId in model.SportCategoriesIds)
                {
                    SportCategory currentCategory = this.sportCategories.GetById(categoryId);
                    mappedFacility.SportCategories.Add(currentCategory);
                }

                this.facilities.UpdateFacility(id, mappedFacility);
                return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = id, area = "Facilities" });
            }

            return this.View(model);
        }

        private IEnumerable<SelectListItem> GetSelectListCities(IEnumerable<City> elements)
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
