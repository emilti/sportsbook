namespace SportsBook.Web.ViewModels.Facilities
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using SportsBook.Data.Models;
    using SportsBook.Web.Infrastructure.Mapping;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class FacilityViewModel : IMapFrom<SportsBook.Data.Models.Facility>, IHaveCustomMappings
    {
        private ICollection<SportCategory> sportCategories;

        public FacilityViewModel()
        {
            this.SportCategories = new HashSet<SportCategory>();
        }

        public int Id { get; set; }

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

        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        public string Image { get; set; }

        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Facility, FacilityViewModel>();
        }
    }
}