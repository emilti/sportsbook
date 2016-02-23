namespace SportsBook.Web.Areas.Events.ViewModels.EventsModels
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

    public class EventDetailedViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        private ICollection<SportCategory> sportCategories;
        private ICollection<EventComment> eventComments;

        public EventDetailedViewModel()
        {
            this.SportCategories = new HashSet<SportCategory>();
            this.EventComments = new HashSet<EventComment>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(50, ErrorMessage = "{0}то трябва да е поне {2} символа.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описанието е задължително")]
        [AllowHtml]
        [StringLength(2000, ErrorMessage = "{0}то трябва да е поне {2} символа.", MinimumLength = 2)]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        public string Description { get; set; }

        public DateTime Start { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual AppUser Author { get; set; }

        [Required(ErrorMessage = "Снимката е задължителна")]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Невалиден символ")]
        public string Image { get; set; }

        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        public virtual ICollection<EventComment> EventComments
        {
            get { return this.eventComments; }
            set { this.eventComments = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Facility, EventDetailedViewModel>();
        }
    }
}