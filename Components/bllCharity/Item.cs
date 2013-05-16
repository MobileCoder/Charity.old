using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class Item : ErrorManager
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
            Images = new ItemImages();
        }

        public Item(int id) : this()
        {
            Images = new ItemImages(id);
        }

        public Item(DataRow dr) : this()
        {
            Id = (int)dr["Id"];
            OrganizationId = (int)dr["OrganizationId"];
            UserId = (int)dr["UserId"];
            Title = (string)dr["Title"];
            Description = (string)dr["Description"];
            StartDate = (DateTime)dr["StartDate"];
            EndDate = (DateTime)dr["EndDate"];
            CashValue = (decimal)dr["CashValue"];
            InitialBid = (decimal)dr["InitialBid"];            
            Status = (ItemStatus)(int)dr["Status"];
            PurchasedBy = (int)dr["PurchasedBy"];

            BidCount = (int)dr["BidCount"];
            CurrentBid = (decimal)dr["CurrentBid"];

            Images = ItemImages.SelectByItemId(Id);
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public string Title{ get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal CashValue { get; set; }
        public decimal InitialBid { get; set; }
        public decimal BidIncrement { get; set; }
        public decimal CurrentBid { get; set; }
        public ItemStatus Status { get; set; }
        public int PurchasedBy { get; set; }
        public int BidCount { get; set; }
        public ItemImages Images { get; set; }
        
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

        public Bidding.Status Bid(int userId, string ip, decimal amount)
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@itemId", this.Id);
            parameters.Add("@userId", userId);
            parameters.Add("@amount", amount);
            parameters.Add("@clientIp", ip);

            Database db = Database.Instance;
            object obj = db.Scalar("sp_Bids_Insert", parameters);
            if (obj == null)
            {
                this.Exception = db.Exception;
            }
            else
            {
                int bid = (int)obj;
                if (bid > 0)
                {
                    return Bidding.Status.Success;
                }
                else
                {
                    return (Bidding.Status)bid;
                }
            }
            return Bidding.Status.UnknownError;
        }

        public bool Create()
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@organizationId", OrganizationId);
            parameters.Add("@userId", UserId);
            parameters.Add("@title", Title);
            parameters.Add("@description", Description);
            parameters.Add("@startDate", StartDate);
            parameters.Add("@endDate", EndDate);
            parameters.Add("@cashValue", CashValue);
            parameters.Add("@initialBid", InitialBid);
            parameters.Add("@bidIncrement", BidIncrement);

            Database db = Database.Instance;
            object obj = db.Scalar("sp_Items_Create", parameters);
            if (obj == null)
            {
                this.Exception = db.Exception;
            }
            else
            {
                int id = (int)obj;
                if (id > 0)
                {
                    Id = id;
                    return true;
                }
            }
            return false;
        }
    }
}
