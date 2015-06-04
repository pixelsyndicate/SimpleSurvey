using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ssWeb.Models;
using ssWeb.Repositories;
using System.Linq;
using Moq;
using Moq.Language;


namespace ssWeb.Tests.Repositories
{
    [TestClass]
    public class SurveyRepositoryTests
    {

        [TestMethod]
        public void TestMock4()
        {

            var mock = new Mock<ISurveyManager>();

            // Create your own responses from the mock method call
            mock.Setup(framework => framework.IsActive(3)).Returns(false);
            mock.Setup(framework => framework.IsActive(2)).Returns(true);
            mock.Setup(framework => framework.IsActive(1)).Returns((bool?) null);
            

            // Hand mock.Object as a collaborator and exercise it, 
            // like calling methods on it...
            ISurveyManager sman = mock.Object;
            bool isActive2 = sman.IsActive(2).Value;
             Assert.IsTrue(isActive2);
            bool? isActive1 = sman.IsActive(1);
            Assert.IsNull(isActive1);
            bool isActive3 = sman.IsActive(3).Value;
            Assert.IsFalse(isActive3);

            // Verify that the given method was indeed called with the expected value at most once
            mock.Verify(s => s.IsActive(1), Times.AtMostOnce());
            mock.Verify(s => s.IsActive(2), Times.AtMostOnce());
            mock.Verify(s => s.IsActive(3), Times.AtMostOnce());

            Assert.IsNull(isActive1);
        }

        [TestMethod]
        public void SurveyManager_GetSurveyQuestionAnswerViewModels()
        {
            // Arrange

            // one way to compare
            var mock = new Mock<ISurveyRepository>(MockBehavior.Strict);
            mock.Setup(x => x.GetAll()).Returns(new List<String>());

            var list = new List<Survey> { new Survey() };
            var surveyRepo = Mock.Of<ISurveyRepository>(x => x.GetAll() == list);


            SurveyManager sMan = new SurveyManager(mock.Object);


            IList<SurveyQuestionAnswerViewModel> toReturnCollection;

            //// Act
        


            // Assert
            Assert.IsNotNull(sMan);

            Assert.IsNotNull(surveyRepo);
        }
    }
}
