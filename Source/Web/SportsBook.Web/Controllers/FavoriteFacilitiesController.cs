using Microsoft.AspNet.Identity;
using SportsBook.Data.Models;
using SportsBook.Services.Data.Contracts;
using SportsBook.Web.ViewModels.Facilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsBook.Web.Controllers
{
    public class FavoriteFacilitiesController : BaseController
    {
        private readonly IUsersService users;

        private readonly IFacilitiesService facilities;

        public FavoriteFacilitiesController(IUsersService usersService, IFacilitiesService facilitiesService)
        {
            this.users = usersService;
            this.facilities = facilitiesService;
        }

        public ActionResult CheckFacilityInFavourites(int id)
        {
            List<Facility> foundFacilities = new List<Facility>();
            List<FacilityViewModel> foundFacilitiesToView = new List<FacilityViewModel>();
            AppUser currentUser = this.users.GetUserDetails(this.User.Identity.GetUserId());
            if (currentUser == null)
            {
                Facility curentFacility = this.facilities.GetFacilityDetails(id);
                ViewBag.className = "favorites-button";
                return this.PartialView("FacilityInFavourites", curentFacility);
            }

            Facility checkedFacilityForCurrentUser = currentUser.FavoriteFacilities.FirstOrDefault(a => a.Id == id);
            if (checkedFacilityForCurrentUser == null)
            {
                Facility curentFacility = this.facilities.GetFacilityDetails(id);
                ViewBag.className = "favorites-button";
                return this.PartialView("FacilityInFavourites", curentFacility);
            }
            else
            {
                ViewBag.className = "remove-from-favorites-button";
                return this.PartialView("FacilityInFavourites", checkedFacilityForCurrentUser);
            }

            // return this.PartialView(foundFacilitiesToView);
        }

        public void AddToFavorites(int id)
        {
            Facility foundFacility = this.facilities.GetFacilityDetails(id);
            var userId = this.User.Identity.GetUserId();
            AppUser currentUser = this.users.GetUserDetails(userId);
            currentUser.FavoriteFacilities.Add(foundFacility);
            foundFacility.UsersLiked.Add(currentUser);
            this.facilities.UpdateFacility();
            this.users.UpdateUser(currentUser);
        }

        public void RemoveFromFavorites(int id)
        {
            Facility foundFacility = this.facilities.GetFacilityDetails(id);
            var userId = this.User.Identity.GetUserId();
            AppUser currentUser = this.users.GetUserDetails(userId);
            currentUser.FavoriteFacilities.Remove(foundFacility);
            foundFacility.UsersLiked.Remove(currentUser);
            this.facilities.UpdateFacility();
            this.users.UpdateUser(currentUser);
           // return this.RedirectToAction("Index", "Home");
        }
    }
}