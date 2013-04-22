using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class Items : List<Item>
    {
        public bool Load()
        {
            this.Clear();

            DataTable dt = Database.Instance.Query("SELECT * FROM Items");
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
