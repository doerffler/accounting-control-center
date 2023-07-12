using ACCDataModel.DPApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACCDataAdapter.DPApp
{
    public class IncomingInvoiceDAL : BaseDAL
    {

        public IncomingInvoiceDAL(string connectionString) : base(connectionString)
        { }

        public async Task<IEnumerable<IncomingInvoice>> ReadIncomingInvoicesAsync(CancellationToken cancellationToken = default)
        {
            return await ReadDPAppObjectAsync<IncomingInvoice>(Properties.Resources.ReadIncomingInvoices, ReadIncomingInvoiceFromDataReader, cancellationToken);
        }

        private IncomingInvoice ReadIncomingInvoiceFromDataReader(DbDataReader reader)
        {
            return new()
            {
                Id = reader.GetInt64("id"),
                CreatedAt = reader.GetNullableDateTime("created_at"),
                UpdatedAt = reader.GetNullableDateTime("updated_at"),
                InvoiceNo = reader.GetString("invoice_no"),
                DateDocument = reader.GetNullableDateTime("date_document"),
                CreatedBy = reader.GetNullableString("created_by"),
                InvoiceText = reader.GetNullableString("invoice_text"),
                DateServiceFromDefault = reader.GetNullableDateTime("date_service_from_default"),
                DateServiceUntilDefault = reader.GetNullableDateTime("date_service_until_default"),
                BusinessYearId = reader.GetInt64("business_year_id"),
                TransferredFree = reader.GetBoolean("transferred_free"),
                DateTransferred = reader.GetDateTime("date_transferred"),
                DatePaid = reader.GetDateTime("date_paid"),
                Paid = reader.GetBoolean("paid"),
                Remark = reader.GetNullableString("remark"),
                InvoiceIdReplacedBy = reader.GetNullableInt64("incoming_invoice_id_replaced_by"),
                AddressId = reader.GetNullableInt64("address_id"),
                DepartmentId = reader.GetNullableInt64("department_id"),
                CostCenterIdDefault = reader.GetNullableInt64("cost_center_id_default"),
                CostTypeIdDefault = reader.GetNullableInt64("cost_type_id_default"),
                ProjectIdDefault = reader.GetNullableInt64("project_id_default"),
            };
        }

        public async Task<IEnumerable<IncomingInvoicePosition>> ReadIncomingInvoicePositionsAsync(CancellationToken cancellationToken = default)
        {
            return await ReadDPAppObjectAsync<IncomingInvoicePosition>(Properties.Resources.ReadIncomingInvoicePositions, ReadIncomingInvoicePositionFromDataReader, cancellationToken);
        }

        private IncomingInvoicePosition ReadIncomingInvoicePositionFromDataReader(DbDataReader reader)
        {
            return new()
            {
                Id = reader.GetInt64("id"),
                CreatedAt = reader.GetNullableDateTime("created_at"),
                UpdatedAt = reader.GetNullableDateTime("updated_at"),
                RelatedInvoiceId = reader.GetInt32("incoming_invoice_id"),
                Sequence = reader.GetInt32("sequence"),
                PositionText = reader.GetNullableString("position_text"),
                DateServiceFrom = reader.GetDateTime("date_service_from"),
                DateServiceUntil = reader.GetDateTime("date_service_until"),
                TypeOfSettlement = reader.GetNullableString("type_of_settlement"),
                Hours = reader.GetNullableDecimal("hours"),
                HourlyRate = reader.GetNullableDecimal("hourly_rate"),
                Netto = reader.GetNullableDecimal("netto"),
                Tax = reader.GetNullableDecimal("tax"),
                TaxPercent = reader.GetNullableDecimal("tax_percent"),
                Gross = reader.GetNullableDecimal("gross"),
                Remark = reader.GetNullableString("remark"),
                CostTypeId = reader.GetNullableInt64("cost_type_id"),
                ProjectId = reader.GetNullableInt64("project_id"),
                CostCenterId = reader.GetNullableInt64("cost_center_id"),
            };
        }

    }
}
