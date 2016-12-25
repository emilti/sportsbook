using SportsBook.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsBook.Data.Models
{
    public class FacilityRating : BaseModel<int>
    {

        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }       

        public string AuthorId { get; set; }

        public virtual AppUser Author { get; set; }

        public int RatingValue { get; set; }
    }
}
