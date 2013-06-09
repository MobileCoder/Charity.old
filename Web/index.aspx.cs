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
                if (Request["id"] != null)
                {
                    Item item = Item.Select(int.Parse(Request["id"]));
                    AddItem(item);
                }
                else
                {
                    Items items = new Items();
                    items.List(Item.ItemStatus.Pending);
                    foreach (Item item in items)
                    {
                        AddItem(item);
                    }
                }
            }
        }

        private void AddItem(Item item)
        {
            ItemWebUserControl control = (ItemWebUserControl)LoadControl("~/controls/ItemWebUserControl.ascx");
            control.SetItem(item);
            ItemsDiv.Controls.Add(control);
        }
    }
}