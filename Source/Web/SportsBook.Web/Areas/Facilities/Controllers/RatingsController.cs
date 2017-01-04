namespace SportsBook.Web.Areas.Facilities.Controllers
{
    using SportsBook.Data.Models;
    using SportsBook.Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using SportsBook.Web.Infrastructure.Mapping;
    using SportsBook.Web.Areas.Facilities.ViewModels.Ratings;

    public class RatingsController : Controller
    {
        private readonly IRatingsService ratings;
        private readonly IFacilitiesService facilities;
        private readonly IUsersService users;

        public RatingsController(IRatingsService ratingsService, IFacilitiesService facilitiesService, IUsersService usersService)
        {
            this.ratings = ratingsService;
            this.facilities = facilitiesService;
            this.users = usersService;
        }

        [HttpGet]
        public ActionResult CheckFacilityRating(int id)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        // [ValidateAntiForgeryToken]
        public void AddRating(int facilityId, int ratingValue)
        {
            AppUser user = this.users.GetUserDetails(this.User.Identity.GetUserId());
            Facility facility = this.facilities.GetFacilityDetails(facilityId);
            FacilityRating facilityRating = facility.FaciltityRatings.FirstOrDefault(x => x.AuthorId == user.Id);
            if (facilityRating == null)
            {
                this.ratings.Add(facilityId, user.Id, ratingValue);

            }
            else
            {
                this.ratings.UpdateRating(facilityRating.Id, ratingValue);
            }

            int ratingsSum = facility.FaciltityRatings.Sum(x => x.RatingValue);
            facility.Rating = (decimal)ratingsSum / (decimal)facility.FaciltityRatings.Count;
            facilities.UpdateFacility(facility.Id, facility);
        }

        [HttpGet]
        // [Authorize]
        public ActionResult GetUserFacilityRating(int id)
        {
            AppUser user = this.users.GetUserDetails(this.User.Identity.GetUserId());
            Facility facility = this.facilities.GetFacilityDetails(id);
            ViewBag.facilityId = facility.Id;
            if (user != null)
            {
                FacilityRating facilityRating = facility.FaciltityRatings.FirstOrDefault(x => x.AuthorId == user.Id);
                var facilityRatingForView = AutoMapperConfig.Configuration.CreateMapper().Map<FacilityRatingViewModel>(facilityRating);
              
                return this.PartialView("_FacilityRating", facilityRatingForView);
            }
            else
            {
                return this.PartialView("_FacilityRating", null);
            }
        }
    }
}