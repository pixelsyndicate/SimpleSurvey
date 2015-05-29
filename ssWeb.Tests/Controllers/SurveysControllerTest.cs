using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ssWeb.Controllers;
using ssWeb.Repositories;

namespace ssWeb.Tests.Controllers
{
    [TestClass]
    public class SurveysControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            SurveysController controller = new SurveysController(new Mock<ISurveyRepository>().Object, new Mock<IUserRepository>().Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

           // Assert.IsNotNull(result.Model);
        }
    }
}