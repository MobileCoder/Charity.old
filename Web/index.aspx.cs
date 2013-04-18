using bllCharity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AwsWebApp1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = UserManager.FindUser("chberry@gmail.com");
            if (user != null)
            {
                Response.Write(user.FirstName + " " + user.LastName);
            }
        }
    }
}