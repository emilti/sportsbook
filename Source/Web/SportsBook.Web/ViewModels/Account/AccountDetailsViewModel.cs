namespace SportsBook.Web.ViewModels.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class AccountDetailsViewModel : IMapFrom<AppUser>, IHaveCustomMappings
    {
        private ICollection<SportCategory> sportCategories;
        private ICollection<Facility> facilities;

        public AccountDetailsViewModel()
        {
            this.SportCategories = new HashSet<SportCategory>();
            this.Facilities = new HashSet<Facility>();
        }

        public string Email { get; set; }

        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Avatar { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        public virtual ICollection<Facility> Facilities
        {
            get { return this.facilities; }
            set { this.facilities = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<SportsBook.Data.Models.AppUser, AccountDetailsViewModel>();
        }
    }
}