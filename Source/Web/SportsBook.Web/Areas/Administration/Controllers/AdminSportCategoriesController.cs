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
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using SportsBook.Data.Models;
    using SportsBook.Services.Data.Contracts;
    using ViewModels;

    [Authorize(Roles = "Admin")]
    public class AdminSportCategoriesController : Controller
    {
        private readonly ISportCategoriesService sportCategories;

        public AdminSportCategoriesController(ISportCategoriesService sportCategoriesService)
        {
            this.sportCategories = sportCategoriesService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult SportCategories_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.sportCategories.All()
               .To<SportCategoryGridViewModel>()
               .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SportCategories_Create([DataSourceRequest]DataSourceRequest request, SportCategoryGridViewModel sportCategory)
        {
            var newId = 0;
            if (this.ModelState.IsValid)
            {
                var entity = new SportCategory
                {
                    Name = sportCategory.Name,
                    Description = sportCategory.Description
                };

                this.sportCategories.Add(entity);
                this.sportCategories.Save();
                newId = entity.Id;
            }

            var postToDisplay =
                this.sportCategories.All()
                .To<SportCategoryGridViewModel>()
                .FirstOrDefault(x => x.Id == newId);

            return this.Json(new[] { sportCategory }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SportCategories_Update([DataSourceRequest]DataSourceRequest request, SportCategoryGridViewModel sportCategory)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.sportCategories.GetById(sportCategory.Id);
                entity.Name = sportCategory.Name;
                entity.Description = sportCategory.Description;
                this.sportCategories.Save();
            }

            var sportCategoryToDisplay =
             this.sportCategories.All()
             .To<SportCategoryGridViewModel>()
             .FirstOrDefault(x => x.Id == sportCategory.Id);

            return this.Json(new[] { sportCategoryToDisplay }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SportCategories_Destroy([DataSourceRequest]DataSourceRequest request, SportCategory sportCategory)
        {
            this.sportCategories.Remove(sportCategory);
            this.sportCategories.Save();

            return this.Json(new[] { sportCategory }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            this.sportCategories.Dispose();
            base.Dispose(disposing);
        }
    }
}
