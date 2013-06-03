using bllCharity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AwsWebApp1
{
    public partial class itemDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["Id"]))
                {
                    Response.Redirect("~/index.aspx");
                }
                else
                {
                    int itemId = Convert.ToInt32(Request["Id"]);
                    Item item = Item.Select(itemId);
                    if (item == null)
                    {
                        Response.Redirect("~/index.aspx");
                    }
                    else
                    {
                        ItemTitle.Text = item.Title;
                        ItemDescription.Text = item.Description;
                        images.ItemId = item.Id;
                        images.Images = item.Images;
                    }
                }
            }
        }
    }
}