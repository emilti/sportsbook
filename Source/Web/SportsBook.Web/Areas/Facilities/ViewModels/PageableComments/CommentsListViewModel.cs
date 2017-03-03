using SportsBook.Web.Areas.Facilities.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsBook.Web.Areas.Facilities.ViewModels.PageableComments
{
    public class CommentsListViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}