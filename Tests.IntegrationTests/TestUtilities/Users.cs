using Data.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.IntegrationTests.TestUtilities
{
    public class Users
    {
        public const string ADMIN_USER_USERNAME = "admin@apistarter.com";
        public const string ADMIN_USER_EMAIL = "admin@apistarter.com";
        public const string ADMIN_USER_PASSWORD = "admin";

        public static User NewUser(string prefix)
        {
            prefix = $"{prefix}-{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}-{Guid.NewGuid()}";
            return new User()
            {
                Username = prefix,
                Email = $"{prefix}@test.com",
                Password = "AllUsersHaveTheSamePasswordInTheTestingUniverse",
                IsAllowed = true
            };

            //test doar ca sa schimb ceva
        }
    }
}
