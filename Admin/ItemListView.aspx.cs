using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using bllCharity;

namespace Admin
{
    public partial class ItemListView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ItemIsEditable = true;
                PopulateStatusLists();
                PopulateItemList();
            }
        }

        protected void StatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateItemList();
        }

        protected void ItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedItem();
        }

        private void PopulateStatusLists()
        {
            PopulateStatusList(StatusList);
            PopulateStatusList(ItemStatus);
        }

        private void PopulateStatusList(DropDownList list)
        {
            list.Items.Clear();
            list.Items.Add(new ListItem("Pending", ((int)Item.ItemStatus.Pending).ToString()));
            list.Items.Add(new ListItem("Active", ((int)Item.ItemStatus.Active).ToString()));
            list.Items.Add(new ListItem("Inactive", ((int)Item.ItemStatus.Inactive).ToString()));
            list.Items.Add(new ListItem("Purchased", ((int)Item.ItemStatus.Purchased).ToString()));
        }

        private Item.ItemStatus Status
        {
            get
            {
                return (Item.ItemStatus)int.Parse(StatusList.SelectedValue);
            }
        }

        private void PopulateItemList()
        {
            ItemList.Items.Clear();

            Items items = new Items();
            if (items.List(Status))
            {
                foreach (Item item in items)
                {
                    ItemList.Items.Add(new ListItem(item.Title, item.Id.ToString()));
                }
            }

            LoadSelectedItem();
        }

        private Item SelectedItem
        {
            get
            {
                if (ItemList.SelectedIndex > -1)
                {
                    int id = int.Parse(ItemList.SelectedValue);
                    return Item.Select(id);
                }
                return null;
            }
        }

        private void ResetItemFields()
        {
            Description.Text = string.Empty;
            ItemStatus.SelectedIndex = -1;
            PurchasedBy.Text = string.Empty;
            PurchasedByRow.Visible = false;
        }

        private void LoadSelectedItem()
        {
            ResetItemFields();

            Item item = SelectedItem;
            if (item != null)
            {
                Description.Text = item.Description;
                ItemStatus.SelectedIndex = ItemStatus.Items.IndexOf(ItemStatus.Items.FindByValue(((int)item.Status).ToString()));
                PurchasedBy.Text = item.PurchasedBy.ToString();
            }
        }

        private bool ItemIsEditable
        {
            set
            {
                Description.Enabled = value;
                ItemStatus.Enabled = value;
            }
        }

        protected void ItemStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            PurchasedByRow.Visible = (((Item.ItemStatus)int.Parse(ItemStatus.SelectedValue)).ToString() == Item.ItemStatus.Purchased.ToString());
        }
    }
}