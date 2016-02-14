namespace SportsBook.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsBook.Data.Models;

    public interface ICommentsService
    {
        FacilityComment Add(int facilityId, string content, string authorId, string username, Facility commentedFacility);

        FacilityComment GetById(int commentId);

        void UpdateComment(int id, string newContent);

        void DeleteComment(FacilityComment id);
    }
}
