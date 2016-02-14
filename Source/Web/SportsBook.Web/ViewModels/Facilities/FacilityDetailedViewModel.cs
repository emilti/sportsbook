namespace SportsBook.Web.ViewModels.Facilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using SportsBook.Web.Infrastructure.Mapping;

    public class FacilityDetailedViewModel : IMapFrom<SportsBook.Data.Models.Facility>, IHaveCustomMappings
    {
        private ICollection<SportCategory> sportCategories;
        private ICollection<FacilityComment> facilityComments;

        public FacilityDetailedViewModel()
        {
            this.SportCategories = new HashSet<SportCategory>();
            this.FacilityComments = new HashSet<FacilityComment>();

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

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

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Facility, FacilityDetailedViewModel>();
        }
    }
}