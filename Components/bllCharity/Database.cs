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
    public class DatabaseParameters : Dictionary<string, object>
    {
    }

    public class Database : ErrorManager
    {
        private static Database instance;
        public static ConnectionStringSettings Configuration { get; set; }

        private Database()
        {
        }

        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                }
                return instance;
            }
        }        

        private DbConnection Open()
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

        private DataTable ExecuteQuery(DbCommand command)
        {
            DbDataReader dr = command.ExecuteReader();
            if (dr != null)
            {
                DataTable dt = new DataTable();
                try
                {
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

                    if (dt.Rows.Count > 0)
                        return dt;

                    dt.Dispose();
                }
                catch (Exception)
                {
                    dt.Dispose();
                    throw;
                }
            }
            return null;
        }

        public DataTable Query(string name)
        {
            return Query(name, null);
        }

        public DataTable Query(string name, DatabaseParameters parameters)
        {
            DbConnection connection = Open();
            if (connection != null)
            {
                try
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = name;

                    if (parameters != null)
                    {
                        foreach (string key in parameters.Keys)
                        {
                            DbParameter parameter = command.CreateParameter();
                            parameter.ParameterName = key;
                            parameter.Value = parameters[key];
                            command.Parameters.Add(parameter);
                        }
                    }

                    return ExecuteQuery(command);
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

        public int NonQuery(string name)
        {
            return NonQuery(name, null);
        }

        public int NonQuery(string name, DatabaseParameters parameters)
        {
            DbConnection connection = Open();
            if (connection != null)
            {
                try
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = name;

                    if (parameters != null)
                    {
                        foreach (string key in parameters.Keys)
                        {
                            DbParameter parameter = command.CreateParameter();
                            parameter.ParameterName = key;
                            parameter.Value = parameters[key];
                            command.Parameters.Add(parameter);
                        }
                    }

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