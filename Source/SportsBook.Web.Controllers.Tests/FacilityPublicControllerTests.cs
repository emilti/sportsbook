using Moq;
using NUnit.Framework;
using SportsBook.Data.Models;
using SportsBook.Services.Data.Contracts;
using SportsBook.Web.Areas.Facilities.Controllers;
using TestStack.FluentMVCTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsBook.Web.Infrastructure.Mapping;
using SportsBook.Web.Areas.Facilities.ViewModels.Facilities;

namespace SportsBook.Web.Controller.Tests
{
   
    [TestFixture]
   public class FacilityPublicControllerTests
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
       public void FacilityDetailsShouldWorkCorrectly()
       {
           AutoMapperConfig autoMapperConfig = GetAutoMapper();
           autoMapperConfig.Execute(typeof(FacilitiesPublicController).Assembly);
           const string Description = "TestDescription";      
           
           facilitiesServiceMock.Setup(x => x.GetFacilityDetails(It.IsAny<int>()))
               .Returns(new Facility { Name = "Test", Description = "TestDescription", Image = "image" });
           
           
           var controller = new FacilitiesPublicController(facilitiesServiceMock.Object, usersServiceMock.Object, citiesServiceMock.Object, sportCategoriesServiceMock.Object);
           controller.WithCallTo(x => x.FacilityDetails(1))
               .ShouldRenderView("FacilityDetails")
               .WithModel<FacilityDetailedViewModel>(
                   viewModel =>
                   {
                       Assert.AreEqual(Description, viewModel.Description);
                   }).AndNoModelErrors();
       }

      //  [Test]
      //  public void SearchFacilitiesShouldWorkCorrectly()
      //  {
      //      AutoMapperConfig autoMapperConfig = GetAutoMapper();
      //      autoMapperConfig.Execute(typeof(FacilitiesPublicController).Assembly);
      //      const string Description = "TestDescription";
      //
      //      facilitiesServiceMock.Setup(x => x.All())
      //          .Returns(new List<Facility>
      //          {
      //              new Facility { Name = "Test", Description = "TestDescription", Image = "image" }
      //          });
      //
      //
      //      var controller = new FacilitiesPublicController(facilitiesServiceMock.Object, usersServiceMock.Object, citiesServiceMock.Object, sportCategoriesServiceMock.Object);
      //      controller.WithCallTo(x => x.FacilityDetails(1))
      //          .ShouldRenderView("FacilityDetails")
      //          .WithModel<FacilityDetailedViewModel>(
      //              viewModel =>
      //              {
      //                  Assert.AreEqual(Description, viewModel.Description);
      //              }).AndNoModelErrors();
      //  }

    }
}
