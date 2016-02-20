namespace SportsBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Common.Models;

    public class FacilityComment : BaseModel<int>
    {
        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Content { get; set; }

        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }

        public virtual AppUser Author { get; set; }

        public string CreatorAvatar { get; set; }
    }
}
