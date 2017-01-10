using AutoMapper;
using SportsBook.Data.Models;
using SportsBook.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsBook.Web.Areas.Facilities.ViewModels.Ratings
{
    public class FacilityRatingViewModel : IMapTo<SportsBook.Data.Models.FacilityRating>, IHaveCustomMappings
    {
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }

        public string AuthorId { get; set; }

        public virtual AppUser Author { get; set; }

        public int RatingValue { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<FacilityRating, FacilityRatingViewModel>();
        }
    }
}