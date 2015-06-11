using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;

namespace ssWeb.Repositories
{
    public class FakeRoleRepository : IRoleRepository
    {
        public IEnumerable GetAll()
        {
            IList<Role> roles = GenerateRoles(2);
            return roles;
        }

        public Role Get(int id)
        {
            List<Role> roles = GetAll() as List<Role>;
            return roles.FirstOrDefault(x => x.ID == id);
        }

        public Role Add(Role item)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Role item)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }


        private IList<Role> GenerateRoles(int i)
        {
            var factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<Role>();
                x.Include<Role>()
                    .Setup(c => c.ID).Use<IntegerIdSource>()
                    .Setup(c => c.Name).Use<DefaultStringSource>();
            });


            var session = factory.CreateSession();


            //var role = session.Single<Role>()
            //    .Impose(x => x.ID, 1)
            //    .Impose(x => x.Name, "User")
            //    .Get();


            var roles = session.List<Role>(i)
                .First(1).Impose(x => x.Name, "User").Next(1).Impose(x => x.Name, "Admin").All().Get();

            return roles;

        }
    }
}