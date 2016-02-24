namespace SportsBook.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using SportsBook.Data.Models;
    using SportsBook.Services.Data.Contracts;
    using SportsBook.Web.Areas.Administration.ViewModels;
    using SportsBook.Web.Infrastructure.Mapping;

    [Authorize(Roles = "Admin")]
    public class AdminEventsController : Controller
    {
        private readonly IEventsService events;

        public AdminEventsController(IEventsService eventsService)
        {
            this.events = eventsService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Events_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.events.All()
                 .To<EventGridViewModel>()
                 .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Events_Update([DataSourceRequest]DataSourceRequest request, EventGridViewModel sportEvent)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.events.GetEventDetails(sportEvent.Id);
                entity.Name = sportEvent.Name;
                entity.Description = sportEvent.Description;
                entity.Image = sportEvent.Image;
                this.events.Save();
            }

            var eventToDisplay =
             this.events.All()
             .To<EventGridViewModel>()
             .FirstOrDefault(x => x.Id == sportEvent.Id);

            return Json(new[] { eventToDisplay }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Events_Destroy([DataSourceRequest]DataSourceRequest request, Event sportEvent)
        {
            this.events.Remove(sportEvent);
            this.events.Save();

            return Json(new[] { sportEvent }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            this.events.Dispose();
            base.Dispose(disposing);
        }
    }
}
