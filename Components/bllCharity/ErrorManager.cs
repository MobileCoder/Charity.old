using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bllCharity
{
    public class ErrorManager
    {
        public string Exception { get; set; }
        protected void ReportException(Exception ex)
        {
            Exception = ex.Message;
            Console.WriteLine(ex.Message);
        }
    }
}
