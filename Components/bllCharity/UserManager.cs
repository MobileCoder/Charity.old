using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bllCharity
{
    public class UserManager : Database
    {
        static public User CreateUser(string email)
        {
            User user = new User();
            user.Email = email;
            user.Id = NonQuery(user.InsertSql);
            return user;
        }
    }
}
