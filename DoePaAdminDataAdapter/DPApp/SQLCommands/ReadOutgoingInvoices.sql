SELECT [id]
      ,[created_at]
      ,[updated_at]
      ,[invoice_no]
      ,[date_document]
      ,[created_by]
      ,[invoice_text]
      ,[introduction]
      ,[date_send]
      ,[date_service_from_default]
      ,[date_service_until_default]
      ,[business_year_id]
      ,[transferred_free]
      ,[date_transferred]
      ,[date_paid]
      ,[paid]
      ,[remark]
      ,[outgoing_invoice_id_replaced_by]
      ,[contact_id]
      ,[address_id]
      ,[department_id]
      ,[client]
      ,[staff_id_signature]
      ,[staff_id_send_by]
      ,[cost_center_id_default]
      ,[cost_type_id_default]
      ,[project_id_default]
      ,[currency]
      ,[invoice_send_per]
      ,[language]
      ,[contract_no]
      ,[order_no]
      ,[term_of_payment]
  FROM [dbo].[outgoing_invoices]
 WHERE [date_transferred] < '9999-12-31'