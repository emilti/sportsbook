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

    public class EventsPrivateController : BaseController
    {
        private readonly IEventsService events;
        private readonly IUsersService users;
        private readonly ICitiesService cities;
        private readonly ISportCategoriesService sportCategories;

        public EventsPrivateController(IEventsService eventsService, IUsersService usersService, ICitiesService citiesService, ISportCategoriesService sportCategories)
        {
            this.events = eventsService;
            this.users = usersService;
            this.cities = citiesService;
            this.sportCategories = sportCategories;
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddEvent()
        {
            EventChangeViewModel model = new EventChangeViewModel();
            var cities = this.cities.All();
            model.CitiesDropDown = this.GetSelectListCities(cities);
            var sportCategories = this.sportCategories.All();
            model.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddFacility(EventChangeViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Event mappedEvent = AutoMapperConfig.Configuration.CreateMapper().Map<Event>(model);
                foreach (var categoryId in model.SportCategoriesIds)
                {
                    SportCategory currentCategory = this.sportCategories.GetById(categoryId);
                    mappedEvent.SportCategories.Add(currentCategory);
                }

                mappedEvent.AuthorId = this.User.Identity.GetUserId();

                this.events.Add(mappedEvent);
                return this.RedirectToAction("EventDetails", "EventsPublic", new { id = mappedEvent.Id, area = "Events" });
            }

            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditEvent(int Id)
        {
            Event editedEvent = this.events.GetEventDetails(Id);
            EventChangeViewModel mappedEvent = AutoMapperConfig.Configuration.CreateMapper().Map<EventChangeViewModel>(editedEvent);
            var cities = this.cities.All();
            mappedEvent.CitiesDropDown = this.GetSelectListCities(cities);
            var sportCategories = this.sportCategories.All();
            mappedEvent.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            return this.View(mappedEvent);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent(int id, EventChangeViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Event mappedEvent = AutoMapperConfig.Configuration.CreateMapper().Map<Event>(model);

                Event currentEvent = this.events.GetEventDetails(id);
                foreach (var category in currentEvent.SportCategories)
                {
                    category.Events.Remove(currentEvent);
                }

                currentEvent.SportCategories.Clear();

                this.events.Save();
                foreach (var categoryId in model.SportCategoriesIds)
                {
                    SportCategory currentCategory = this.sportCategories.GetById(categoryId);
                    mappedEvent.SportCategories.Add(currentCategory);
                }

                this.events.UpdateEvent(id, mappedEvent);
                return this.RedirectToAction("EventDetails", "EventsPublic", new { id = id, area = "Events" });
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
