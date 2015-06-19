using System;
using System.Collections.Generic;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ssWeb.Tests.Fakes
{
    [TestClass]
    public class AutoPocoTests
    {
        [TestMethod]
        public void TestMethod1()
        {
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

            var halfCount = i / 2;
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
    }
}
