using Data.Domain.Entity;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.IntegrationTests.TestUtilities
{
    public class Db
    {
        public static void InitializeDbForTests(AppDbContext db)
        {
            //just for example - delete all weather info
            //var allWeather = db.Set<Weather>().Select(w => w);
            //db.Set<Weather>().RemoveRange(allWeather);

            ////ensure admin user
            //var adminUser = db.Set<User>().FirstOrDefault(u => u.Email == "admin@apistarter.com");
            //if (adminUser == null)
            //{
            //    adminUser = new User()
            //    {
            //        Username = Users.ADMIN_USER_USERNAME,
            //        Email = Users.ADMIN_USER_EMAIL,
            //        Password = Utilities.Crypt.CreateMD5(Users.ADMIN_USER_PASSWORD),
            //        IsAllowed = true
            //    };
            //    db.Set<User>().Add(adminUser);
            //}

            //db.SaveChanges();

        }
    }
}
