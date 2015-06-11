using System.Collections;
using System.Collections.Generic;
using ssWeb.Models;

namespace ssWeb.Repositories
{
    public interface IQuestionRepository
    {
        IEnumerable GetAll();
        Question Get(int id);
        Question Add(Question item);
        bool Update(Question item);
        bool Delete(int id);

        /// <summary>
        /// Calls SurveyManager.GetSurveyViewModelBySurveyId
        /// </summary>
        /// <returns>SurveyQuestionAnswerViewModel</returns>
        SurveyQuestionAnswerViewModel GetCompleteDataSet(int id);

        IList<SurveyQuestionAnswerViewModel> GetCompleteDataSet();
    }
}