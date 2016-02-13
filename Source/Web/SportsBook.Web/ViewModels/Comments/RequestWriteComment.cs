namespace SportsBook.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class RequestWriteComment
    {
        public int AuthorId { get; set; }

        public int FacilityId { get; set; }

        public string Content { get; set; }
    }
}