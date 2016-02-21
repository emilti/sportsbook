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

    public class AdminFacilitiesController : Controller
    {
        private SportsBookDbContext db = new SportsBookDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Facilities_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Facility> facilities = db.Facilities;
            DataSourceResult result = facilities.ToDataSourceResult(request, facility => new {
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

                db.Facilities.Add(entity);
                db.SaveChanges();
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

                db.Facilities.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { facility }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Facilities_Destroy([DataSourceRequest]DataSourceRequest request, Facility facility)
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

                db.Facilities.Attach(entity);
                db.Facilities.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { facility }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
