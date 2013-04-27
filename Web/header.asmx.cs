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
    public class header : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ValidateUser(string email, string password)
        {
            ClientSettings settings = new ClientSettings();
            CharityUser user = CharityUser.Select(email);
            if (user == null)
            {
                settings.Message = "User not found";
            }
            else
            {
                if (!user.ValidatePassword(password))
                {
                    settings.Message = "Invalid password";
                }
                else
                {
                    settings.IsValid = true;
                    settings.DisplayName = user.DisplayName;
                }
            }

            return settings.ToString();
        }
    }
}
