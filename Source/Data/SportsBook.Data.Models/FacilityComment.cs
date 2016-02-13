namespace SportsBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;

    public class FacilityComment : BaseModel<int>
    {
        [Required]
        public string Content { get; set; }

        public int FacilityId { get; set; }

        public Facility Facility { get; set; }

        public string AuthorId { get; set; }

        public AppUser Author { get; set; }
    }
}
