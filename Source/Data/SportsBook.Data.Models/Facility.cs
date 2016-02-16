namespace SportsBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;

    public class Facility : BaseModel<int>
    {
        private ICollection<SportCategory> sportCategories;

        private ICollection<FacilityComment> facilityComments;

        private ICollection<AppUser> usersLiked;

        public Facility()
        {
            this.SportCategories = new HashSet<SportCategory>();
            this.FacilityComments = new HashSet<FacilityComment>();
            this.UsersLiked = new HashSet<AppUser>();
        }

        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }

        [MaxLength(2000)]
        [MinLength(2)]
        public string Description { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
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

        public virtual ICollection<AppUser> UsersLiked
        {
            get { return this.usersLiked; }
            set { this.usersLiked = value; }
        }
    }
}
