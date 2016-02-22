namespace SportsBook.Web.Areas.Events.ViewModels.PageableEventList
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Facilities;
    using SportsBook.Data.Models;
    using EventsModels;

    public class EventsListViewModel
    {
        private ICollection<SportCategory> sportCategories;
        private ICollection<City> cities;

        public EventsListViewModel()
        {
            this.sportCategories = new HashSet<SportCategory>();
            this.Cities = new HashSet<City>();
        }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string Search { get; set; }

        public IEnumerable<int> SportCategoriesIds { get; set; }

        public IEnumerable<SelectListItem> SportCategoriesDropDown { get; set; }

        [DisplayName("Sport Categries")]
        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        public IEnumerable<int> CitiesIds { get; set; }

        public IEnumerable<SelectListItem> CitiesDropDown { get; set; }

        [DisplayName("Cities")]
        public virtual ICollection<City> Cities
        {
            get { return this.cities; }
            set { this.cities = value; }
        }

        public IEnumerable<EventViewModel> Events { get; set; }
    }
}