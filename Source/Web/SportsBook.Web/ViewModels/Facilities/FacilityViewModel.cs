namespace SportsBook.Web.ViewModels.Facilities
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using SportsBook.Data.Models;
    using SportsBook.Web.Infrastructure.Mapping;

    public class FacilityViewModel : IMapFrom<SportsBook.Data.Models.Facility>, IHaveCustomMappings
    {
        private ICollection<SportCategory> sportCategories;

        public FacilityViewModel()
        {
            this.SportCategories = new HashSet<SportCategory>();
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

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Facility, FacilityViewModel>();
        }
    }
}