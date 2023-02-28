using ACCDataModel.DPApp;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ACCDataAdapter.DPApp
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

        protected async Task<IEnumerable<T>> ReadDPAppObjectAsync<T>(string commandText, ReadDPObjectFromReaderDelegate<T> readDataDelegate, CancellationToken cancellationToken = default) where T : DPAppObject
        {

            List<T> listObjects = new();

            using (SqlConnection connection = await GetSqlConnectionAsync(cancellationToken))
            {

                using (SqlCommand cmd = new(commandText, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    using SqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);

                    while (await reader.ReadAsync(cancellationToken))
                    {

                        listObjects.Add(readDataDelegate(reader));

                    }

                }

            }

            return listObjects;

        }

    }
}
