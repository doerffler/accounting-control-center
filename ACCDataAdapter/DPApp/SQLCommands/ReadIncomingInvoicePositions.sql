WITH cte_validinvoices AS
(
	SELECT oi.[id] AS [incoming_invoice_id] FROM [dbo].[incoming_invoices] oi WHERE oi.[date_transferred] < '9999-12-31'
)
SELECT [id]
      ,[created_at]
      ,[updated_at]
      ,[incoming_invoice_id]
      ,[sequence]
      ,[position_text]
      ,[date_service_from]
      ,[date_service_until]
      ,[type_of_settlement]
      ,[hours]
      ,[hourly_rate]
      ,[netto]
      ,[tax]
      ,[tax_percent]
      ,[gross]
      ,[remark]
      ,[hourly_rate_external]
      ,[netto_external]
      ,[tax_percent_external]
      ,[tax_external]
      ,[gross_external]
      ,[invoice_id_external]
      ,[cost_type_id]
      ,[project_id]
      ,[cost_center_id]
      ,[raid]
  FROM [dbo].[incoming_invoice_positions] oip
 WHERE oip.[incoming_invoice_id] IN (SELECT oi.[incoming_invoice_id] FROM cte_validinvoices oi)