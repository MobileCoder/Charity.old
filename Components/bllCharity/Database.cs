using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bllCharity
{
    public abstract class DatabaseTable : ErrorManager
    {
        static public string FindSql(string key) { return string.Empty; }
        static public string InsertSql(string key) { return string.Empty; }
        public abstract bool LoadData(DataRow dr);
        public abstract string UpdateSql { get; }
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

        static protected DataSet Query(string sql)
        {
            DbConnection connection = Open();
            if (connection != null)
            {
                try
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    DbDataReader dr = command.ExecuteReader();
                    if (dr != null)
                    {
                        DataSet ds = new DataSet();
                        try
                        {
                            DataTable dt = new DataTable();
                            ds.Tables.Add(dt);
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                dt.Columns.Add(dr.GetName(i), dr.GetFieldType(i));
                            }

                            IEnumerator ienum = dr.GetEnumerator();
                            ienum.MoveNext();
                            while (ienum.Current != null)
                            {
                                DataRow r = dt.NewRow();
                                for (int j = 0; j < dr.FieldCount; j++)
                                {
                                    r[j] = dr.GetValue(j);
                                }
                                dt.Rows.Add(r);
                                ienum.MoveNext();
                            }

                            if (ds.Tables[0].Rows.Count > 0)
                                return ds;

                            ds.Dispose();
                        }
                        catch (Exception)
                        {
                            ds.Dispose();
                            throw;
                        }
                    }
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
            return null;
        }

        static protected int NonQuery(string sql)
        {
            DbConnection connection = Open();
            if (connection != null)
            {
                try
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    return command.ExecuteNonQuery();
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
            return -1;
        }
    }
}
