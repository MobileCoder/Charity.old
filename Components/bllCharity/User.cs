using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bllCharity
{
    public class User : IDatabaseTable
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string InsertSql 
        {
            get
            {
                return string.Format(
                    "insert into Users (Email) values ('{0}')",
                    Email);
            }
        }
    }
}
