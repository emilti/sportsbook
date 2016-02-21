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

        public FacilityComment Add(int facilityId, string content, string authorId, string username, Facility commentedFacility, string userAvatar)
        {
            var newComment = new FacilityComment
            {
                FacilityId = facilityId,
                Content = content,
                AuthorName = username,
                AuthorId = authorId,
                CreatedOn = DateTime.UtcNow,
                CreatorAvatar = userAvatar
            };

            this.comments.Add(newComment);
            commentedFacility.FacilityComments.Add(newComment);
            this.comments.Save();

            return newComment;
        }

        public void Add(FacilityComment comment)
        {
            this.comments.Add(comment);
            this.comments.Save();
        }

        public IQueryable<FacilityComment> All()
        {
            return this.comments.All();
        }

        public void DeleteComment(FacilityComment facilityComment)
        {
            this.comments.Delete(facilityComment);
            this.comments.Save();
        }

        public FacilityComment GetById(int commentId)
        {
            FacilityComment foundComment = this.comments.GetById(commentId);
            return foundComment;
        }

        public void UpdateComment(int id, string newContent)
        {
            var comment = this.comments.GetById(id);
            comment.Content = newContent;
            this.comments.Save();
        }

        public void Dispose()
        {
            this.comments.Dispose();
        }
    }
}
