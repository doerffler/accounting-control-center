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
                
        public BaseDAL(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected async Task<SqlConnection> GetSqlConnectionAsync(CancellationToken cancellationToken = default)
        {
            SqlConnection connection;

            connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            return connection;
        }

        protected async Task<SqlDataReader> CreateReaderFromCommandAsync(SqlCommand cmd, CancellationToken cancellationToken = default)
        {

            cmd.Connection = await GetSqlConnectionAsync(cancellationToken);

            SqlDataReader rdr = await cmd.ExecuteReaderAsync(cancellationToken);
            return rdr;

        }

    }
}
