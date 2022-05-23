using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using DoePaAdminDataModel.DPApp;

namespace DoePaAdminDataAdapter.DPApp
{
    public class MasterdataDAL : BaseDAL
    {

        public MasterdataDAL(string connectionString) : base(connectionString)
        {

        }

        public async Task<DataTable> ReadCostCenterAsync(CancellationToken cancellationToken = default)
        {

            SqlCommand cmd = new(Properties.Resources.ReadCostCenters, await GetSqlConnectionAsync(cancellationToken))
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter da = new(cmd);
            DataSet ds = new();

            da.Fill(ds);

            return ds.Tables[0];

        }

        public async Task<IEnumerable<CostCenter>> ReadCostCentersAsync(CancellationToken cancellationToken = default)
        {
            List<CostCenter> listCostCenters = new();

            using (SqlCommand cmd = new(Properties.Resources.ReadCostCenters, await GetSqlConnectionAsync(cancellationToken)))
            {
                cmd.CommandType = CommandType.Text;

                using SqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    CostCenter costCenter = new()
                    {
                        Id = reader.GetInt64("id"),
                        CreatedAt = reader.GetNullableDateTime("created_at"),
                        UpdatedAt = reader.GetNullableDateTime("updated_at"),
                        Number = reader.GetInt32("number"),
                        Name = reader.GetString("name"),
                        Active = reader.GetBoolean("active")
                    };

                    listCostCenters.Add(costCenter);
                }

            }

            return listCostCenters;
        }

    }
}
