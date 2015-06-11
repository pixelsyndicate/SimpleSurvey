using System;
using System.Collections.Generic;
using AutoPoco;
using AutoPoco.DataSources;
using Faker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ssWeb.Models;
using ssWeb.Repositories;

namespace ssWeb.Tests.Repositories
{
    [TestClass]
    public class SurveyRepositoryTests
    {
        [TestMethod]
        public void TestMock4()
        {
            var mock = new Mock<ISimpleSurveyManager>();

            // Create your own responses from the mock method call
            mock.Setup(framework => framework.IsActive(3)).Returns(false);
            mock.Setup(framework => framework.IsActive(2)).Returns(true);
            mock.Setup(framework => framework.IsActive(1)).Returns((bool?) null);


            // Hand mock.Object as a collaborator and exercise it, 
            // like calling methods on it...
            var sman = mock.Object;
            var active = sman.IsActive(2);
            var isActive2 = active != null && active.Value;
            Assert.IsTrue(isActive2);
            var isActive1 = sman.IsActive(1);
            Assert.IsNull(isActive1);
            var isActive = sman.IsActive(3);
            var isActive3 = isActive != null && isActive.Value;
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

            var mockSurveyManager = Mock.Of<ISimpleSurveyManager>();
            // one way to compare
            var mockSurveyRepo = new Mock<ISurveyRepository>(MockBehavior.Strict);

            // mock data

            var mockModel = CreateMockData();

            IList<SurveyQuestionAnswerViewModel> mockModels = new List<SurveyQuestionAnswerViewModel> {mockModel};

            // make our surveyRepo.GetAll() return a complete survey model
            mockSurveyRepo.Setup(x => x.GetAll()).Returns(mockModels);


            var list = new List<Survey> {new Survey()};
            var surveyRepo = Mock.Of<ISurveyRepository>(x => x.GetAll().Equals(list));


            var sMan = mockSurveyManager; // new SurveyManager(mockSurveyRepo.Object);


            // Act


            // Assert
            Assert.IsNotNull(sMan);

            Assert.IsNotNull(surveyRepo);
        }

        private SurveyQuestionAnswerViewModel CreateMockData()
        {
            var roleOfUserTakingSurvey = new Role {ID = 1, Name = "User", Users = new List<User>()};
            var roleOfUserCreatingSurvey = new Role {ID = 2, Name = "Admin", Users = new List<User>()};
            var userTakingSurvey = new User
            {
                FirstName = "Bob",
                ID = 2,
                LastName = "Robers",
                Password = "LetMeIn",
                Role = roleOfUserTakingSurvey.ID,
                Role1 = roleOfUserTakingSurvey,
                SurveyResponses = null,
                Surveys = null,
                UserName = "bob.roberts"
            };


            var userCreatingSurvey = new User
            {
                FirstName = "Wil",
                ID = 2,
                LastName = "Dobson",
                Password = "LetMeIn",
                Role = roleOfUserCreatingSurvey.ID,
                Role1 = roleOfUserCreatingSurvey,
                SurveyResponses = null,
                Surveys = null,
                UserName = "wil.dobson"
            };


            roleOfUserCreatingSurvey.Users.Add(userCreatingSurvey);


            var surveyQuestion = new Survey_Questions
            {
                ID = 1,
                OrderId = null,
                Question = null,
                QuestionID = 0,
                Survey = null,
                SurveyID = 0
            };

            var question = new Question
            {
                ID = 1,
                Options = "yes,no,maybe",
                QuestionType = "multiple",
                Text = "do you do stuff?",
                Survey_Questions = null,
                SurveyResponses = null
            };
            surveyQuestion.Question = question;
            surveyQuestion.QuestionID = question.ID;
            var surveyQuestions = new List<Survey_Questions> {surveyQuestion};
            question.Survey_Questions = surveyQuestions;


            var survey = new Survey
            {
                CreatedBy = userCreatingSurvey.ID,
                CreatedOn = DateTime.Today.AddDays(-7),
                Description = "This is a mock Survey",
                ExpiresOn = DateTime.Today.AddDays(1),
                ID = 1000,
                Publish = true,
                Survey_Questions = surveyQuestions,
                SurveyResponses = null,
                Title = "This is my Survey 0001",
                User = userCreatingSurvey
            };


            var response = new SurveyResponse
            {
                FilledBy = userTakingSurvey.ID,
                ID = 1,
                Question = question,
                QuestionID = question.ID,
                Response = "Sure, why not?",
                Survey = survey,
                SurveyID = survey.ID,
                User = userTakingSurvey
            };
            var surveyResponses = new List<SurveyResponse> {response};
            survey.SurveyResponses = surveyResponses;
            question.SurveyResponses = surveyResponses;

            roleOfUserTakingSurvey.Users.Add(userTakingSurvey);


            var model = new SurveyQuestionAnswerViewModel
            {
                Survey = survey,
                Response = response,
                ResponseBy = userTakingSurvey,
                SurveyCreatedBy = userCreatingSurvey,
                UserTakingSurveyRole = userTakingSurvey.Role1,
                UserCreatingSurveyRole = userCreatingSurvey.Role1
            };

            return model;
        }

        [TestMethod]
        public void Do_I_Get_100_Users_From_AutoPoco()
        {
            // Arrange


            // Act
            var users = GenerateAutoPocoUsers(100);

            // Assert
            Assert.IsNotNull(users);

            Assert.AreEqual(users.Count, 100);
        }

        [TestMethod]
        public void Do_I_Get_2_Roles_From_AutoPoco()
        {
            // Arrange


            // Act
            var roles = GenerateAutoPocoRoles(2);

            // Assert
            Assert.IsNotNull(roles);

            Assert.AreEqual(roles.Count, 2);
        }

        [TestMethod]
        public void Do_I_Get_100_Users_From_Faker()
        {
            // Arrange
            IList<User> users = GenerateFakerUsers(100);

            // Assert
            Assert.IsNotNull(users);

            Assert.AreEqual(users.Count, 100);
        }

        private IList<User> GenerateAutoPocoUsers(int i)
        {
            var factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<User>();

                // configuration will provide more meaningful data

                x.Include<User>()
                    .Setup(c => c.UserName).Use<EmailAddressSource>()
                    .Setup(c => c.FirstName).Use<FirstNameSource>()
                    .Setup(c => c.LastName).Use<LastNameSource>()
                    .Setup(c => c.ID).Use<DefaultIntegerSource>()
                    .Setup(c => c.Password).Use<RandomStringSource>(5, 9);


                // Invoke calls a method -- .Invoke(c => c.SetPassword(Use.Source<String, PasswordSource>()));
                x.Include<Role>()
                    .Setup(c => c.ID).Use<ValueSource<int>>(1)
                    .Setup(c => c.Name).Use<ValueSource<string>>("User");
            });


            var session = factory.CreateSession();


            //var user = session.Single<User>()
            //    .Impose(x => x.Role, 1)
            //    .Impose(x => x.Password, "123")
            //    .Get();

            var users = session.List<User>(i)
                .Random(50)
                .Impose(x => x.Role, 1)
                .Next(50)
                .Impose(x => x.Role, 2)
                .All()
                .First(25)
                .Impose(x => x.UserName, "Blue.Thunder")
                .Next(25)
                .Impose(x => x.UserName, "Red.October")
                .All()
                .Get();

            return users;
        }


        private IList<User> GenerateFakerUsers(int i)
        {
            var users = new List<User>(i);
            for (var u = 1; u <= i; u++)
            {
                var usr = new User
                {
                    FirstName = Name.First(),
                    LastName = Name.Last(),
                    Password = RandomNumber.Next(9999, 9999999).ToString(),
                    Role = 1
                };

                usr.UserName = usr.FirstName + "." + usr.LastName;
                users.Add(usr);
            }

            return users;
        }

        private IList<Role> GenerateAutoPocoRoles(int i)
        {
            var factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<Role>();
                x.Include<Role>()
                    .Setup(c => c.ID).Use<IntegerIdSource>()
                    .Setup(c => c.Name).Use<DefaultStringSource>();
            });


            var session = factory.CreateSession();


            //var role = session.Single<Role>()
            //    .Impose(x => x.ID, 1)
            //    .Impose(x => x.Name, "User")
            //    .Get();


            var roles = session.List<Role>(i)
                .First(1).Impose(x => x.Name, "User").Next(1).Impose(x => x.Name, "Admin").All().Get();

            return roles;
        }
    }
}
