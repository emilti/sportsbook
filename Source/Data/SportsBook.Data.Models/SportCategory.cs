namespace SportsBook.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SportCategory
    {
        private ICollection<Facility> facilities;

        public SportCategory()
        {
            this.Facilities = new HashSet<Facility>();
        }

        public int Id { get; set; }

        [MaxLength(25)]
        [MinLength(2)]
        public string Name { get; set; }

        [MaxLength(150)]
        [MinLength(2)]
        public string Description { get; set; }

        public virtual ICollection<Facility> Facilities
        {
            get { return this.facilities; }
            set { this.facilities = value; }
        }
    }
}