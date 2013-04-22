using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bllCharity
{
    public class ErrorManager
    {
        protected void ReportException(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
