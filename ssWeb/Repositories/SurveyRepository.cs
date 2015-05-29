using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using ssWeb.Models;

namespace ssWeb.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private simpleSurvey1Entities _db = new simpleSurvey1Entities();
        private List<Survey> _surveys = new List<Survey>();
        private Survey _survey;
        private int _nextId = 1;

        public IEnumerable GetAll()
        {

            // TODO : Code to get the list of all the records in database

            // test get values from db
            using (_db = new simpleSurvey1Entities())
            {
                _db.Configuration.ProxyCreationEnabled = false;

                var surveyModels = from s in _db.Surveys
                                   join u in _db.Users on s.CreatedBy equals u.ID
                                   select s;


                _surveys = new List<Survey>(surveyModels);
            }
            return _surveys;
        }

        public Survey Get(int id)
        {
            // TO DO : Code to find a record in database
            _surveys = GetAll() as List<Survey>;
            return _surveys.FirstOrDefault(p => p.ID == id);//.Find(p => p.ID == id);
        }
        public Survey Add(Survey item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            _surveys = GetAll() as List<Survey>;

            // TO DO : Code to save record into database
            item.ID = _nextId++;
            using (_db = new simpleSurvey1Entities())
            {
                _db.Entry(item).State = EntityState.Added;
                _db.SaveChanges();
            }

            _surveys.Add(item);
            return item;
        }
        public bool Update(Survey item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to update record into database
            _surveys = GetAll() as List<Survey>;
            int index = _surveys.FindIndex(p => p.ID == item.ID);
            if (index == -1)
            {
                return false;
            }

            using (_db = new simpleSurvey1Entities())
            {
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
            }
            _surveys.RemoveAt(index);
            _surveys.Add(item);
            return true;
        }
        public bool Delete(int id)
        {
            // TO DO : Code to remove the records from database
            _surveys = GetAll() as List<Survey>;
            int index = _surveys.FindIndex(p => p.ID == id);
            if (index == -1)
            {
                return false;
            }

            _survey = Get(id);
            using (_db = new simpleSurvey1Entities())
            {
                _db.Entry(_survey).State = EntityState.Deleted;
                _db.SaveChanges();
            }

            _surveys.RemoveAll(p => p.ID == id);
            return true;
        }

        /// <summary>
        /// Calls SurveyManager.GetSurveyViewModelBySurveyId
        /// </summary>
        /// <returns>SurveyQuestionAnswerViewModel</returns>
        public SurveyQuestionAnswerViewModel GetCompleteDataSet(int id)
        {
            SurveyManager sm = new SurveyManager(new SurveyRepository());
            var data = sm.GetSurveyViewModelBySurveyId(id);

            return data;
        }

        public IList<SurveyQuestionAnswerViewModel> GetCompleteDataSet()
        {
            SurveyManager sm = new SurveyManager(new SurveyRepository());
            var data = sm.GetSurveyQuestionAnswerViewModels();

            return data;
        } 
    }
}