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

        }
    }
}
