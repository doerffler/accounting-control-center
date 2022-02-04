using DoePaAdminDataAdapter.DPApp.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdminDataAdapter.DPApp
{
    public abstract class BaseDAL
    {

        protected string ConnectionString { get; set; }

        protected SqlConnection Connection { get; set; }

        public BaseDAL(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected async Task<SqlConnection> GetSqlConnectionAsync(CancellationToken cancellationToken = default)
        {
            if (Connection == null || Connection.State != ConnectionState.Open)
            {
                Connection = new SqlConnection(ConnectionString);
                await Connection.OpenAsync(cancellationToken);
            }

            return Connection;
        }

        protected async Task<SqlDataReader> CreateReaderFromCommandAsync(SqlCommand cmd, CancellationToken cancellationToken = default)
        {

            cmd.Connection = await GetSqlConnectionAsync(cancellationToken);

            SqlDataReader rdr = await cmd.ExecuteReaderAsync(cancellationToken);
            return rdr;

        }

        protected static T GetDataItem<T>(SqlDataReader rdr) where T : IDPAppModel, new()
        {
            T newDataItem = new();
            
            Type t = newDataItem.GetType();
            PropertyInfo[] prprts = t.GetProperties();
            
            foreach(PropertyInfo currentProperty in prprts)
            {

                DBColumnAttribute attr = currentProperty.GetCustomAttribute<DBColumnAttribute>();

                if (attr != null)
                {
                    currentProperty.SetValue(newDataItem, rdr.GetValue(attr.ColumnName));
                }

            }

            return newDataItem;

        }

    }
}
