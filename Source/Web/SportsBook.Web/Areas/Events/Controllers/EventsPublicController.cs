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

    public class EventsPublicController : BaseController
    {
        private const int ItemsPerPage = 6;
        private readonly IEventsService events;
        private readonly IUsersService users;
        private readonly ICitiesService cities;
        private readonly ISportCategoriesService sportCategories;

        public EventsPublicController(IEventsService eventsService, IUsersService usersService, ICitiesService citiesService, ISportCategoriesService sportCategories)
        {
            this.events = eventsService;
            this.users = usersService;
            this.cities = citiesService;
            this.sportCategories = sportCategories;
        }

        public ActionResult EventDetails(int id)
        {
            Event foundEvent = this.events.GetEventDetails(id);
            var eventForView = AutoMapperConfig.Configuration.CreateMapper().Map<EventDetailedViewModel>(foundEvent);
            return this.View(eventForView);
        }

        [HttpGet]
        public ActionResult SearchEvents(EventsListViewModel model, int id = 0)
        {
           if (model.CurrentPage == 0)
            {
                model.CurrentPage = 1;
            }

            var sportCategory = this.sportCategories.All().FirstOrDefault(x => x.Id == id);

            var sportCategories = this.sportCategories.All();
            model.SportCategoriesDropDown = this.GetSelectListSportCategories(sportCategories);
            var foundEvents = new List<Event>();

            if (id > 0)
                {
                foundEvents = this.events.All().Where(y => y.SportCategories.Where(x => x.Id == id).Count() > 0).ToList();
            }
            else
            {
                foundEvents = this.events.All().ToList();
            }

            var allItemsCount = foundEvents.Count();
            var foundEventsAfterSearch = foundEvents.ToList();
            if (!string.IsNullOrEmpty(model.Search))
            {
                foundEventsAfterSearch = foundEvents.Where(a => a.Name.ToUpper().Contains(model.Search.ToUpper())).ToList();
                allItemsCount = foundEventsAfterSearch.Count();
            }

            var page = model.CurrentPage;

            var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;
            var foundEventsToView =
                foundEventsAfterSearch
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .Skip(itemsToSkip).Take(ItemsPerPage)
               .ToList();
            var eventsToView = AutoMapperConfig.Configuration.CreateMapper().Map<List<EventViewModel>>(foundEventsToView);

            var viewModel = new EventsListViewModel()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Events = eventsToView
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