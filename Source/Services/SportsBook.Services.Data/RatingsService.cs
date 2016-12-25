using SportsBook.Data.Common;
using SportsBook.Data.Models;
using SportsBook.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBook.Services.Data
{
    public class RatingsService : IRatingsService
    {
        private readonly IRepository<FacilityRating> ratings;

        public RatingsService(
            IRepository<FacilityRating> ratingsRepo)
        {
            this.ratings = ratingsRepo;
        }

        public FacilityRating Add(int facilityId, string authorId, int ratingValue)
        {
            var newFacilityRating = new FacilityRating
            {
                FacilityId = facilityId,
                AuthorId = authorId,   
                RatingValue = ratingValue
            };

            this.ratings.Add(newFacilityRating);            
            this.ratings.SaveChanges();

            return newFacilityRating;
        }

        public void Add(FacilityRating facilityRating)
        {
            this.ratings.Add(facilityRating);
            this.ratings.SaveChanges();
        }

        public IQueryable<FacilityRating> All()
        {
            return this.ratings.All();
        }

        public void DeleteRating(FacilityRating facilityRating)
        {
            this.ratings.Delete(facilityRating);
            this.ratings.SaveChanges();
        }

        public FacilityRating GetById(int ratingId)
        {
            FacilityRating  foundRating= this.ratings.GetById(ratingId);
            return foundRating;
        }      

        public void UpdateRating(int id, int ratingValue)
        {
            var rating = this.ratings.GetById(id);
            rating.RatingValue = ratingValue;
            this.ratings.SaveChanges();
        }

        public void Dispose()
        {
            this.ratings.Dispose();
        }
    }
}
