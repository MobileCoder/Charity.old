
using bllCharity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace AwsWebApp1
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class wsUsers : System.Web.Services.WebService
    {
        private bool ValidateUser(CharityUser user, JsonObject settings)
        {
            return ValidateUser(user, null, settings);
        }

        private bool ValidateUser(CharityUser user, string password, JsonObject settings)
        {
            settings.IsValid = (user != null);
            
            if (!settings.IsValid)
            {
                settings.Message = "User not found";
                return false;
            }            

            if (!string.IsNullOrEmpty(password))
            {
                settings.IsValid = user.ValidatePassword(password);
                if (!settings.IsValid)
                {
                    settings.Message = "Invalid password";
                    return false;
                }
            }

            if (settings is JsonUser)
            {
                JsonUser u = (JsonUser)settings;
                u.IsActive = (user.Status == CharityUser.CharityUserStatus.Active);
                if (!u.IsActive)
                {
                    settings.Message = "Inactive user";
                    return false;
                }

                u.Populate(user);
            }
            
            return true;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ValidateUserById(int Id)
        {
            JsonUser settings = new JsonUser();
            CharityUser user = CharityUser.Select(Id);
            ValidateUser(user, (user != null) ? user.Password : null, settings);
            string rc = settings.ToString();
            return rc;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ValidateUser(string email, string password)
        {
            JsonUser settings = new JsonUser();
            CharityUser user = CharityUser.Select(email);
            ValidateUser(user, password, settings);
            return settings.ToString();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ForgotPassword(string email)
        {
            JsonUser settings = new JsonUser();
            CharityUser user = CharityUser.Select(email);
            ValidateUser(user, user.Password, settings);
            if (user != null)
            {
                settings.IsValid = user.ResetPassword();
                settings.Message = "Your email address will be notified of your reset password";
            }
            return settings.ToString();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string RegisterUser(string email)
        {
            JsonUser settings = new JsonUser();
            string error = string.Empty;

            CharityUser user = CharityUser.Select(email);
            if (user != null)
            {
                settings.Message = "User already exists";
            }
            else
            {
                user = CharityUser.Create(email, out error);
                if (user != null)
                {
                    string emailBody = "Your password is " + user.Password;
                    Utility.Email.Send(email, user.DisplayName, "Your Password", emailBody);

                    settings.Id = user.Id;
                    settings.DisplayName = user.DisplayName;
                    settings.IsValid = true;
                }
                else
                {
                    settings.Message = error;
                }
            }
            return settings.ToString();
        }

        [WebMethod]
        public bool UpdateUser(int Id, string FirstName, string LastName, string Password)
        {
            CharityUser user = CharityUser.Select(Id);
            if (user != null)
            {
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.SetPassword(Password);
                return user.Update();
            }
            return false;
        }

        [WebMethod]
        public string GetAddress(int userId, int addressType)
        {
            JsonUser rc = new JsonUser();
            CharityUser user = CharityUser.Select(userId);
            if (ValidateUser(user, rc))
            {
                Address address = user.GetAddress(addressType);
                if (address != null)
                {
                    return address.ToString();
                }
            }
            return null;
        }

        [WebMethod]
        public string UpdateAddress(int userId, int addressType, string address1, string address2, string city, string state, string zipcode)
        {
            JsonUser rc = new JsonUser();
            CharityUser user = CharityUser.Select(userId);
            if (ValidateUser(user, rc))
            {
                Address address = user.GetAddress(addressType);
                if (address == null)
                {
                    address = new Address();
                    address.UserId = userId;
                    address.Type = (Address.AddressType)addressType;
                    address.Address1 = address1;
                    address.Address2 = address2;
                    address.City = city;
                    address.State = state;
                    address.Zipcode = zipcode;
                    rc.IsValid = address.Insert();
                    rc.Message = address.Exception;
                }
                else
                {
                    address.Address1 = address1;
                    address.Address2 = address2;
                    address.City = city;
                    address.State = state;
                    address.Zipcode = zipcode;
                    rc.IsValid = address.Update();
                    rc.Message = address.Exception;
                }
            }
            return rc.ToString();
        }

        [WebMethod]
        public string GetContact(int userId, int contactType)
        {
            JsonObject rc = new JsonObject();
            CharityUser user = CharityUser.Select(userId);
            if (ValidateUser(user, rc))
            {
                Contact contact = user.GetContact(contactType);
                if (contact != null)
                {
                    return contact.ToString();
                }
            }
            return null;
        }

        [WebMethod]
        public string UpdateContact(int userId, int contactType, string number)
        {
            JsonObject rc = new JsonObject();
            CharityUser user = CharityUser.Select(userId);
            if (ValidateUser(user, rc))
            {
                Contact contact = user.GetContact(contactType);
                if (contact == null)
                {
                    contact = new Contact();
                    contact.UserId = userId;
                    contact.Type = (Contact.ContactType)contactType;
                    contact.Number = number;
                    rc.IsValid = contact.Insert();
                    rc.Message = contact.Exception;
                }
                else
                {
                    contact.Number = number;
                    rc.IsValid = contact.Update();
                    rc.Message = contact.Exception;
                }
            }
            return rc.ToString();
        }

        [WebMethod]
        public string AcceptTerms(int userId)
        {
            JsonObject rc = new JsonObject();
            CharityUser user = CharityUser.Select(userId);
            if (ValidateUser(user, rc))
            {
                user.AcceptedTerms = true;
                rc.IsValid = user.Update();
                rc.Message = user.Exception;
            }
            return rc.ToString();
        }
    }
}
