using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class CharityUser
    {
        public enum CharityUserStatus
        {
            Active = 1,
            Inactive = 2
        }

        public enum CharityUserSecurityLevel
        {
            User = 1,
            Administrator= 2
        }

        public CharityUser()
        {
        }

        public CharityUser(DataRow dr)
        {
            Id = (int)dr["Id"];
            OrganizationId = (int)dr["OrganizationId"];
            UserSecurity = (CharityUserSecurityLevel)(int)dr["UserSecurity"];
            Email = (string)dr["Email"];
            Password = (string)dr["Password"];
            FirstName = (string)dr["FirstName"];
            LastName = (string)dr["LastName"];
            Status = (CharityUserStatus)(int)dr["Status"];
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public CharityUserSecurityLevel UserSecurity { get; set; }
        public string Email { get; set; }
        private string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CharityUserStatus Status { get; set; }

        public string DisplayName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public static CharityUser Select(string email)
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@email", email);

            DataTable dt = Database.Instance.Query("sp_Users_Get", parameters);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    return new CharityUser(dr);
                }
            }
            return null;
        }

        public static CharityUser Select(int id)
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@id", id);

            DataTable dt = Database.Instance.Query("sp_Users_Get", parameters);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    return new CharityUser(dr);
                }
            }
            return null;
        }

        public bool Update()
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@id", Id);
            parameters.Add("@status", Status);
            return (Database.Instance.NonQuery("sp_Users_Update", parameters) == 1);
        }

        public bool ValidatePassword(string _password)
        {
            return (_password == Password);
        }
    }
}
