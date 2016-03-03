namespace SportsBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Common.Models;

    public class Event : BaseModel<int>
    {
        private ICollection<SportCategory> sportCategories;

        private ICollection<EventComment> eventComments;

        private ICollection<AppUser> usersLiked;

        public Event()
        {
            this.SportCategories = new HashSet<SportCategory>();
            this.FacilityComments = new HashSet<EventComment>();
            this.UsersLiked = new HashSet<AppUser>();
        }

        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [AllowHtml]
        [StringLength(2000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        public string Description { get; set; }

        public DateTime Start { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual AppUser Author { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        public string Image { get; set; }

        public virtual ICollection<SportCategory> SportCategories
        {
            get { return this.sportCategories; }
            set { this.sportCategories = value; }
        }

        public virtual ICollection<EventComment> FacilityComments
        {
            get { return this.eventComments; }
            set { this.eventComments = value; }
        }

        [InverseProperty("FavoriteEvents")]
        public virtual ICollection<AppUser> UsersLiked
        {
            get { return this.usersLiked; }
            set { this.usersLiked = value; }
        }
    }
}
