namespace SportsBook.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using SportsBook.Data.Models;
    using SportsBook.Web.Infrastructure.Mapping;

    public class SportCategoryGridViewModel : IMapFrom<SportCategory>, IMapTo<SportCategory>
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Description { get; set; }
    }
}