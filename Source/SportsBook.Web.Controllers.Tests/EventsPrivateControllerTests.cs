using Moq;
using NUnit.Framework;
using SportsBook.Data.Models;
using SportsBook.Services.Data.Contracts;
using SportsBook.Web.Areas.Events.Controllers;
using SportsBook.Web.Areas.Events.ViewModels.EventsModels;
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
    public class EventsPrivateControllerTests
    {

        Mock<IEventsService> eventsServiceMock = new Mock<IEventsService>();
        Mock<IUsersService> usersServiceMock = new Mock<IUsersService>();
        Mock<ICitiesService> citiesServiceMock = new Mock<ICitiesService>();
        Mock<ISportCategoriesService> sportCategoriesServiceMock = new Mock<ISportCategoriesService>();
        Mock<IFacilitiesService> facilitiesServiceMock = new Mock<IFacilitiesService>();

        public AutoMapperConfig GetAutoMapper()
        {
            AutoMapperConfig autoMapperConfig = new AutoMapperConfig();
            return autoMapperConfig;
        }

        [Test]
        public void AddEventShouldreutrnCorrectiewWihCorrectNumberOfSportCategories()
        {
            AutoMapperConfig autoMapperConfig = GetAutoMapper();
            autoMapperConfig.Execute(typeof(EventsPrivateController).Assembly);


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
                }.AsQueryable);


            var controller = new EventsPrivateController(eventsServiceMock.Object, usersServiceMock.Object, citiesServiceMock.Object, sportCategoriesServiceMock.Object, facilitiesServiceMock.Object);
            controller.WithCallTo(x => x.AddEvent())
                .ShouldRenderView("AddEvent")
                .WithModel<EventChangeViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(2, viewModel.SportCategoriesDropDown.Count());
                    }).AndNoModelErrors();
        }

        [Test]
        public void AddEventShouldReutrnCorrectiewWihCorrectNumberOfCities()
        {
            AutoMapperConfig autoMapperConfig = GetAutoMapper();
            autoMapperConfig.Execute(typeof(EventsPrivateController).Assembly);


            citiesServiceMock.Setup(x => x.All())
                .Returns(new List<City>
                {
                    new City()
                    {
                        Name = "Test1"
                    },
                     new City{
                        Name = "Test2"
                    },
                     new City{
                        Name = "Test3"
                    }
                }.AsQueryable);


            var controller = new EventsPrivateController(eventsServiceMock.Object, usersServiceMock.Object, citiesServiceMock.Object, sportCategoriesServiceMock.Object, facilitiesServiceMock.Object);
            controller.WithCallTo(x => x.AddEvent())
                .ShouldRenderView("AddEvent")
                .WithModel<EventChangeViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(3, viewModel.CitiesDropDown.Count());
                    }).AndNoModelErrors();
        }

        [Test]
        public void AddEventPostShouldReutrnCorrectViewWhenModelStateIsNotValid()
        {
            AutoMapperConfig autoMapperConfig = GetAutoMapper();
            autoMapperConfig.Execute(typeof(EventsPrivateController).Assembly);


            EventChangeViewModel model = new EventChangeViewModel();
            var controller = new EventsPrivateController(eventsServiceMock.Object, usersServiceMock.Object, citiesServiceMock.Object, sportCategoriesServiceMock.Object, facilitiesServiceMock.Object);
            controller.ModelState.AddModelError("key", "error");
            controller.WithCallTo(x => x.AddEvent(model))
                .ShouldRenderView("AddEvent")
                .WithModel<EventChangeViewModel>();
        }

        [Test]
        public void AddEventPostShouldReutrnCorrectViewWhenInvaliDataIsPassed()
        {
            AutoMapperConfig autoMapperConfig = GetAutoMapper();
            autoMapperConfig.Execute(typeof(EventsPrivateController).Assembly);


            EventChangeViewModel model = new EventChangeViewModel();
            var controller = new EventsPrivateController(eventsServiceMock.Object, usersServiceMock.Object, citiesServiceMock.Object, sportCategoriesServiceMock.Object, facilitiesServiceMock.Object);
            controller.ModelState.AddModelError("Name", "Exception");
            controller.WithCallTo(x => x.AddEvent(model))
                .ShouldRenderView("AddEvent")
                .WithModel<EventChangeViewModel>()
                .AndModelErrorFor(x => x.Name);
        }
    }
}
