namespace SportsBook.Services.Data.Contracts
{
    using SportsBook.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRatingsService
    {
        IQueryable<FacilityRating> All();

        FacilityRating Add(int facilityId, string authorId, int ratingValue);

        FacilityRating GetById(int ratingId);

        void UpdateRating(int id, int ratingValue);

        void DeleteRating(FacilityRating facilityRating);

        void Dispose();
    }
}
