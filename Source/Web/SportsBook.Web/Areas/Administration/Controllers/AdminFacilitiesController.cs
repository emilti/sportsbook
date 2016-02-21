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
    using Data;
    using Data.Models;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels;

    [Authorize(Roles = "Admin")]
    public class AdminFacilitiesController : Controller
    {
        private readonly IFacilitiesService facilities;

        public AdminFacilitiesController(IFacilitiesService facilitiesService)
        {
            this.facilities = facilitiesService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Facilities_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.facilities.All()
               .To<FacilityGridViewModel>()
               .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Facilities_Create([DataSourceRequest]DataSourceRequest request, FacilityGridViewModel facility)
        {
            var newId = 0;
            if (this.ModelState.IsValid)
            {
                var entity = new Facility
                {
                    Name = facility.Name,
                    Description = facility.Description,
                    AuthorId = this.User.Identity.GetUserId(),
                    Image = facility.Image,
                    CityId = 1
                };


                this.facilities.Add(entity);
                this.facilities.Save();
                newId = entity.Id;
            }

            var facilityToDisplay =
                this.facilities.All()
                .To<FacilityGridViewModel>()
                .FirstOrDefault(x => x.Id == newId);
            return this.Json(new[] { facilityToDisplay }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Facilities_Update([DataSourceRequest]DataSourceRequest request, FacilityGridViewModel facility)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.facilities.GetFacilityDetails(facility.Id);
                entity.Name = facility.Name;
                entity.Description = facility.Description;
                entity.Image = facility.Image;
                this.facilities.Save();
            }

            var postToDisplay =
             this.facilities.All()
             .To<FacilityGridViewModel>()
             .FirstOrDefault(x => x.Id == facility.Id);

            return this.Json(new[] { facility }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Facilities_Destroy([DataSourceRequest]DataSourceRequest request, Facility facility)
        {
            this.facilities.Remove(facility);
            this.facilities.Save();

            return this.Json(new[] { facility }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            this.facilities.Dispose();
            base.Dispose(disposing);
        }
    }
}
