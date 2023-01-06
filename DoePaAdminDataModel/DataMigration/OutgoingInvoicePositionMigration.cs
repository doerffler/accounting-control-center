using DoePaAdminDataModel.DPApp;
using DoePaAdminDataModel.Kostenrechnung;
using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DataMigration
{
    public class OutgoingInvoicePositionMigration
    {

        public OutgoingInvoicePosition OutgoingInvoicePositionForImport { get; set; }

        public Auftragsposition RelatedAuftragsposition { get; set; }

        public Kostenstelle RelatedKostenstelle { get; set; }

        public Abrechnungseinheit RelatedAbrechnungseinheit { get; set; }

        public Waehrung RelatedWaehrung { get; set; }

        public Ausgangsrechnungsposition CreateAusgangsrechnungsposition(Ausgangsrechnung relatedAusgangsrechnung)
        {
            Ausgangsrechnungsposition newARP = new()
            {
                LeistungszeitraumBis = OutgoingInvoicePositionForImport.DateServiceUntil,
                LeistungszeitraumVon = OutgoingInvoicePositionForImport.DateServiceFrom,
                Positionsbeschreibung = OutgoingInvoicePositionForImport.PositionText,
                PositionsNummer = OutgoingInvoicePositionForImport.Sequence,
                Steuersatz = OutgoingInvoicePositionForImport.TaxPercent/100+1 ?? 0,
                StueckpreisNetto = OutgoingInvoicePositionForImport.HourlyRate ?? 0,
                Stueckzahl = OutgoingInvoicePositionForImport.Hours ?? 0,
                NettobetragWaehrung = RelatedWaehrung,
                
                ZugehoerigeAbrechnungseinheit = RelatedAbrechnungseinheit,
                ZugehoerigeAuftragsposition = RelatedAuftragsposition,
                ZugehoerigeKostenstelle = RelatedKostenstelle,
                ZugehoerigeRechnung = relatedAusgangsrechnung
            };

            return newARP;

        }

    }
}
