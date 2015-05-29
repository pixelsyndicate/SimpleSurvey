using System.Collections;
using System.Collections.Generic;

namespace ssWeb.Repositories
{
    public interface IUserRepository
    {
        IEnumerable GetAll();
        User Get(int id);
        User Add(User item);
        bool Update(User item);
        bool Delete(int id);
    }
}