using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ssWeb.Controllers;
using ssWeb.Repositories;

namespace ssWeb.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange IUserRepository userRepo, IRoleRepository roleRepo
            UsersController controller = new UsersController(new Mock<IUserRepository>().Object, new Mock<IRoleRepository>().Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Model);
        }
    }
}