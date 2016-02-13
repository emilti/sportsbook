namespace SportsBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsBook.Data.Common;
    using SportsBook.Data.Models;
    using SportsBook.Services.Data.Contracts;

    public class CommentsService : ICommentsService
    {
        private readonly IDbRepository<FacilityComment> comments;


        public CommentsService(
            IDbRepository<FacilityComment> commentsRepo)
        {
            this.comments = commentsRepo;
        }

        public FacilityComment Add(int facilityId, string content, string authorId, string username, Facility commentedFacility)
        {
            var newComment = new FacilityComment
            {
                FacilityId = facilityId,
                Content = content,
                AuthorName = username,
                AuthorId = authorId,
                CreatedOn = DateTime.UtcNow,
            };

            this.comments.Add(newComment);
            commentedFacility.FacilityComments.Add(newComment);
            this.comments.Save();

            return newComment;
        }
    }
}
