using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataAdapter
{
    public static class ReaderExtensions
    {

        public static DateTime? GetNullableDateTime(this SqlDataReader reader, string name)
        {

            int col = reader.GetOrdinal(name);
            return reader.IsDBNull(col) ?
                null :
                reader.GetDateTime(col);

        }

        public static long? GetNullableInt64(this SqlDataReader reader, string name)
        {

            int col = reader.GetOrdinal(name);
            return reader.IsDBNull(col) ?
                null :
                reader.GetInt64(col);

        }

        public static string GetNullableString(this SqlDataReader reader, string name)
        {

            int col = reader.GetOrdinal(name);
            return reader.IsDBNull(col) ?
                null :
                reader.GetString(col);

        }

    }
}
