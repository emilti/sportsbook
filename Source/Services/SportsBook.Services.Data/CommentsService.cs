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
        private readonly IRepository<FacilityComment> comments;

        public CommentsService(
            IRepository<FacilityComment> commentsRepo)
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
                CreatedOn = DateTime.UtcNow
            };

            this.comments.Add(newComment);
            commentedFacility.FacilityComments.Add(newComment);
            this.comments.SaveChanges();

            return newComment;
        }

        public void Add(FacilityComment comment)
        {
            this.comments.Add(comment);
            this.comments.SaveChanges();
        }

        public IQueryable<FacilityComment> All()
        {
            return this.comments.All();
        }

        public void DeleteComment(FacilityComment facilityComment)
        {
            this.comments.Delete(facilityComment);
            this.comments.SaveChanges();
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
            this.comments.SaveChanges();
        }

        public void Dispose()
        {
            this.comments.Dispose();
        }
    }
}
