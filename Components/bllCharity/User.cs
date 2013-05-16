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
            Administrator = 2
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
                string displayName = string.Empty;

                if (!string.IsNullOrEmpty(FirstName))
                    displayName += FirstName;

                displayName += " ";

                if (!string.IsNullOrEmpty(LastName))
                    displayName += LastName;

                if (displayName.Trim().Length == 0)
                    displayName = Email;

                return displayName;
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

        public static CharityUser Create(string email, out string error)
        {
            error = string.Empty;

            int organizationId = 0;
            int userSecurity = (int)CharityUserSecurityLevel.User;
            string password = string.Empty;
            string firstname = string.Empty;
            string lastname = string.Empty;
            int status = (int)CharityUserStatus.Active;

            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@organizationId", organizationId);
            parameters.Add("@UserSecurity", userSecurity);
            parameters.Add("@email", email);
            parameters.Add("@password", password);
            parameters.Add("@firstname", firstname);
            parameters.Add("@lastname", lastname);
            parameters.Add("@status", status);

            Database db = Database.Instance;
            object obj = db.Scalar("sp_Users_Create", parameters);
            if (obj != null)
            {
                error = db.Exception;
            }
            else
            {
                int id = (int)obj;
                if (id > 0)
                {
                    return CharityUser.Select(id);
                }
            }
            return null;
        }

        public bool Update()
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@id", Id);
            parameters.Add("@status", Status);
            parameters.Add("@password", Password);
            parameters.Add("@firstName", FirstName);
            parameters.Add("@lastName", LastName);
            int rc = Database.Instance.NonQuery("sp_Users_Update", parameters);
            return (rc == 1);
        }

        public bool ValidatePassword(string _password)
        {
            return (_password == Password);
        }

        public bool SetPassword(string password)
        {
            Password = password;
            return true;
        }

        public bool ResetPassword()
        {
            string password = "Welcome1";
            if (SetPassword(password))
            {
                return Update();
            }
            return false;
        }
    }
}
