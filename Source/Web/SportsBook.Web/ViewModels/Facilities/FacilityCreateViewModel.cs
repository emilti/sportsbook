namespace SportsBook.Web.ViewModels.Facilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Web.Infrastructure.Mapping;

    public class FacilityCreateViewModel : IMapFrom<SportsBook.Data.Models.Facility>, IHaveCustomMappings
    {
        private ICollection<SportCategory> sportCategories;

        public FacilityCreateViewModel()
        {
            this.SportCategories = new HashSet<SportCategory>();
        }

        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }

        [MaxLength(1200)]
        [MinLength(2)]
        public string Description { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        public string Image { get; set; }

        public IEnumerable<SelectListItem> CitiesDropDown { get; set; }

        public IEnumerable<SelectListItem> SportCategoriesDropDown { get; set; }

        [DisplayName("Sport Categries")]
        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Facility, FacilityCreateViewModel>();
        }
    }
}
