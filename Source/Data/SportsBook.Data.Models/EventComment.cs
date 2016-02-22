namespace SportsBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using SportsBook.Data.Common.Models;

    public class EventComment : BaseModel<int>
    {
        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Content { get; set; }

        public int EventId { get; set; }

        public virtual Event Facility { get; set; }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }

        public virtual AppUser Author { get; set; }

        public string CreatorAvatar { get; set; }
    }
}