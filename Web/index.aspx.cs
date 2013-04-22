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
            if (!this.IsPostBack)
            {
                Items items = new Items();
                if (items.Load())
                {
                    foreach (Item item in items)
                    {
                        ItemWebUserControl control = (ItemWebUserControl)LoadControl("~/controls/ItemWebUserControl.ascx");
                        control.Title = item.Title;
                        control.Description = item.Description;

                        ItemsDiv.Controls.Add(control);
                    }
                }
            }
        }
    }
}