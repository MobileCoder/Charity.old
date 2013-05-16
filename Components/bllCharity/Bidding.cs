using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class Bidding : ErrorManager
    {
        public enum Status
        {
            Success,
            UnknownError = 0,
            ItemNotFound = -1,
            UserNotFound = -2,
            BelowMin = -3,
            TooLow = -4            
        }

        private Bidding()
        {
            DataTable dt = Database.Instance.Query("sp_BidErrors_Get");
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Status status = (Status)dr["Id"];
                    hash[status] = (string)dr["Description"];
                }
            }
        }

        private static Bidding instance = null;
        private Hashtable hash = new Hashtable();

        public static Bidding Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Bidding();
                }
                return instance;
            }
        }

        public string Description(Status status)
        {
            string rc = (string)hash[status];
            if (string.IsNullOrEmpty(rc))
            {
                rc = (string)hash[Status.UnknownError];
            }
            return rc;
        }
    }
}
