﻿namespace SportsBook.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Common.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {
        private ICollection<SportCategory> sportCategories;
        private ICollection<Facility> favoriteFfacilities;
        private ICollection<Facility> submittedFacilities;       

        public AppUser()
        {
            this.SportCategories = new HashSet<SportCategory>();
            this.FavoriteFacilities = new HashSet<Facility>();
            this.SubmittedFacilities = new HashSet<Facility>();           
        }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Avatar { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"[a-zA-Z_0-9]+", ErrorMessage = "Only letters, numbers and _ allowed")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"[a-zA-Z_0-9]+", ErrorMessage = "Only letters, numbers and _ allowed")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        public virtual ICollection<Facility> FavoriteFacilities
        {
            get { return this.favoriteFfacilities; }
            set { this.favoriteFfacilities = value; }
        }

        public virtual ICollection<Facility> SubmittedFacilities
        {
            get { return this.submittedFacilities; }
            set { this.submittedFacilities = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
