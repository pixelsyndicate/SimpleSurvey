using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ssWeb.Services
{
    public class Helpers
    {
        public static User TranslateIdentityToUserRecord(string identName)
        {
            var un = identName.Replace("GO\\", string.Empty);
            var lastNamesubStart = un.IndexOf(".", StringComparison.Ordinal);
            var firstNameSubEnd = lastNamesubStart;
            var firstName = un.Substring(0, firstNameSubEnd);
            var lastName = un.Substring(lastNamesubStart + 1, un.Length - firstName.Length - 1);
            return new User
            {
                FirstName = firstName,
                ID = 0,
                LastName = lastName,
                Password = "",
                Role = 1,
                UserName = un
            };
        }
    }
}