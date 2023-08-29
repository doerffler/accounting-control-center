using ACCDataModel.DPApp;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ACCDataAdapter.DPApp
{
    public abstract class BaseDAL
    {
        // TODO: Überschreiben zu Pgsql
        protected string ConnectionString { get; set; }
                
        public BaseDAL(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected async Task<NpgsqlConnection> GetSqlConnectionAsync(CancellationToken cancellationToken = default)
        {
            NpgsqlConnection connection;

            connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync(cancellationToken);
            
            return connection;
        }

        protected async Task<NpgsqlDataReader> CreateReaderFromCommandAsync(NpgsqlCommand cmd, CancellationToken cancellationToken = default)
        {

            cmd.Connection = await GetSqlConnectionAsync(cancellationToken);

            NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync(cancellationToken);
            return rdr;

        }

        protected async Task<IEnumerable<T>> ReadDPAppObjectAsync<T>(string commandText, ReadDPObjectFromReaderDelegate<T> readDataDelegate, CancellationToken cancellationToken = default) where T : DPAppObject
        {

            List<T> listObjects = new();

            using (NpgsqlConnection connection = await GetSqlConnectionAsync(cancellationToken))
            {

                using (NpgsqlCommand cmd = new(commandText, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);

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
