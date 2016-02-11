namespace SportsBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Facility
    {
        private ICollection<SportCategory> sportCategories;

        private ICollection<FacilityComment> facilityComments;

        public Facility()
        {
            this.SportCategories = new HashSet<SportCategory>();
            this.FacilityComments = new HashSet<FacilityComment>();
        }

        public int Id { get; set; }

        [MaxLength(25)]
        [MinLength(2)]
        public string Name { get; set; }

        [MaxLength(150)]
        [MinLength(2)]
        public string Description { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

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
    }
}
