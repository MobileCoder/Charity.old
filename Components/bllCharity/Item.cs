using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class Item
    {
        public Item(DataRow dr)
        {
            Id = (int)dr["Id"];
            OrganizationId = (int)dr["OrganizationId"];
            UserId = (int)dr["UserId"];
            Title = (string)dr["Title"];
            Description = (string)dr["Description"];
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public string Title{ get; set; }
        public string Description { get; set; }
    }
}
