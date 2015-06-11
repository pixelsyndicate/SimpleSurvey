using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;


namespace ssWeb.Repositories
{
    public class FakeUserRepository : IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            IList<User> users = new List<User>();

           users=  GenerateUsers(100);

            return users;
        }

        private IList<User> GenerateUsers(int i)
        {
            IDatasource<User> _users = new AutoSource<User>();
            IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });
                x.AddFromAssemblyContainingType<User>();

                // configuration will provide more meaningful data

                x.Include<User>()
                    .Setup(c => c.UserName).Use<EmailAddressSource>()
                    .Setup(c => c.FirstName).Use<FirstNameSource>()
                    .Setup(c => c.LastName).Use<LastNameSource>()
                    .Setup(c => c.ID).Use<IntegerIdSource>()
                    .Setup(c => c.Password).Use<RandomStringSource>(5, 9);



                // Invoke calls a method -- .Invoke(c => c.SetPassword(Use.Source<String, PasswordSource>()));
                x.Include<Role>()
                    .Setup(c => c.ID).Use<ValueSource<int>>(1)
                    .Setup(c => c.Name).Use<ValueSource<string>>("User");
            });


            IGenerationSession session = factory.CreateSession();


            User user = session.Single<User>()
                .Impose(x => x.Role, 1)
                .Impose(x => x.Password, "123")
                .Get();

            var halfCount = i/2;
            IList<User> users = session.List<User>(i)
                .Random(halfCount)
                    .Impose(x => x.Role, 1)
                .Next(halfCount)
                    .Impose(x => x.Role, 2)
                .All()
                .First(halfCount)
                    .Impose(x => x.UserName, "Blue.Thunder")
                .Next(halfCount)
                    .Impose(x => x.UserName, "Red.October")
                .All()
                
                .Get();

            return users;

            //List<User> users = new List<User>(i);
            //for (int u = 1; u <= i; u++)
            //{
            //    User usr = new User
            //    {
            //        FirstName = Faker.Name.First(),
            //        LastName = Faker.Name.Last(),
            //        Password = 123.ToString(),
            //        Role = 1
            //    };
            //    usr.UserName = usr.FirstName + "." + usr.LastName;
            //    users.Add(usr);
            //}

            //return users;

        }

        public User Get(int id)
        {
            var userList = GetAll();
            return userList.FirstOrDefault(x => x.ID == id);
           // throw new System.NotImplementedException();
        }

        public User Add(User item)
        {
            var userList = GetAll().ToList();
            userList.Add(item);
            
            return item;
            
        }

        public bool Update(User item)
        {
            return false;
        }

        public bool Delete(int id)
        {
            return false;
        }
    }
}