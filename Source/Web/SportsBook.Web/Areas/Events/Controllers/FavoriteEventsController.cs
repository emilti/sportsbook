namespace SportsBook.Web.Areas.Events.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Events.ViewModels.EventsModels;
    using Microsoft.AspNet.Identity;
    using SportsBook.Data.Models;
    using SportsBook.Services.Data.Contracts;
    using Web.Controllers;

    public class FavoriteEventsController : BaseController
    {
        private readonly IUsersService users;

        private readonly IEventsService events;

        public FavoriteEventsController(IUsersService usersService, IEventsService eventsService)
        {
            this.users = usersService;
            this.events = eventsService;
        }

        public ActionResult CheckEventInFavourites(int id)
        {
            List<Event> foundEvent = new List<Event>();
            List<EventViewModel> foundEventsToView = new List<EventViewModel>();
            AppUser currentUser = this.users.GetUserDetails(this.User.Identity.GetUserId());
            if (currentUser == null)
            {
                Event curentEvent = this.events.GetEventDetails(id);
                this.ViewBag.className = "favorites-button";
                return this.PartialView("_EventInFavourites", curentEvent);
            }

            Event checkedEventForCurrentUser = currentUser.FavoriteEvents.FirstOrDefault(a => a.Id == id);
            if (checkedEventForCurrentUser == null)
            {
                Event curentEvent = this.events.GetEventDetails(id);
                this.ViewBag.className = "favorites-button";
                return this.PartialView("_EventInFavourites", curentEvent);
            }
            else
            {
                this.ViewBag.className = "remove-from-favorites-button";
                return this.PartialView("_EventInFavourites", checkedEventForCurrentUser);
            }

            // return this.PartialView(foundFacilitiesToView);
        }

        [Authorize]
        public void AddToFavorites(int id)
        {
            Event foundEvent = this.events.GetEventDetails(id);
            var userId = this.User.Identity.GetUserId();
            AppUser currentUser = this.users.GetUserDetails(userId);
            currentUser.FavoriteEvents.Add(foundEvent);
            foundEvent.UsersLiked.Add(currentUser);
            this.events.UpdateEvent(id, foundEvent);
            this.users.UpdateUser(currentUser);
        }

        [Authorize]
        public void RemoveFromFavorites(int id)
        {
            Event foundEvent = this.events.GetEventDetails(id);
            var userId = this.User.Identity.GetUserId();
            AppUser currentUser = this.users.GetUserDetails(userId);
            currentUser.FavoriteEvents.Remove(foundEvent);
            foundEvent.UsersLiked.Remove(currentUser);
            this.events.UpdateEvent(id, foundEvent);
            this.users.UpdateUser(currentUser);
           // return this.RedirectToAction("Index", "Home");
        }
    }
}