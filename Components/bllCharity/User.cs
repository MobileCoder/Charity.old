using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bllCharity
{
    public class User : DatabaseTable
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        new static public string FindSql(string key)
        {
            return string.Format("select * from Users where Email='{0}'", key);
        }

        new static public string InsertSql(string key)
        {
            return string.Format("insert into Users (Email) values ('{0}')", key);
        }

        public override bool LoadData(DataRow dr)
        {
            try
            {
                Id = int.Parse(dr["Id"].ToString());
                Email = (string)dr["Email"];
                FirstName = (string)dr["FirstName"];
                LastName = (string)dr["LastName"];
                return true;
            }
            catch (Exception ex)
            {
                ReportException(ex);
            }
            return false;
        }

        public override string UpdateSql 
        {
            get
            {
                return string.Format(
                    "update Users set Email='{1}', FirstName='{2}', LastName='{3}' where Id={0}",
                    Id,
                    Email,
                    FirstName,
                    LastName);
            }
        }
    }
}
