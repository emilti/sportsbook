namespace SportsBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class City
    {
        private ICollection<Facility> facilities;

        public City()
        {
            this.Facilities = new HashSet<Facility>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Facility> Facilities
        {
            get { return this.facilities; }
            set { this.facilities = value; }
        }
    }
}
