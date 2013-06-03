using System;
using bllCharity;
using System.Web.Script.Serialization;

namespace AwsWebApp1  
{
    public class BasePage : System.Web.UI.Page
    {
        protected void UserIsRequired()
        {
            if (CurrentUser == null)
            {
                GotoHome();
            }
            else
            {
                CharityUser user = CharityUser.Select(CurrentUser);
                if (user == null)
                {
                    GotoHome();
                }
            }
        }

        private void GotoHome()
        {
            Response.Redirect("~/index.aspx");
        }

        private string GetCookieData(string key)
        {
            string data = Request.Cookies[key].Value;
            if (!string.IsNullOrEmpty(data))
                return Uri.UnescapeDataString(data);
            return null;
        }

        protected JsonUser CurrentUser
        {
            get
            {
                string data = GetCookieData("user");
                if (!string.IsNullOrEmpty(data))
                    return new JavaScriptSerializer().Deserialize<JsonUser>(data);
                return null;
            }
        }
    }
}