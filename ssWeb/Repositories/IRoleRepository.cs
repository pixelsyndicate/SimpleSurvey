using System.Collections;

namespace ssWeb.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable GetAll();
        Role Get(int id);
        Role Add(Role item);
        bool Update(Role item);
        bool Delete(int id);
    }
}