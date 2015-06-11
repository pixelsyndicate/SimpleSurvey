using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ssWeb.Models;

namespace ssWeb.Repositories
{
    public class SurveyResponsesRepository : ISurveyResponsesRepository
    {
        private simpleSurvey1Entities _db = new simpleSurvey1Entities();
        private int _nextId = 1;
        private SurveyResponse _question;
        private List<SurveyResponse> _questions = new List<SurveyResponse>();

        public IEnumerable GetAll()
        {
            // TODO : Code to get the list of all the records in database

            // test get values from db
            using (_db = new simpleSurvey1Entities())
            {
                _db.Configuration.ProxyCreationEnabled = false;

                var responseModels = from r in _db.SurveyResponses
                    join q in _db.Questions on r.QuestionID equals q.ID
                    join s in _db.Surveys on r.SurveyID equals s.ID
                    select r;


                _questions = new List<SurveyResponse>(responseModels);
            }
            _nextId = _questions.LastOrDefault().ID + 1;
            return _questions;
        }

        public SurveyResponse Get(int id)
        {
            // TO DO : Code to find a record in database
            _questions = GetAll() as List<SurveyResponse>;
            return _questions.FirstOrDefault(p => p.ID == id); //.Find(p => p.ID == id);
        }

        /// <summary>
        ///     Adds and saves the supplied question to the database
        /// </summary>
        /// <param name="item">Question object</param>
        /// <returns>Question object</returns>
        public SurveyResponse Add(SurveyResponse item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            _questions = GetAll() as List<SurveyResponse>;

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

        public bool Update(SurveyResponse item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to update record into database
            _questions = GetAll() as List<SurveyResponse>;
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
            _questions = GetAll() as List<SurveyResponse>;
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
            return SimpleSurveyManager.GetSurveyResponseManager().GetSurveyViewModelBySurveyId(id);
        }

        /// <summary>
        /// Calls SurveyManager.GetSurveyQuestionAnswerViewModels()
        /// </summary>
        /// <returns>List of SurveyQuestionAnswerViewModel</returns>
        public IList<SurveyQuestionAnswerViewModel> GetCompleteDataSet()
        {
            return SimpleSurveyManager.GetSurveyResponseManager().GetSurveyQuestionAnswerViewModels();
        }


    }
}