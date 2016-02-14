namespace SportsBook.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class EditCommentViewModel : IMapTo<SportsBook.Data.Models.FacilityComment>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public int FacilityId { get; set; }

        public Facility Facility { get; set; }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }

        public AppUser Author { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<FacilityComment, EditCommentViewModel>();
        }
    }
}