using DoePaAdminDataAdapter.DPApp.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DoePaAdminDataAdapter.DPApp
{
    public class MasterdataDAL : BaseDAL
    {

        public MasterdataDAL(string connectionString) : base(connectionString)
        {

        }

        public async Task<DataTable> ReadCostCenterAsync()
        {

            SqlCommand cmd = new(Properties.Resources.ReadCostCenters, await GetSqlConnection())
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter da = new(cmd);
            DataSet ds = new();

            da.Fill(ds);

            return ds.Tables[0];

        }

    }
}
