using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bllCharity
{
    public class UserManager : Database
    {
        static public User FindUser(string email)
        {
            DataSet ds = Query(User.FindSql(email));
            if (ds != null)
            {
                User user = new User();
                user.LoadData(ds.Tables[0].Rows[0]);
                return user;
            }
            return null;
        }

        static public User CreateUser(string email)
        {
            if (NonQuery(User.InsertSql(email)) > -1)
            {
                return FindUser(email);
            }
            return null;
        }

        static public bool UpdateUser(User user)
        {
            return (NonQuery(user.UpdateSql) > -1);
        }
    }
}
