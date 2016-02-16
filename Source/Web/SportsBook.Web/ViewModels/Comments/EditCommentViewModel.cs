namespace SportsBook.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class EditCommentViewModel : IMapTo<SportsBook.Data.Models.FacilityComment>, IHaveCustomMappings
    {
        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Content { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<FacilityComment, EditCommentViewModel>();
        }
    }
}