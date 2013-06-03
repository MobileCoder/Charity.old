using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class Contact : JsonObject
    {
        public enum ContactType
        {
            Home = 1,
            Mobile = 2,
            Work = 3
        }

        public Contact()
        {
        }

        public Contact(DataRow dr)
        {
            Id = (int)dr["Id"];
            UserId = (int)dr["UserId"];
            Type = (ContactType)dr["ContactTypeId"];
            Number = (string)dr["Number"];
        }

        public int UserId { get; set; }
        public ContactType Type { get; set; }
        public string Number { get; set; }

        public bool Insert()
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@userId", this.UserId);
            parameters.Add("@contactType", this.Type);
            parameters.Add("@number", this.Number);

            return (Database.Instance.NonQuery("sp_Contact_Insert", parameters) > 0);
        }

        public bool Update()
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@Id", this.Id);
            parameters.Add("@userId", this.UserId);
            parameters.Add("@contactType", this.Type);
            parameters.Add("@number", this.Number);

            return (Database.Instance.NonQuery("sp_Contact_Update", parameters) > 0);
        }
    }
}
