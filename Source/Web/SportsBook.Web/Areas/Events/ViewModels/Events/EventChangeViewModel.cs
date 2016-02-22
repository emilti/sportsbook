namespace SportsBook.Web.Areas.Facilities.ViewModels.Facilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Web.Infrastructure.Mapping;

    public class EventChangeViewModel : IMapFrom<Event>, IMapTo<Event>, IHaveCustomMappings
    {
        private ICollection<SportCategory> sportCategories;

        public EventChangeViewModel()
        {
            this.SportCategories = new HashSet<SportCategory>();
        }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(2000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        public string Description { get; set; }

        public DateTime Start { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        public string Image { get; set; }

        public IEnumerable<SelectListItem> CitiesDropDown { get; set; }

        public IEnumerable<int> SportCategoriesIds { get; set; }

        public IEnumerable<SelectListItem> SportCategoriesDropDown { get; set; }

        [DisplayName("Sport Categries")]
        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<FacilityChangeViewModel, Facility>();
        }
    }
}
