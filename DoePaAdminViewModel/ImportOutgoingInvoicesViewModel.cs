using DoePaAdmin.ViewModel.Model;
using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.DataMigration;
using DoePaAdminDataModel.DPApp;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ImportOutgoingInvoicesViewModel : DoePaAdminViewModelBase
    {

        private IDPAppService _dpAppservice;

        private IDPAppService DPAppService
        {
            get { return _dpAppservice; }
            set { _dpAppservice = value; }
        }

        private IEnumerable<Auftragsposition> _auftragspositionen;

        public IEnumerable<Auftragsposition> Auftragspositionen
        {
            get => _auftragspositionen;
            set => SetProperty(ref _auftragspositionen, value, true);
        }

        private OutgoingInvoiceEnumerable _outgoingInvoices = new();

        public OutgoingInvoiceEnumerable OutgoingInvoices
        {
            get => _outgoingInvoices;
            set => SetProperty(ref _outgoingInvoices, value, true);
        }

        public ImportOutgoingInvoicesViewModel(IDoePaAdminService doePaAdminService, IDPAppService dpAppService, IUserInteractionService userInteractionService) : base(doePaAdminService, userInteractionService)
        {
            DPAppService = dpAppService;

            IEnumerable<OutgoingInvoiceMigration> outgoingInvoiceMigrations = Task.Run(async () => await DPAppService.GetOutgoingInvoicesAsync()).Result;

            Task.Run(async () => await MapDPAppMasterdataAsync(outgoingInvoiceMigrations)).Wait();

            Auftragspositionen = Task.Run(async () => await DoePaAdminService.GetAuftragspositionenAsync()).Result;
            OutgoingInvoices = new(outgoingInvoiceMigrations);

        }

        private static Waehrung MapWaehrung(IEnumerable<Waehrung> listWaehrungen, string waehrungDPApp)
        {
            Waehrung waehrungEURO = null;

            foreach (Waehrung currentWaehrung in listWaehrungen)
            {
                
                if (currentWaehrung.WaehrungISO.Equals("EUR"))
                {
                    waehrungEURO = currentWaehrung;
                }

                if (currentWaehrung.WaehrungAdditions != null && currentWaehrung.WaehrungAdditions.TryGetValue("DPAppKey", out string waehrungDPAppValue))
                {
                    if (waehrungDPAppValue.Equals(waehrungDPApp))
                    {
                        return currentWaehrung;
                    }
                }
            }

            return waehrungEURO;
        }

        private static Abrechnungseinheit MapAbrechnungseinheit(IEnumerable<Abrechnungseinheit> listAbrechnungseinheiten, string abrechnungseinheitDPApp)
        {
            Abrechnungseinheit aeStunden = null;

            foreach (Abrechnungseinheit currentAbrechnungseinheit in listAbrechnungseinheiten)
            {

                if (currentAbrechnungseinheit.Name.Equals("Stunden"))
                {
                    aeStunden = currentAbrechnungseinheit;
                }

                if (currentAbrechnungseinheit.Additions != null && currentAbrechnungseinheit.Additions.TryGetValue("DPAppKey", out string aeDPAppValue))
                {
                    if (aeDPAppValue.Equals(abrechnungseinheitDPApp))
                    {
                        return currentAbrechnungseinheit;
                    }
                }
            }

            return aeStunden;
        }

        private static Debitor MapRechnungsempfaenger(IEnumerable<Debitor> listDebitoren, Department relatedDepartment)
        {
            foreach (Debitor currentDebitor in listDebitoren)
            {
                if (currentDebitor.Additions != null && currentDebitor.Additions.TryGetValue("DPAppKey", out string debDPAppValue))
                {
                    if (debDPAppValue.Equals(relatedDepartment))
                    {
                        return currentDebitor;
                    }
                }
            }

            return null;
        }

        private async Task MapDPAppMasterdataAsync(IEnumerable<OutgoingInvoiceMigration> outgoingInvoiceMigrations, CancellationToken cancellationToken = default)
        {
            IEnumerable<Kostenstelle> listCostCenters = await DoePaAdminService.GetKostenstellenAsync(cancellationToken);
            IEnumerable<Waehrung> listWaehrungen = await DoePaAdminService.GetWaehrungenAsync(cancellationToken);
            IEnumerable<Abrechnungseinheit> listAbrechnungseinheiten = await DoePaAdminService.GetAbrechnungseinheitenAsync(cancellationToken);
            IEnumerable<Auftrag> listAuftraege = await DoePaAdminService.GetAuftraegeAsync(cancellationToken);
            IEnumerable<Geschaeftsjahr> listGeschaeftsjahre = await DoePaAdminService.GetGeschaeftsjahreAsync(cancellationToken);
            IEnumerable<Debitor> listDebitoren = await DoePaAdminService.GetGeschaeftspartnerAsync<Debitor>(cancellationToken);
                        
            foreach (OutgoingInvoiceMigration currentInvoice in outgoingInvoiceMigrations)
            {

                //Map business year:
                currentInvoice.RelatedGeschaeftsjahr = listGeschaeftsjahre.FirstOrDefault(gj =>
                    gj.DatumVon.Equals(currentInvoice.OutgoingInvoiceForImport.RelatedBusinessYear.DateFrom) &&
                    gj.DatumBis.Equals(currentInvoice.OutgoingInvoiceForImport.RelatedBusinessYear.DateUntil)
                    );

                //Map invoice recipient:
                currentInvoice.RelatedRechnungsempfaenger = MapRechnungsempfaenger(listDebitoren, currentInvoice.OutgoingInvoiceForImport.RelatedDepartment);

                //Map currency:
                currentInvoice.RelatedWaehrung = MapWaehrung(listWaehrungen, currentInvoice.OutgoingInvoiceForImport.Currency.Trim());

                foreach (OutgoingInvoicePositionMigration currentPosition in currentInvoice.OutgoingInvoicePositions)
                {

                    //Map cost centers first:
                    int? costCenterNo = currentPosition.OutgoingInvoicePositionForImport.RelatedCostCenter?.Number;

                    if (costCenterNo.HasValue)
                    {
                        currentPosition.RelatedKostenstelle = listCostCenters.FirstOrDefault(cc => cc.KostenstellenNummer.Equals(costCenterNo.Value));
                    }

                    //Map Abrechnungseinheit:
                    currentPosition.RelatedAbrechnungseinheit = MapAbrechnungseinheit(listAbrechnungseinheiten, currentPosition.OutgoingInvoicePositionForImport.TypeOfSettlement);

                    //Order position next (maybe use the RAID?)
                    //We could also think about creating a list of orders, that could match the invoice item
                    Auftrag auftrag = listAuftraege.FirstOrDefault(a => a.Vertragsnummer.Equals(currentPosition.OutgoingInvoicePositionForImport.Raid));

                };
                
            }

        }

    }
}
