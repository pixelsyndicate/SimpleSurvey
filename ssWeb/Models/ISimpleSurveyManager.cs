using System.Collections.Generic;

namespace ssWeb.Models
{
    public interface ISimpleSurveyManager
    {
        User GetNewUserFromIdentity();
        SurveyQuestionAnswerViewModel GetSurveyViewModelBySurveyId(int id);
        bool? IsActive(int id);
        IList<SurveyQuestionAnswerViewModel> GetSurveyQuestionAnswerViewModels();

    }
}