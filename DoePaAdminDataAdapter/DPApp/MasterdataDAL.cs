using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
            return await ReadDPAppObjectAsync<CostCenter>(Properties.Resources.ReadCostCenters, ReadCostCenterFromDbReader, cancellationToken);
        }

        private CostCenter ReadCostCenterFromDbReader(DbDataReader reader)
        {
            return new()
            {
                Id = reader.GetInt64("id"),
                CreatedAt = reader.GetNullableDateTime("created_at"),
                UpdatedAt = reader.GetNullableDateTime("updated_at"),
                Number = reader.GetInt32("number"),
                Name = reader.GetString("name"),
                Active = reader.GetBoolean("active")
            };
        }

        public async Task<IEnumerable<CostType>> ReadCostTypesAsync(CancellationToken cancellationToken = default)
        {
            return await ReadDPAppObjectAsync<CostType>(Properties.Resources.ReadCostTypes, ReadCostTypeFromDbReader, cancellationToken);
        }

        private CostType ReadCostTypeFromDbReader(DbDataReader reader)
        {
            return new()
            {
                Id = reader.GetInt64("id"),
                CreatedAt = reader.GetNullableDateTime("created_at"),
                UpdatedAt = reader.GetNullableDateTime("updated_at"),
                Name = reader.GetString("name")
            };
        }

        public async Task<IEnumerable<Project>> ReadProjectsAsync(CancellationToken cancellationToken = default)
        {
            return await ReadDPAppObjectAsync<Project>(Properties.Resources.ReadProjects, ReadProjectFromDbReader, cancellationToken);
        }

        private Project ReadProjectFromDbReader(DbDataReader reader)
        {
            return new()
            {
                Id = reader.GetInt64("id"),
                CreatedAt = reader.GetNullableDateTime("created_at"),
                UpdatedAt = reader.GetNullableDateTime("updated_at"),
                ShortName = reader.GetString("short_name"),
                Name = reader.GetString("name"),
                CompanyId = reader.GetInt64("company_id")
            };
        }

        public async Task<IEnumerable<BusinessYear>> ReadBusinessYearsAsync(CancellationToken cancellationToken = default)
        {
            return await ReadDPAppObjectAsync<BusinessYear>(Properties.Resources.ReadBusinessYears, ReadBusinessYearFromDbReader, cancellationToken);
        }

        private BusinessYear ReadBusinessYearFromDbReader(DbDataReader reader)
        {
            return new()
            {
                Id = reader.GetInt64("id"),
                CreatedAt = reader.GetNullableDateTime("created_at"),
                UpdatedAt = reader.GetNullableDateTime("updated_at"),
                Name = reader.GetString("name"),
                DateFrom = reader.GetDateTime("date_from"),
                DateUntil = reader.GetDateTime("date_until"),
                InvoiceName = reader.GetNullableString("invoice_name")
            };
        }

        public async Task<IEnumerable<Contact>> ReadContactsAsync(CancellationToken cancellationToken = default)
        {
            return await ReadDPAppObjectAsync<Contact>(Properties.Resources.ReadContacts, ReadContactFromDbReader, cancellationToken);
        }

        private Contact ReadContactFromDbReader(DbDataReader reader)
        {
            return new()
            {
                Id = reader.GetInt64("id"),
                CreatedAt = reader.GetNullableDateTime("created_at"),
                UpdatedAt = reader.GetNullableDateTime("updated_at"),
                Title = reader.GetNullableString("title"),
                FirstName = reader.GetNullableString("first_name"),
                LastName = reader.GetString("last_name"),
                Language = reader.GetString("language"),
                Sex = reader.GetString("sex"),
                Position = reader.GetNullableString("position"),
                Formal = reader.GetBoolean("formal"),
                StaffIdMainContact = reader.GetInt64("staff_id_main_contact"),
                StaffContacts = reader.GetNullableString("staff_contacts"),
                EmailPrivate = reader.GetNullableString("email_private"),
                AddressIdPrivate = reader.GetNullableInt64("address_id_private"),
                DepartmentId = reader.GetNullableInt64("department_id"),
                Email = reader.GetNullableString("email"),
                Telephon = reader.GetNullableString("telephon"),
                Mobil= reader.GetNullableString("mobil"),
                Remark = reader.GetNullableString("remark"),
                ValidFrom = reader.GetNullableDateTime("valid_from")
            };
        }

        public async Task<IEnumerable<Address>> ReadAddressesAsync(CancellationToken cancellationToken = default)
        {
            return await ReadDPAppObjectAsync<Address>(Properties.Resources.ReadAddresses, ReadAddressFromDbReader, cancellationToken);
        }

        private Address ReadAddressFromDbReader(DbDataReader reader)
        {
            return new()
            {
                Id = reader.GetInt64("id"),
                CreatedAt = reader.GetNullableDateTime("created_at"),
                UpdatedAt = reader.GetNullableDateTime("updated_at"),
                Addition1 = reader.GetNullableString("addition1"),
                Addition2 = reader.GetNullableString("addition2"),
                Street = reader.GetNullableString("street"),
                StreetNumber = reader.GetNullableString("street_number"),
                Postcode = reader.GetNullableString("postcode"),
                City = reader.GetNullableString("city"),
                District = reader.GetNullableString("district"),
                State = reader.GetNullableString("state"),
                Country = reader.GetNullableString("country")
            };
        }

        public async Task<IEnumerable<Department>> ReadDepartmentsAsync(CancellationToken cancellationToken = default)
        {
            return await ReadDPAppObjectAsync<Department>(Properties.Resources.ReadDepartments, ReadDepartmentFromDbReader, cancellationToken);
        }

        private Department ReadDepartmentFromDbReader(DbDataReader reader)
        {
            return new()
            {
                Id = reader.GetInt64("id"),
                CreatedAt = reader.GetNullableDateTime("created_at"),
                UpdatedAt = reader.GetNullableDateTime("updated_at"),
                ShortName = reader.GetString("short_name"),
                Name = reader.GetNullableString("name"),
                Language = reader.GetString("language"),
                InvoiceRelevant = reader.GetBoolean("invoice_relevant"),
                Supplier = reader.GetBoolean("supplier"),
                Client = reader.GetBoolean("client"),
                ValidFrom = reader.GetDateTime("valid_from"),
                AddressId = reader.GetNullableInt64("address_id"),
                CompanyId = reader.GetNullableInt64("company_id")
            };
        }

    }
}
