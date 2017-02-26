namespace SportsBook.Web.ViewModels.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using SportsBook.Data.Models;
    using SportsBook.Web.Infrastructure.Mapping;

    public class AppUserViewModel : IMapFrom<AppUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Avatar { get; set; }

        public string NumberofFavoritesFacilities { get; set; }

        public string NumberofSubmittedFacilities { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<AppUser, AppUserViewModel>()
                .ForMember(x => x.NumberofFavoritesFacilities, opt => opt.MapFrom(x => x.FavoriteFacilities.Count));           
            configuration.CreateMap<AppUser, AppUserViewModel>()
               .ForMember(x => x.NumberofSubmittedFacilities, opt => opt.MapFrom(x => x.SubmittedFacilities.Count));           
        }
    }
}