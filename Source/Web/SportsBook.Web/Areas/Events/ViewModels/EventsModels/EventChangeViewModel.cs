namespace SportsBook.Web.Areas.Events.ViewModels.EventsModels
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

        [Required(ErrorMessage = "Името е задължително")]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        [StringLength(50, ErrorMessage = "{0}то трябва да е поне {2} сивмола.", MinimumLength = 2)]
        [DisplayName("Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описанието е задължително")]
        [AllowHtml]
        [StringLength(2000, ErrorMessage = "{0}то трябва да е поне {2} символа.", MinimumLength = 2)]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Начало")]
        public DateTime Start { get; set; }

        public int CityId { get; set; }

        [DisplayName("Град")]
        public virtual City City { get; set; }

        [Required(ErrorMessage = "Снимката е задължителна")]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        [DisplayName("Снимка")]
        public string Image { get; set; }

        public IEnumerable<SelectListItem> CitiesDropDown { get; set; }

        public IEnumerable<int> SportCategoriesIds { get; set; }

        public IEnumerable<SelectListItem> SportCategoriesDropDown { get; set; }

        [DisplayName("Спортни категории")]
        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<EventChangeViewModel, Facility>();
        }
    }
}
