﻿namespace SportsBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels.Comments;
    using Infrastructure.Mapping;

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
            AppUser user = this.users.GetUserDetails(this.User.Identity.GetUserId());
            string username = user.UserName;
            var comment = this.comments.Add(id, model.Content, this.User.Identity.GetUserId(), username, commentedFacility);
            return this.RedirectToAction("FacilityDetails", "Facilities", new { id = id });
        }

        [HttpGet]
        [Authorize(Roles = "Regular,Admin")]
        public ActionResult EditComment(int id)
        {
            FacilityComment foundComment = this.comments.GetById(id);
            var foundCommentForView = AutoMapperConfig.Configuration.CreateMapper().Map<EditCommentViewModel>(foundComment);
            return this.View(foundCommentForView);
        }


        [HttpPost]
        [Authorize(Roles = "Regular,Admin")]
        public ActionResult EditComment(int id, EditCommentResponseModel model)
        {
            if (!this.ModelState.IsValid)
            {

            }

            FacilityComment foundComment = this.comments.GetById(id);
            this.comments.UpdateComment(id, model.Content);
            return this.RedirectToAction("FacilityDetails", "Facilities", new { id = foundComment.FacilityId });
        }
    }
}