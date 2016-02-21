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
    using SportsBook.Data;
    using SportsBook.Data.Models;
    using Services.Data.Contracts;

    public class AdminFacilitiesController : Controller
    {
        private readonly IFacilitiesService facilities;

        public AdminFacilitiesController(IFacilitiesService facilitiesService)
        {
            this.facilities = facilitiesService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Facilities_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.facilities.All().ToDataSourceResult(request, facility => new
            {
                Id = facility.Id,
                Name = facility.Name,
                Description = facility.Description,
                Image = facility.Image,
                CreatedOn = facility.CreatedOn,
                ModifiedOn = facility.ModifiedOn,
                IsDeleted = facility.IsDeleted,
                DeletedOn = facility.DeletedOn
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Facilities_Create([DataSourceRequest]DataSourceRequest request, Facility facility)
        {
            if (ModelState.IsValid)
            {
                var entity = new Facility
                {
                    Name = facility.Name,
                    Description = facility.Description,
                    Image = facility.Image,
                    CreatedOn = facility.CreatedOn,
                    ModifiedOn = facility.ModifiedOn,
                    IsDeleted = facility.IsDeleted,
                    DeletedOn = facility.DeletedOn
                };

                this.facilities.Add(entity);
                this.facilities.Save();
                facility.Id = entity.Id;
            }

            return Json(new[] { facility }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Facilities_Update([DataSourceRequest]DataSourceRequest request, Facility facility)
        {
            if (ModelState.IsValid)
            {
                var entity = new Facility
                {
                    Id = facility.Id,
                    Name = facility.Name,
                    Description = facility.Description,
                    Image = facility.Image,
                    CreatedOn = facility.CreatedOn,
                    ModifiedOn = facility.ModifiedOn,
                    IsDeleted = facility.IsDeleted,
                    DeletedOn = facility.DeletedOn
                };

                this.facilities.UpdateFacility(facility.Id, entity);
                this.facilities.Save();
            }

            return Json(new[] { facility }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Facilities_Destroy([DataSourceRequest]DataSourceRequest request, Facility facility)
        {
            this.facilities.Remove(facility);
            this.facilities.Save();

            return Json(new[] { facility }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            this.facilities.Dispose();
            base.Dispose(disposing);
        }
    }
}
