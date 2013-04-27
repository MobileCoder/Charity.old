using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class Item
    {
        public enum ItemStatus
        {
            Pending = 1,
            Active = 2,
            Inactive = 3,
            Purchased = 4
        }

        public Item()
        {
        }

        public Item(DataRow dr)
        {
            Id = (int)dr["Id"];
            OrganizationId = (int)dr["OrganizationId"];
            UserId = (int)dr["UserId"];
            Title = (string)dr["Title"];
            Description = (string)dr["Description"];
            StartDate = (DateTime)dr["StartDate"];
            ExpireDate = (DateTime)dr["ExpireDate"];
            Status = (ItemStatus)(int)dr["Status"];
            PurchasedBy = (int)dr["PurchasedBy"];
            Amount = (decimal)dr["Amount"];
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public string Title{ get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public ItemStatus Status { get; set; }
        public int PurchasedBy { get; set; }
        public decimal Amount { get; set; }
        
        public static Item Select(int id)
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@id", id);

            DataTable dt = Database.Instance.Query("sp_Items_Get", parameters);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    return new Item(dr);
                }
            }
            return null;
        }

        public bool Update()
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@id", Id);
            parameters.Add("@status", Status);
            parameters.Add("@purchasedBy", PurchasedBy);
            return (Database.Instance.NonQuery("sp_Items_Update", parameters) == 1);
        }
    }
}
