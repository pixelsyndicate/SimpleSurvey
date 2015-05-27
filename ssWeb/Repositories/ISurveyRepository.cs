using System.Collections;

namespace ssWeb.Repositories
{
    public interface ISurveyRepository
    {
        IEnumerable GetAll();
        Survey Get(int id);
        Survey Add(Survey item);
        bool Update(Survey item);
        bool Delete(int id);
    }
}