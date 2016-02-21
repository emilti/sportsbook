namespace SportsBook.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;

    public class AppUserGridViewModel : IMapFrom<AppUser>, IMapTo<AppUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

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

        [Required]
        [AllowHtml]
        [RegularExpression(@"[a-zA-Z_0-9]+", ErrorMessage = "Only letters, numbers and _ allowed")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}