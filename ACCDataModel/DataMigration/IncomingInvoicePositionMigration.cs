using ACCDataModel.DPApp;
using ACCDataModel.Kostenrechnung;
using ACCDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.DataMigration
{
    public class IncomingInvoicePositionMigration
    {

        public IncomingInvoicePosition IncomingInvoicePositionForImport { get; set; }

        public Auftragsposition RelatedAuftragsposition { get; set; }

        public Kostenstelle RelatedKostenstelle { get; set; }

        public Abrechnungseinheit RelatedAbrechnungseinheit { get; set; }

        public Ausgangsrechnungsposition CreateAusgangsrechnungsposition(Ausgangsrechnung relatedAusgangsrechnung)
        {
            Ausgangsrechnungsposition newARP = new()
            {
                LeistungszeitraumBis = IncomingInvoicePositionForImport.DateServiceUntil,
                LeistungszeitraumVon = IncomingInvoicePositionForImport.DateServiceFrom,
                Positionsbeschreibung = IncomingInvoicePositionForImport.PositionText,
                PositionsNummer = IncomingInvoicePositionForImport.Sequence,
                Steuersatz = IncomingInvoicePositionForImport.TaxPercent/100+1 ?? 0,
                StueckpreisNetto = IncomingInvoicePositionForImport.HourlyRate ?? 0,
                Stueckzahl = IncomingInvoicePositionForImport.Hours ?? 0,
                                
                ZugehoerigeAbrechnungseinheit = RelatedAbrechnungseinheit,
                ZugehoerigeAuftragsposition = RelatedAuftragsposition,
                ZugehoerigeKostenstelle = RelatedKostenstelle,
                ZugehoerigeRechnung = relatedAusgangsrechnung
            };

            return newARP;

        }

    }
}
