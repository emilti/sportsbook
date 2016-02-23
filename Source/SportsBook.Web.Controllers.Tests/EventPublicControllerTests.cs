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
    public class EventPublicControllerTests
    {

        Mock<IEventsService> eventsServiceMock = new Mock<IEventsService>();
        Mock<IUsersService> usersServiceMock = new Mock<IUsersService>();
        Mock<ICitiesService> citiesServiceMock = new Mock<ICitiesService>();
        Mock<ISportCategoriesService> sportCategoriesServiceMock = new Mock<ISportCategoriesService>();

        public AutoMapperConfig GetAutoMapper()
        {
            AutoMapperConfig autoMapperConfig = new AutoMapperConfig();
            return autoMapperConfig;
        }

        [Test]
        public void EventDetailsShouldWorkCorrectly()
        {
            AutoMapperConfig autoMapperConfig = GetAutoMapper();
            autoMapperConfig.Execute(typeof(EventsPublicController).Assembly);
            const string Description = "TestDescription";

            eventsServiceMock.Setup(x => x.GetEventDetails(It.IsAny<int>()))
                .Returns(new Event { Name = "Test", Description = "TestDescription", Image = "image" });


            var controller = new EventsPublicController(eventsServiceMock.Object, usersServiceMock.Object, citiesServiceMock.Object, sportCategoriesServiceMock.Object);
            controller.WithCallTo(x => x.EventDetails(1))
                .ShouldRenderView("EventDetails")
                .WithModel<EventDetailedViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(Description, viewModel.Description);
                    }).AndNoModelErrors();
        }
    }
}
