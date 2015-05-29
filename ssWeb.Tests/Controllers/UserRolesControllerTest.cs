using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ssWeb.Controllers;
using ssWeb.Repositories;

namespace ssWeb.Tests.Controllers
{
    [TestClass]
    public class UserRolesControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
           RolesController controller = new RolesController(new Mock<IRoleRepository>().Object);

            // Act
          ViewResult result = controller.Index() as ViewResult;

            // Assert
          Assert.IsNotNull(result);

       //    Assert.IsNotNull(result.Model);
        }
    }
}