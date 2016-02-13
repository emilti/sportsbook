namespace SportsBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels.Comments;
    using Data.Models;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService comments;
        private readonly IFacilitiesService facilities;

        public CommentsController(ICommentsService commentsService, IFacilitiesService facilitiesService)
        {
            this.comments = commentsService;
            this.facilities = facilitiesService;
        }

        [HttpPost]
        [Authorize(Roles = "Regular,Admin")]
        public ActionResult WriteComment(int id, RequestWriteComment model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ModelState.AddModelError("", "Message title is invlaid.");
                return this.View(model);
            }

            Facility commentedFacility = this.facilities.GetFacilityDetails(id);
            var comment = this.comments.Add(id, model.Content, this.User.Identity.GetUserId(), commentedFacility);
            return this.RedirectToAction("FacilityDetails", "Facilities", new { id = id });
        }
    }
}