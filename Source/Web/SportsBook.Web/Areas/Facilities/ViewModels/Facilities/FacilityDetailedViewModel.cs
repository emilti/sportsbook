namespace SportsBook.Web.Areas.Facilities.ViewModels.Facilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using SportsBook.Web.Infrastructure.Mapping;

    public class FacilityDetailedViewModel : IMapFrom<SportsBook.Data.Models.Facility>, IHaveCustomMappings
    {
        private ICollection<SportCategory> sportCategories;
        private ICollection<FacilityComment> facilityComments;

        public FacilityDetailedViewModel()
        {
            this.SportCategories = new HashSet<SportCategory>();
            this.FacilityComments = new HashSet<FacilityComment>();

        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Ието е задължително")]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        [StringLength(50, ErrorMessage = "{0} трябва да е поне {2} символа.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описанието е задължително")]
        [AllowHtml]
        [StringLength(2000, ErrorMessage = "{0} трябва да е поне {2} символа.", MinimumLength = 2)]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ!")]
        public string Description { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual AppUser Author { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        public string Image { get; set; }

        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        public virtual ICollection<FacilityComment> FacilityComments
        {
            get { return this.facilityComments; }
            set { this.facilityComments = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Facility, FacilityDetailedViewModel>();
        }
    }
}