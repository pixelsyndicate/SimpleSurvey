using System.Collections;
using System.Collections.Generic;
using ssWeb.Models;

namespace ssWeb.Repositories
{
    public interface ISurveyRepository
    {
        IEnumerable GetAll();
        Survey Get(int id);
        Survey Add(Survey item);
        bool Update(Survey item);
        bool Delete(int id);


        /// <summary>
        /// Calls SurveyManager.GetSurveyViewModelBySurveyId
        /// </summary>
        /// <returns>SurveyQuestionAnswerViewModel</returns>
        SurveyQuestionAnswerViewModel GetCompleteDataSet(int id);

        IList<SurveyQuestionAnswerViewModel> GetCompleteDataSet();
    }
}