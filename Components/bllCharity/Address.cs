using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class Address : JsonObject
    {
        public enum AddressType
        {
            Billing = 1,
            Shipping = 2
        }

        public Address()
        {
        }

        public Address(DataRow dr)
        {
            Id = (int)dr["Id"];
            UserId = (int)dr["UserId"];
            Type = (AddressType)dr["AddressTypeId"];
            Address1 = (string)dr["Address1"];
            Address2 = (string)dr["Address2"];
            City = (string)dr["City"];
            State = (string)dr["State"];
            Zipcode = (string)dr["Zipcode"];
        }

        public int UserId { get; set; }
        public AddressType Type { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        public bool Insert()
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@userId", this.UserId);
            parameters.Add("@addressType", this.Type);
            parameters.Add("@address1", this.Address1);
            parameters.Add("@address2", this.Address2);
            parameters.Add("@city", this.City);
            parameters.Add("@state", this.State);
            parameters.Add("@zipcode", this.Zipcode);

            return (Database.Instance.NonQuery("sp_Address_Insert", parameters) > 0);
        }

        public bool Update()
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@Id", this.Id);
            parameters.Add("@userId", this.UserId);
            parameters.Add("@addressType", this.Type);
            parameters.Add("@address1", this.Address1);
            parameters.Add("@address2", this.Address2);
            parameters.Add("@city", this.City);
            parameters.Add("@state", this.State);
            parameters.Add("@zipcode", this.Zipcode);

            return (Database.Instance.NonQuery("sp_Address_Update", parameters) > 0);
        }
    }
}
