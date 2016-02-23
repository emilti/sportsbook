using Moq;
using NUnit.Framework;
using SportsBook.Data.Models;
using SportsBook.Services.Data.Contracts;
using SportsBook.Web.Areas.Facilities.Controllers;
using SportsBook.Web.Areas.Facilities.ViewModels.Facilities;
using SportsBook.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace SportsBook.Web.Controllers.Tests
{
    [TestFixture]
    public class FacilityPrivateControllerTests
    {
        Mock<IFacilitiesService> facilitiesServiceMock = new Mock<IFacilitiesService>();
        Mock<IUsersService> usersServiceMock = new Mock<IUsersService>();
        Mock<ICitiesService> citiesServiceMock = new Mock<ICitiesService>();
        Mock<ISportCategoriesService> sportCategoriesServiceMock = new Mock<ISportCategoriesService>();

        public AutoMapperConfig GetAutoMapper()
        {
            AutoMapperConfig autoMapperConfig = new AutoMapperConfig();
            return autoMapperConfig;
        }

        [Test]
        public void AddFacilityShouldreutrnCorrectiewWihCorrectNumberOfSportCategories()
        {
            AutoMapperConfig autoMapperConfig = GetAutoMapper();
            autoMapperConfig.Execute(typeof(FacilitiesPrivateController).Assembly);
            

            sportCategoriesServiceMock.Setup(x => x.All())
                .Returns(new List<SportCategory>
                {
                    new SportCategory()
                    {
                        Name = "Test", Description = "TestDescription"
                    },
                     new SportCategory{
                        Name = "Test", Description = "TestDescription"
                    } 
                }.AsQueryable) ;


            var controller = new FacilitiesPrivateController(facilitiesServiceMock.Object, usersServiceMock.Object, citiesServiceMock.Object, sportCategoriesServiceMock.Object);
            controller.WithCallTo(x => x.AddFacility())
                .ShouldRenderView("AddFacility")
                .WithModel<FacilityChangeViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(2, viewModel.SportCategoriesDropDown.Count());
                    }).AndNoModelErrors();
        }

        [Test]
        public void AddFacilityShouldReutrnCorrectiewWihCorrectNumberOfCities()
        {
            AutoMapperConfig autoMapperConfig = GetAutoMapper();
            autoMapperConfig.Execute(typeof(FacilitiesPrivateController).Assembly);


            citiesServiceMock.Setup(x => x.All())
                .Returns(new List<City>
                {
                    new City()
                    {
                        Name = "Test"
                    },
                     new City{
                        Name = "Test"
                    },                     
                     new City{
                        Name = "Test"
                    }
                }.AsQueryable);


            var controller = new FacilitiesPrivateController(facilitiesServiceMock.Object, usersServiceMock.Object, citiesServiceMock.Object, sportCategoriesServiceMock.Object);
            controller.WithCallTo(x => x.AddFacility())
                .ShouldRenderView("AddFacility")
                .WithModel<FacilityChangeViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(3, viewModel.CitiesDropDown.Count());
                    }).AndNoModelErrors();
        }

        [Test]
        public void AddFacilityPostShouldReutrnCorrectViewWhenModelStateIsNotvalid()
        {
            AutoMapperConfig autoMapperConfig = GetAutoMapper();
            autoMapperConfig.Execute(typeof(FacilitiesPrivateController).Assembly);


            FacilityChangeViewModel model = new FacilityChangeViewModel();
            var controller = new FacilitiesPrivateController(facilitiesServiceMock.Object, usersServiceMock.Object, citiesServiceMock.Object, sportCategoriesServiceMock.Object);
            controller.ModelState.AddModelError("key", "error");
            controller.WithCallTo(x => x.AddFacility(model))
                .ShouldRenderView("AddFacility")
                .WithModel<FacilityChangeViewModel>();
        }
    }
}
