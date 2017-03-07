namespace SportsBook.Web.Areas.Facilities.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels.Comments;
    using Web.Controllers;
    using SportsBook.Web.Areas.Facilities.ViewModels.PageableComments;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService comments;
        private readonly IFacilitiesService facilities;
        private readonly IUsersService users;

        public CommentsController(ICommentsService commentsService, IFacilitiesService facilitiesService, IUsersService usersService)
        {
            this.comments = commentsService;
            this.facilities = facilitiesService;
            this.users = usersService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddComment(int id)
        {
            this.ViewBag.facilityId = id;
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public void AddComment(int id, string content)
        {
            if (this.ModelState.IsValid)
            {
                // FacilityComment mappedComment = AutoMapperConfig.Configuration.CreateMapper().Map<FacilityComment>(model);
                Facility commentedFacility = this.facilities.GetFacilityDetails(id);
                AppUser user = this.users.GetUserDetails(this.User.Identity.GetUserId());
                string username = user.UserName;
                var comment = this.comments.Add(id, content, this.User.Identity.GetUserId(), username, commentedFacility, user.Avatar);
                // return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = id, area = "Facilities" });
            }

            // return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = id, area = "Facilities" });
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditComment(int id)
        {
            FacilityComment foundComment = this.comments.GetById(id);
            var foundCommentForView = AutoMapperConfig.Configuration.CreateMapper().Map<CommentViewModel>(foundComment);
            return this.View(foundCommentForView);
        }

        public ActionResult GetSelectedPageComments(int id, int pageNumber)
        {
            var facilityComments = this.facilities.GetLatestFacilityComments(id);
            List<CommentViewModel> commentsViewModel = AutoMapperConfig.Configuration.CreateMapper().Map<List<CommentViewModel>>(facilityComments);
            CommentsListViewModel commentsListViewModel = new CommentsListViewModel();
            decimal totalCommentsCount = (decimal)commentsViewModel.Count();
            commentsListViewModel.TotalPages = (int)Math.Ceiling((totalCommentsCount / (decimal)SportsBook.Data.Common.Constants.Constants.COUNT_OF_COMMENTS_PER_PAGE));
            commentsListViewModel.CurrentPage = pageNumber;
            commentsListViewModel.Comments = commentsViewModel.Skip((int)(pageNumber - 1) * 5).Take(5).ToList();           
            return this.PartialView("_PageableCommentsPartial", commentsListViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditComment(int id, CommentViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                FacilityComment foundComment = this.comments.GetById(id);
                this.comments.UpdateComment(id, model.Content);
                return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = foundComment.FacilityId, area = "Facilities" });
            }

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int id)
        {
            FacilityComment foundComment = this.comments.GetById(id);
            this.comments.DeleteComment(foundComment);
            return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = foundComment.FacilityId, area = "Facilities" });
        }
    }
}