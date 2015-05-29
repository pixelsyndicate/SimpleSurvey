using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ssWeb.Models;
using ssWeb.Repositories;
using System.Linq;
using Moq;


namespace ssWeb.Tests.Repositories
{
    [TestClass]
    public class SurveyRepositoryTests
    {
        [TestMethod]
        public void SurveyManager_GetSurveyQuestionAnswerViewModels()
        {
            //// Arrange
            SurveyManager sMan = new SurveyManager(new Mock<ISurveyRepository>().Object);
            IList<SurveyQuestionAnswerViewModel> toReturnCollection;

            //// Act
            // var results = sMan.GetSurveyQuestionAnswerViewModels();
            //using (simpleSurvey1Entities db = new simpleSurvey1Entities())
            //{
            //    var surveyModel = from sq in db.Survey_Questions
            //                      // join table surveys to questions
            //                      join survey in db.Surveys on sq.SurveyID equals survey.ID // link to Surveys
            //                      join surveyuser in db.Users on survey.CreatedBy equals surveyuser.ID // Survey Created By
            //                      join question in db.Questions on sq.QuestionID equals question.ID
            //                      // link to Questions
            //                      join surveyresponse in db.SurveyResponses on survey.ID equals surveyresponse.SurveyID
            //                      // responses of Survey
            //                      join responsefilledby in db.Users on surveyresponse.FilledBy equals responsefilledby.ID
            //                      // Response Filled By
            //                      select new SurveyQuestionAnswerViewModel
            //                      {
            //                          Survey = survey,
            //                          Response = surveyresponse,
            //                          SurveyCreatedBy = surveyuser,
            //                          ResponseBy = responsefilledby,
            //                          UserRole = surveyuser.Role1
            //                      };

            //    toReturnCollection = surveyModel.ToList();
            //}


            // Assert
            Assert.IsNotNull(sMan);
        }
    }
}
