using DoePaAdminDataModel.DPApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdminDataAdapter.DPApp
{
    public class OutgoingInvoiceDAL : BaseDAL
    {

        public OutgoingInvoiceDAL(string connectionString) : base(connectionString)
            { }

        public async Task<IEnumerable<OutgoingInvoice>> ReadOutgoingInvoicesAsync(CancellationToken cancellationToken = default)
        {
            List<OutgoingInvoice> outgoingInvoices = new();

            using (SqlCommand cmd = new(Properties.Resources.ReadOutgoingInvoices, await GetSqlConnectionAsync(cancellationToken)))
            {
                cmd.CommandType = CommandType.Text;

                using SqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    OutgoingInvoice outgoingInvoice = new()
                    {
                        Id = reader.GetInt64("id"),
                        CreatedAt = reader.GetNullableDateTime("created_at"),
                        UpdatedAt = reader.GetNullableDateTime("updated_at"),
                        InvoiceNo = reader.GetString("invoice_no"),
                        DateDocument = reader.GetNullableDateTime("date_document"),
                        CreatedBy = reader.GetNullableString("created_by"),
                        InvoiceText = reader.GetNullableString("invoice_text"),
                        Introduction = reader.GetNullableString("introduction"),
                        DateSend = reader.GetNullableDateTime("date_send"),
                        DateServiceFromDefault = reader.GetNullableDateTime("date_service_from_default"),
                        DateServiceUntilDefault = reader.GetNullableDateTime("date_service_until_default"),
                        BusinessYearId = reader.GetInt64("business_year_id"),
                        TransferredFree = reader.GetBoolean("transferred_free"),
                        DateTransferred = reader.GetDateTime("date_transferred"),
                        DatePaid = reader.GetDateTime("date_paid"),
                        Paid = reader.GetBoolean("paid"),
                        Remark = reader.GetNullableString("remark"),
                        InvoiceIdReplacedBy = reader.GetNullableInt64("outgoing_invoice_id_replaced_by"),
                        ContactId = reader.GetNullableInt64("contact_id"),
                        AddressId = reader.GetNullableInt64("address_id"),
                        DepartmentId = reader.GetNullableInt64("department_id"),
                        Client = reader.GetNullableString("client"),
                        StaffIdSignature = reader.GetNullableInt64("staff_id_signature"),
                        StaffIdSendBy = reader.GetNullableInt64("staff_id_send_by"),
                        CostCenterIdDefault = reader.GetNullableInt64("cost_center_id_default"),
                        CostTypeIdDefault = reader.GetNullableInt64("cost_type_id_default"),
                        ProjectIdDefault = reader.GetNullableInt64("project_id_default"),
                        Currency = reader.GetNullableString("currency"),
                        InvoiceSendPer = reader.GetNullableString("invoice_send_per"),
                        Language = reader.GetNullableString("language"),
                        ContractNo = reader.GetNullableString("contract_no"),
                        OrderNo = reader.GetNullableString("order_no"),
                        TermOfPayment = reader.GetNullableString("term_of_payment")
                    };

                    outgoingInvoices.Add(outgoingInvoice);
                }

            }

            return outgoingInvoices;
        }

        public async Task<IEnumerable<OutgoingInvoicePosition>> ReadOutgoingInvoicePositionsAsync(CancellationToken cancellationToken = default)
        {
            List<OutgoingInvoicePosition> outgoingInvoicePositions = new();

            using (SqlCommand cmd = new(Properties.Resources.ReadOutgoingInvoicePositions, await GetSqlConnectionAsync(cancellationToken)))
            {
                cmd.CommandType = CommandType.Text;

                using SqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    OutgoingInvoicePosition outgoingInvoicePosition = new()
                    {
                        Id = reader.GetInt64("id"),
                        CreatedAt = reader.GetNullableDateTime("created_at"),
                        Updated_at = reader.GetNullableDateTime("updated_at"),
                        RelatedInvoiceId = reader.GetInt64("outgoing_invoice_id"),
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
                        HourlyRateExternal = reader.GetNullableDecimal("hourly_rate_external"),
                        NettoExternal = reader.GetNullableDecimal("netto_external"),
                        TaxPercentExternal = reader.GetNullableDecimal("tax_percent_external"),
                        TaxExternal = reader.GetNullableDecimal("tax_external"),
                        GrossExternal = reader.GetNullableDecimal("gross_external"),
                        InvoiceIdExternal = reader.GetNullableInt64("invoice_id_external"),
                        CostTypeId = reader.GetNullableInt64("cost_type_id"),
                        ProjectId = reader.GetNullableInt64("project_id"),
                        CostCenterId = reader.GetNullableInt64("cost_center_id"),
                        Raid = reader.GetNullableInt64("raid")
                    };

                    outgoingInvoicePositions.Add(outgoingInvoicePosition);
                }
            }

            return outgoingInvoicePositions;
        }

    }
}
