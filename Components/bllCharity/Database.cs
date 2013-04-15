using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bllCharity
{
    public interface IDatabaseTable
    {
        string InsertSql { get; }
    }

    public class Database : ErrorManager
    {
        static public ConnectionStringSettings Configuration { get; set; }

        static private DbConnection Open()
        {
            try
            {
                DbConnection connection = null;
                DbProviderFactory factory = DbProviderFactories.GetFactory(Configuration.ProviderName);
                connection = factory.CreateConnection();
                connection.ConnectionString = Configuration.ConnectionString;
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                ReportException(ex);
            }

            return null;
        }

        static protected int NonQuery(string sql)
        {
            int rc = -1;

            DbConnection connection = Open();
            if (connection != null)
            {
                try
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    rc = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ReportException(ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return rc;
        }
    }
}
