using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class Items : List<Item>
    {
        public int Create(Item item)
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@organizationId", item.OrganizationId);
            parameters.Add("@userId", item.UserId);
            parameters.Add("@title", item.Title);
            parameters.Add("@description", item.Description);
            return Database.Instance.NonQuery("sp_Items_Create", parameters);
        }

        public bool List(Item.ItemStatus status)
        {
            this.Clear();

            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@status", (int)status);

            DataTable dt = Database.Instance.Query("sp_Items_Get", parameters);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    this.Add(new Item(dr));
                }
                return true;
            }

            return false;
        }
    }
}
