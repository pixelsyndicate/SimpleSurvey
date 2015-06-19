using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ssWeb.Models;

namespace ssWeb.Tests.Fakes
{
    [TestClass]
    public class FakerNetTests
    {
        [TestMethod]
        public void TestMethod1()
        {

            var users = GenerateUsers(30);

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
        }


  



        private IList<User> GenerateUsers(int i)
        {


            List<User> users = new List<User>(i);
            for (int u = 1; u <= i; u++)
            {
                User usr = new User
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Password = 123.ToString(),
                    Role = 1
                };
                usr.UserName = usr.FirstName + "." + usr.LastName;
                users.Add(usr);
            }

            return users;

        }
    }
}
