namespace SportsBook.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;
    using Common.Models;

    public class Facility : BaseModel<int>
    {
        private ICollection<SportCategory> sportCategories;

        private ICollection<FacilityComment> facilityComments;

        private ICollection<AppUser> usersLiked;

        private ICollection<FacilityRating> facilityRatings;

        public Facility()
        {
            this.SportCategories = new HashSet<SportCategory>();
            this.FacilityComments = new HashSet<FacilityComment>();
            this.UsersLiked = new HashSet<AppUser>();
            this.FaciltityRatings = new HashSet<FacilityRating>();
        }

        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [AllowHtml]
        [StringLength(2000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        public string Description { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual AppUser Author { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        public string Image { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public decimal? Rating { get; set; }

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

        [InverseProperty("FavoriteFacilities")]
        public virtual ICollection<AppUser> UsersLiked
        {
            get { return this.usersLiked; }
            set { this.usersLiked = value; }
        }

        public virtual ICollection<FacilityRating> FaciltityRatings
        {
            get { return this.facilityRatings; }
            set { this.facilityRatings = value; }
        }
    }
}
