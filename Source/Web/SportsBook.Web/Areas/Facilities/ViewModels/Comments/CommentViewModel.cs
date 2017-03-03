namespace SportsBook.Web.Areas.Facilities.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
using System;

    public class CommentViewModel : IMapTo<SportsBook.Data.Models.FacilityComment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }

        public virtual AppUser Author { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<FacilityComment, CommentViewModel>();
        }
    }
}