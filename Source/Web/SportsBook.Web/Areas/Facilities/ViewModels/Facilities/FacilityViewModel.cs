namespace SportsBook.Web.Areas.Facilities.ViewModels.Facilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using SportsBook.Data.Models;
    using SportsBook.Web.Infrastructure.Mapping;

    public class FacilityViewModel : IMapFrom<SportsBook.Data.Models.Facility>, IHaveCustomMappings
    {
        private ICollection<SportCategory> sportCategories;

        public FacilityViewModel()
        {
            this.SportCategories = new HashSet<SportCategory>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        [StringLength(50, ErrorMessage = "{0}то трябва да е поне {2} символа.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описанието е задължително")]
        [AllowHtml]
        [StringLength(2000, ErrorMessage = "{0}то трябва да е поне символа {2}.", MinimumLength = 2)]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        public string Description { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public Decimal? Rating { get; set; }

        public int VotesCount { get; set; }

        [Required(ErrorMessage = "Снимата е задължителна")]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        public string Image { get; set; }

        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Facility, FacilityViewModel>()          
            .ForMember(dest => dest.VotesCount,
               opts => opts.MapFrom(src => src.FaciltityRatings.Count));
        }
    }
}