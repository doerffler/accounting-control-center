﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoePaAdminDataAdapter.Properties {
    using System;
    
    
    /// <summary>
    ///   Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    /// </summary>
    // Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    // -Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    // Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    // mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DoePaAdminDataAdapter.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        ///   Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die INSERT INTO [dbo].[Kostenstellenarten] ([Kostenstellenartbezeichnung])
        ///VALUES
        ///	(&apos;Angestellte Mitarbeiter/innen&apos;),
        ///	(&apos;Erlöse&apos;),
        ///	(&apos;Freie Mitarbeiter/innen&apos;),
        ///	(&apos;Sonstige Kostenstellen&apos;);
        ///
        ///INSERT INTO [dbo].[Taetigkeiten] ([Taetigkeitsbeschreibung])
        ///VALUES
        ///	(&apos;Geschäftsführerin / -Führer&apos;),
        ///	(&apos;Werkstudent&apos;),
        ///	(&apos;Beraterin / Berater&apos;),
        ///	(&apos;Bürokauffrau / -Mann&apos;); ähnelt.
        /// </summary>
        internal static string InitializeMasterdataTables {
            get {
                return ResourceManager.GetString("InitializeMasterdataTables", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[addition1]
        ///      ,[addition2]
        ///      ,[street]
        ///      ,[street_number]
        ///      ,[postcode]
        ///      ,[city]
        ///      ,[district]
        ///      ,[state]
        ///      ,[country]
        ///  FROM [dbo].[addresses] ähnelt.
        /// </summary>
        internal static string ReadAddresses {
            get {
                return ResourceManager.GetString("ReadAddresses", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[invoice_no]
        ///      ,[date_document]
        ///      ,[created_by]
        ///      ,[invoice_text]
        ///      ,[introduction]
        ///      ,[date_send]
        ///      ,[date_service_from_default]
        ///      ,[date_service_until_default]
        ///      ,[business_year_id]
        ///      ,[transferred_free]
        ///      ,[date_transferred]
        ///      ,[date_paid]
        ///      ,[paid]
        ///      ,[remark]
        ///      ,[outgoing_invoice_id_replaced_by]
        ///      ,[contact_id]
        ///      ,[address_id]
        ///      ,[department_id]
        ///      ,[cli [Rest der Zeichenfolge wurde abgeschnitten]&quot;; ähnelt.
        /// </summary>
        internal static string ReadAusgangsrechnung {
            get {
                return ResourceManager.GetString("ReadAusgangsrechnung", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[invoice_no]
        ///      ,[date_document]
        ///      ,[created_by]
        ///      ,[invoice_text]
        ///      ,[introduction]
        ///      ,[date_send]
        ///      ,[date_service_from_default]
        ///      ,[date_service_until_default]
        ///      ,[business_year_id]
        ///      ,[transferred_free]
        ///      ,[date_transferred]
        ///      ,[date_paid]
        ///      ,[paid]
        ///      ,[remark]
        ///      ,[outgoing_invoice_id_replaced_by]
        ///      ,[contact_id]
        ///      ,[address_id]
        ///      ,[department_id]
        ///      ,[cli [Rest der Zeichenfolge wurde abgeschnitten]&quot;; ähnelt.
        /// </summary>
        internal static string ReadAusgangsrechnungen {
            get {
                return ResourceManager.GetString("ReadAusgangsrechnungen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT oip.[id]
        ///      ,oip.[created_at]
        ///      ,oip.[updated_at]
        ///      ,oip.[outgoing_invoice_id]
        ///      ,oip.[sequence]
        ///      ,oip.[position_text]
        ///      ,oip.[date_service_from]
        ///      ,oip.[date_service_until]
        ///      ,oip.[type_of_settlement]
        ///      ,oip.[hours]
        ///      ,oip.[hourly_rate]
        ///      ,oip.[netto]
        ///      ,oip.[tax]
        ///      ,oip.[tax_percent]
        ///      ,oip.[gross]
        ///      ,oip.[remark]
        ///      ,oip.[hourly_rate_external]
        ///      ,oip.[netto_external]
        ///      ,oip.[tax_external]
        ///      ,oip.[tax_perc [Rest der Zeichenfolge wurde abgeschnitten]&quot;; ähnelt.
        /// </summary>
        internal static string ReadAusgangsrechnungspositionen {
            get {
                return ResourceManager.GetString("ReadAusgangsrechnungspositionen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[name]
        ///      ,[date_from]
        ///      ,[date_until]
        ///      ,[invoice_name]
        ///  FROM [dbo].[business_years] ähnelt.
        /// </summary>
        internal static string ReadBusinessYears {
            get {
                return ResourceManager.GetString("ReadBusinessYears", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[short_name]
        ///      ,[name1]
        ///      ,[name2]
        ///      ,[valid_from]
        ///      ,[address_id]
        ///      ,[reverse_charge]
        ///      ,[vat_no]
        ///  FROM [dbo].[companies] ähnelt.
        /// </summary>
        internal static string ReadCompanies {
            get {
                return ResourceManager.GetString("ReadCompanies", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[title]
        ///      ,[first_name]
        ///      ,[last_name]
        ///      ,[language]
        ///      ,[sex]
        ///      ,[position]
        ///      ,[formal]
        ///      ,[staff_id_main_contact]
        ///      ,[staff_contacts]
        ///      ,[email_private]
        ///      ,[address_id_private]
        ///      ,[department_id]
        ///      ,[email]
        ///      ,[telephon]
        ///      ,[mobil]
        ///      ,[remark]
        ///      ,[valid_from]
        ///  FROM [dbo].[contacts] ähnelt.
        /// </summary>
        internal static string ReadContacts {
            get {
                return ResourceManager.GetString("ReadContacts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[number]
        ///      ,[name]
        ///      ,[active]
        ///  FROM [dbo].[cost_centers] ähnelt.
        /// </summary>
        internal static string ReadCostCenters {
            get {
                return ResourceManager.GetString("ReadCostCenters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[name]
        ///  FROM [dbo].[cost_types] ähnelt.
        /// </summary>
        internal static string ReadCostTypes {
            get {
                return ResourceManager.GetString("ReadCostTypes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[short_name]
        ///      ,[name]
        ///      ,[language]
        ///      ,[invoice_relevant]
        ///      ,[supplier]
        ///      ,[client]
        ///      ,[valid_from]
        ///      ,[address_id]
        ///      ,[company_id]
        ///  FROM [dbo].[departments] ähnelt.
        /// </summary>
        internal static string ReadDepartments {
            get {
                return ResourceManager.GetString("ReadDepartments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die WITH cte_validinvoices AS
        ///(
        ///	SELECT oi.[id] AS [outgoing_invoice_id] FROM [dbo].[outgoing_invoices] oi WHERE oi.[date_transferred] &lt; &apos;9999-12-31&apos;
        ///)
        ///SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[outgoing_invoice_id]
        ///      ,[sequence]
        ///      ,[position_text]
        ///      ,[date_service_from]
        ///      ,[date_service_until]
        ///      ,[type_of_settlement]
        ///      ,[hours]
        ///      ,[hourly_rate]
        ///      ,[netto]
        ///      ,[tax]
        ///      ,[tax_percent]
        ///      ,[gross]
        ///      ,[remark]
        ///      ,[hourly_rate_ext [Rest der Zeichenfolge wurde abgeschnitten]&quot;; ähnelt.
        /// </summary>
        internal static string ReadOutgoingInvoicePositions {
            get {
                return ResourceManager.GetString("ReadOutgoingInvoicePositions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[invoice_no]
        ///      ,[date_document]
        ///      ,[created_by]
        ///      ,[invoice_text]
        ///      ,[introduction]
        ///      ,[date_send]
        ///      ,[date_service_from_default]
        ///      ,[date_service_until_default]
        ///      ,[business_year_id]
        ///      ,[transferred_free]
        ///      ,[date_transferred]
        ///      ,[date_paid]
        ///      ,[paid]
        ///      ,[remark]
        ///      ,[outgoing_invoice_id_replaced_by]
        ///      ,[contact_id]
        ///      ,[address_id]
        ///      ,[department_id]
        ///      ,[cli [Rest der Zeichenfolge wurde abgeschnitten]&quot;; ähnelt.
        /// </summary>
        internal static string ReadOutgoingInvoices {
            get {
                return ResourceManager.GetString("ReadOutgoingInvoices", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[short_name]
        ///      ,[name]
        ///      ,[company_id]
        ///  FROM [dbo].[projects] ähnelt.
        /// </summary>
        internal static string ReadProjects {
            get {
                return ResourceManager.GetString("ReadProjects", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die SELECT [id]
        ///      ,[created_at]
        ///      ,[updated_at]
        ///      ,[short_name]
        ///      ,[first_name]
        ///      ,[last_name]
        ///      ,[personal_no]
        ///      ,[role_id]
        ///      ,[cost_center_id]
        ///      ,[address_id]
        ///      ,[entry_date]
        ///      ,[leaving_date]
        ///      ,[birthday]
        ///  FROM [DoePaAppDB].[dbo].[staffs] ähnelt.
        /// </summary>
        internal static string ReadStaff {
            get {
                return ResourceManager.GetString("ReadStaff", resourceCulture);
            }
        }
    }
}
