namespace SportsBook.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using ViewModels;

    [Authorize(Roles = "Admin")]
    public class AdminFacilityCommentsController : Controller
    {
        private readonly ICommentsService comments;

        public AdminFacilityCommentsController(ICommentsService commentsService)
        {
            this.comments = commentsService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult FacilityComments_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.comments.All()
               .To<FacilityCommentGridViewModel>()
               .ToDataSourceResult(request);
            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FacilityComments_Update([DataSourceRequest]DataSourceRequest request, FacilityCommentGridViewModel facilityComment)
        {
            if (this.ModelState.IsValid)
            {
                this.comments.UpdateComment(facilityComment.Id, facilityComment.Content);
            }

            var commentToDisplay =
             this.comments.All()
             .To<FacilityCommentGridViewModel>()
             .FirstOrDefault(x => x.Id == facilityComment.Id);

            return this.Json(new[] { commentToDisplay }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FacilityComments_Destroy([DataSourceRequest]DataSourceRequest request, FacilityComment facilityComment)
        {
            FacilityComment foundComment = this.comments.GetById(facilityComment.Id);
            this.comments.DeleteComment(foundComment);
            return this.Json(new[] { facilityComment }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            this.comments.Dispose();
            base.Dispose(disposing);
        }
    }
}
