using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class CharityUser : ErrorManager
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
            CanBid = (bool)dr["CanBid"];
            AcceptedTerms = (bool)dr["AcceptedTerms"];
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public CharityUserSecurityLevel UserSecurity { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CharityUserStatus Status { get; set; }
        public bool CanBid { get; set; }
        public bool AcceptedTerms { get; set; }

        private List<Address> addresses = null;
        public List<Address> Addresses
        {
            get
            {
                if (addresses == null)
                {
                    DatabaseParameters parameters = new DatabaseParameters();
                    parameters.Add("@userId", this.Id);

                    DataTable dt = Database.Instance.Query("sp_Address_Get", parameters);
                    if (dt != null)
                    {
                        addresses = new List<Address>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            addresses.Add(new Address(dr));
                        }
                    }
                }
                return addresses;
            }
        }

        private List<Contact> contacts = null;
        public List<Contact> Contacts
        {
            get
            {
                if (contacts == null)
                {
                    DatabaseParameters parameters = new DatabaseParameters();
                    parameters.Add("@userId", this.Id);

                    DataTable dt = Database.Instance.Query("sp_Contact_Get", parameters);
                    if (dt != null)
                    {
                        contacts = new List<Contact>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            contacts.Add(new Contact(dr));
                        }
                    }
                }
                return contacts;
            }
        }

        public Address GetAddress(int addressType)
        {
            return GetAddress((Address.AddressType)addressType);
        }

        public Contact GetContact(int contactType)
        {
            return GetContact((Contact.ContactType)contactType);
        }

        public Address GetAddress(Address.AddressType addressType)
        {
            List<Address> list = Addresses;
            if (list != null)
            {
                foreach (Address address in list)
                {
                    if (address.Type == addressType)
                        return address;
                }
            }
            return null;
        }

        public Contact GetContact(Contact.ContactType contactType)
        {
            List<Contact> list = Contacts;
            if (list != null)
            {
                foreach (Contact contact in list)
                {
                    if (contact.Type == contactType)
                        return contact;
                }
            }
            return null;
        }

        public bool Add(Address address)
        {
            addresses = null;

            DatabaseParameters parameters = new DatabaseParameters();            
            parameters.Add("@userId", this.Id);
            parameters.Add("@addressType", address.Type);
            parameters.Add("@address1", address.Address1);
            parameters.Add("@address2", address.Address2);
            parameters.Add("@city", address.City);
            parameters.Add("@state", address.State);
            parameters.Add("@zip", address.Zipcode);

            Database db = Database.Instance;
            object obj = db.Scalar("sp_Address_Insert", parameters);
            if (obj == null)
            {
                this.Exception = db.Exception;
            }
            else
            {
                int id = (int)obj;
                if (id > 0)
                {
                    address.Id = id;
                    return true;
                }
            }
            return false;
        }

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

        public bool CanDonate
        {
            get
            {
                return OrganizationId > 0;
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

        public static CharityUser Select(JsonUser user)
        {
            return Select(user.Id);
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

        private static Random random = new Random((int)DateTime.Now.Ticks);
        private static string RandomString(int Size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static CharityUser Create(string email, out string error)
        {
            error = string.Empty;

            int organizationId = 0;
            int userSecurity = (int)CharityUserSecurityLevel.User;
            string password = RandomString(8);
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
            if (obj == null)
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
            parameters.Add("@acceptedTerms", AcceptedTerms);
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
