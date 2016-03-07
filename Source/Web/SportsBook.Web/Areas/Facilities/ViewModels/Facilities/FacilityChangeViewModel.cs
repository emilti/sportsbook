namespace SportsBook.Web.Areas.Facilities.ViewModels.Facilities
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Web.Infrastructure.Mapping;

    public class FacilityChangeViewModel : IMapFrom<Facility>, IMapTo<SportsBook.Data.Models.Facility>, IHaveCustomMappings
    {
        private ICollection<SportCategory> sportCategories;

        public FacilityChangeViewModel()
        {
            this.SportCategories = new HashSet<SportCategory>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително!")]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        [StringLength(50, ErrorMessage = "{0}то трябва да е поне {2} символа.", MinimumLength = 2)]
        [DisplayName("Име")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Описанието е задължително!")]
        [AllowHtml]
        [StringLength(2000, ErrorMessage = "{0}то трябва да е поне {2} символа.", MinimumLength = 2)]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиде символ")]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Град")]
        public int CityId { get; set; }

        [DisplayName("Град")]
        public virtual City City { get; set; }

        [Required(ErrorMessage = "Снимката е задължителна!")]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ!")]
        [DisplayName("Снимка")]
        public string Image { get; set; }

        public IEnumerable<SelectListItem> CitiesDropDown { get; set; }

        public IEnumerable<int> SportCategoriesIds { get; set; }

        public IEnumerable<SelectListItem> SportCategoriesDropDown { get; set; }

        [DisplayName("Категории спортове")]
        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        [DisplayName("Географска дължина")]
        public decimal Longitude { get; set; }

        [DisplayName("Географска ширина")]
        public decimal Latitude { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<FacilityChangeViewModel, Facility>();
        }
    }
}
