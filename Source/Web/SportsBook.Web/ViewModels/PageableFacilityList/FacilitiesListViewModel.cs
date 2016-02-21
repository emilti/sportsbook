namespace SportsBook.Web.ViewModels.PageableFacilityList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using SportsBook.Data.Models;
    using SportsBook.Web.ViewModels.Facilities;

    public class FacilitiesListViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string Search { get; set; }

        public string SportCategory { get; set; }

        public IEnumerable<FacilityViewModel> Facilities { get; set; }
    }
}