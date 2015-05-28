using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ssWeb;
using ssWeb.Controllers;
using ssWeb.Repositories;

namespace ssWeb.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        private string connStr =
            @"metadata=res://*/SimpleSurveyModel.csdl|res://*/SimpleSurveyModel.ssdl|res://*/SimpleSurveyModel.msl;provider=System.Data.SqlClient;"
            + @"provider connection string=&quot;data source=(LocalDB)\v11.0;attachdbfilename=|DataDirectory|\simpleSurvey1.mdf;integrated security=True;"
            + @"MultipleActiveResultSets=True;App=EntityFramework&quot;";

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(new Mock<ISurveyRepository>().Object, new Mock<IUserRepository>().Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;


            // Assert
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(new Mock<ISurveyRepository>().Object, new Mock<IUserRepository>().Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(new Mock<ISurveyRepository>().Object, new Mock<IUserRepository>().Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
