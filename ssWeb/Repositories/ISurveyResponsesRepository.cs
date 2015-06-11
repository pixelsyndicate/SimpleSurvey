using System.Collections;
using System.Collections.Generic;
using ssWeb.Models;

namespace ssWeb.Repositories
{
    public interface ISurveyResponsesRepository 
    {
        IEnumerable GetAll();
        SurveyResponse Get(int id);

        /// <summary>
        ///     Adds and saves the supplied question to the database
        /// </summary>
        /// <param name="item">Question object</param>
        /// <returns>Question object</returns>
        SurveyResponse Add(SurveyResponse item);

        bool Update(SurveyResponse item);
        bool Delete(int id);

        /// <summary>
        /// Calls SurveyManager.GetSurveyViewModelBySurveyId
        /// </summary>
        /// <returns>SurveyQuestionAnswerViewModel</returns>
        SurveyQuestionAnswerViewModel GetCompleteDataSet(int id);

        /// <summary>
        /// Calls SurveyManager.GetSurveyQuestionAnswerViewModels()
        /// </summary>
        /// <returns>List of SurveyQuestionAnswerViewModel</returns>
        IList<SurveyQuestionAnswerViewModel> GetCompleteDataSet();
    }
}