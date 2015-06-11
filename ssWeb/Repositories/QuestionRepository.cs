using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using AutoPoco;
using AutoPoco.DataSources;
using ssWeb.Models;

namespace ssWeb.Repositories
{

    public class FakeQuestionRepository : IQuestionRepository
    {

        private IUserRepository _userRepo;

        public FakeQuestionRepository(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IEnumerable GetAll()
        {
            throw new NotImplementedException();
        }

        public Question Get(int id)
        {
            throw new NotImplementedException();
        }

        public Question Add(Question item)
        {
            throw new NotImplementedException();
        }

        public bool Update(Question item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public SurveyQuestionAnswerViewModel GetCompleteDataSet(int id)
        {
            throw new NotImplementedException();
        }

        public IList<SurveyQuestionAnswerViewModel> GetCompleteDataSet()
        {
            throw new NotImplementedException();
        }

        private IList<Question> GenerateQuestions(int i)
        {
           
            var factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<Question>();

                x.Include<SurveyResponse>()
                    .Setup(c => c.ID).Use<IntegerIdSource>()
                    .Setup(c => c.Response).Use<DefaultStringSource>()
                    .Setup(c => c.User).Use<ValueSource<User>>();

                x.Include<Question>()
                    .Setup(c => c.ID).Use<IntegerIdSource>()
                    .Setup(c => c.Text).Use<DefaultStringSource>()
                    .Setup(c => c.QuestionType).Use<DefaultStringSource>(); ;
            });


            var session = factory.CreateSession();


            //var role = session.Single<Role>()
            //    .Impose(x => x.ID, 1)
            //    .Impose(x => x.Name, "User")
            //    .Get();


            var questions = session.List<Question>(i)
                .Get();

            return questions;

        }
    }

    public class QuestionRepository : IQuestionRepository
    {
        private simpleSurvey1Entities _db = new simpleSurvey1Entities();
        private int _nextId = 1;
        private Question _question;
        private List<Question> _questions = new List<Question>();

        public IEnumerable GetAll()
        {
            // TODO : Code to get the list of all the records in database

            // test get values from db
            using (_db = new simpleSurvey1Entities())
            {
                _db.Configuration.ProxyCreationEnabled = false;

                var questionModels = from q in _db.Questions
                    join sq in _db.Survey_Questions on q.ID equals sq.QuestionID
                    join s in _db.Surveys on sq.SurveyID equals s.ID
                    select q;


                _questions = new List<Question>(questionModels);
            }
            _nextId = _questions.LastOrDefault().ID + 1;
            return _questions;
        }

        public Question Get(int id)
        {
            // TO DO : Code to find a record in database
            _questions = GetAll() as List<Question>;
            return _questions.FirstOrDefault(p => p.ID == id); //.Find(p => p.ID == id);
        }

        /// <summary>
        ///     Adds and saves the supplied question to the database
        /// </summary>
        /// <param name="item">Question object</param>
        /// <returns>Question object</returns>
        public Question Add(Question item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            _questions = GetAll() as List<Question>;

            // TO DO : Code to save record into database
            item.ID = _nextId++;
            using (_db = new simpleSurvey1Entities())
            {
                _db.Entry(item).State = EntityState.Added;
                _db.SaveChanges();
            }

            _questions.Add(item);
            return item;
        }

        public bool Update(Question item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to update record into database
            _questions = GetAll() as List<Question>;
            var index = _questions.FindIndex(p => p.ID == item.ID);
            if (index == -1)
            {
                return false;
            }

            using (_db = new simpleSurvey1Entities())
            {
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
            }
            _questions.RemoveAt(index);
            _questions.Add(item);
            return true;
        }

        public bool Delete(int id)
        {
            // TO DO : Code to remove the records from database
            _questions = GetAll() as List<Question>;
            var index = _questions.FindIndex(p => p.ID == id);
            if (index == -1)
            {
                return false;
            }

            _question = Get(id);
            using (_db = new simpleSurvey1Entities())
            {
                _db.Entry(_question).State = EntityState.Deleted;
                _db.SaveChanges();
            }

            _questions.RemoveAll(p => p.ID == id);
            return true;
        }

        /// <summary>
        /// Calls SurveyManager.GetSurveyViewModelBySurveyId
        /// </summary>
        /// <returns>SurveyQuestionAnswerViewModel</returns>
        public SurveyQuestionAnswerViewModel GetCompleteDataSet(int id)
        {
            return SimpleSurveyManager.GetQuestionManager().GetSurveyViewModelBySurveyId(id);
        }

        /// <summary>
        /// Calls SurveyManager.GetSurveyQuestionAnswerViewModels()
        /// </summary>
        /// <returns>List of SurveyQuestionAnswerViewModel</returns>
        public IList<SurveyQuestionAnswerViewModel> GetCompleteDataSet()
        {
            return SimpleSurveyManager.GetQuestionManager().GetSurveyQuestionAnswerViewModels();
        }


    }
}