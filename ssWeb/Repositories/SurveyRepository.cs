using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ssWeb.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {

        private List<Survey> _surveys = new List<Survey>();
        private readonly Survey _survey;
        private int _nextId = 1;

        public IEnumerable GetAll()
        {

            // TODO : Code to get the list of all the records in database

            // test get values from db
            using (simpleSurvey1Entities db = new simpleSurvey1Entities())
            {
                var surveyModels = from s in db.Surveys
                                   // Response Filled By
                                   select s;


                _surveys = new List<Survey>(surveyModels);
            }
            return _surveys;
        }

        public Survey Get(int id)
        {
            // TO DO : Code to find a record in database
            return _surveys.Find(p => p.ID == id);
        }
        public Survey Add(Survey item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to save record into database
            item.ID = _nextId++;
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
            int index = _surveys.FindIndex(p => p.ID == item.ID);
            if (index == -1)
            {
                return false;
            }
            _surveys.RemoveAt(index);
            _surveys.Add(item);
            return true;
        }
        public bool Delete(int id)
        {
            // TO DO : Code to remove the records from database
            _surveys.RemoveAll(p => p.ID == id);
            return true;
        }
    }

}