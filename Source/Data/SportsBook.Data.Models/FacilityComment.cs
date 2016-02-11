namespace SportsBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FacilityComment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int FacilityId { get; set; }

        public Facility Facility { get; set; }
    }
}
