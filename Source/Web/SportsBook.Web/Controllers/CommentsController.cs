namespace SportsBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels.Comments;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService comments;

        public CommentsController(ICommentsService commentsService)
        {
            this.comments = commentsService;
        }

        [HttpPost]
        [Authorize(Roles = "Regular,Admin")]
        public ActionResult WriteMessage(RequestWriteComment model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ModelState.AddModelError("", "Message title is invlaid.");
                return this.View(model);
            }

            var message = this.comments.Add(model.Title, model.Content, User.Identity.GetUserId());
            return this.RedirectToAction("Index", "Home");
        }
    }
}