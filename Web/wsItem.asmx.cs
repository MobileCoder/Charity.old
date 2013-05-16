using bllCharity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AwsWebApp1
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class wsItem : System.Web.Services.WebService
    {
        [WebMethod]
        public string Create(int userId, string title, string description, DateTime startDate, DateTime expireDate, decimal cashValue, decimal initialBid)
        {
            JsonItem rc = new JsonItem();
            Item item = new Item();
            item.UserId = userId;
            item.Title = title;
            item.Description = description;
            item.StartDate = startDate;
            item.EndDate = expireDate;
            item.CashValue = cashValue;
            item.InitialBid = initialBid;

            if (item.Create())
            {
                item.Status = Item.ItemStatus.Active;
                item.Update();

                rc.Id.Value = item.Id;
                rc.IsValid.Value = true;
            }
            else
            {
                rc.Message.Value = item.Exception;
            }

            return rc.ToString();
        }

        [WebMethod]
        public string AddImage(int userId, int itemId, string description)
        {
            JsonItem rc = new JsonItem();
            Item item = Item.Select(itemId);
            if (item.Images.AddImage(userId, description))
            {
                rc.Id.Value = item.Id;
                rc.IsValid.Value = true;
            }
            else
            {
                rc.Message.Value = item.Exception;
            }
            return rc.ToString();
        }

        [WebMethod]
        public string Bid(int userId, int itemId, decimal amount)
        {
            JsonBid rc = new JsonBid();

            Item item = Item.Select(itemId);
            if (item == null)
            {
                rc.Message.Value = "Item not found";
            }
            else
            {
                string ip = "";
                Bidding.Status status = item.Bid(userId, ip, amount);
                if (status == Bidding.Status.Success)
                {
                    rc.IsValid.Value = true;
                }
                else 
                {
                    rc.Message.Value = Bidding.Instance.Description(status);
                }

                item = Item.Select(itemId);
                rc.Amount.Value = item.CurrentBid;
            }

            return rc.ToString();
        }
    }
}
