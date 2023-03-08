using CommunityToolkit.Mvvm.Input;
using ACC.ViewModel.Model;
using ACC.ViewModel.Services;
using ACCDataModel.DataMigration;
using ACCDataModel.DPApp;
using ACCDataModel.Kostenrechnung;
using ACCDataModel.Stammdaten;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ACC.ViewModel.Messages;

namespace ACC.ViewModel
{
    public class ImportOutgoingInvoicesViewModel : ACCViewModelBase
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

        public IRelayCommand MigrateInvoicesCommand { get; }

        public ImportOutgoingInvoicesViewModel(IACCService accService, IDPAppService dpAppService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {
            DPAppService = dpAppService;

            MigrateInvoicesCommand = new AsyncRelayCommand(DoMigrateInvoicesCommandAsync);

            GetData();

            Messenger.Register<ImportOutgoingInvoicesViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void GetData()
        {
            IEnumerable<OutgoingInvoiceMigration> outgoingInvoiceMigrations = Task.Run(async () => await DPAppService.GetOutgoingInvoicesAsync()).Result;

            Task.Run(async () => await MapDPAppMasterdataAsync(outgoingInvoiceMigrations)).Wait();

            Auftragspositionen = Task.Run(async () => await ACCService.GetAuftragspositionenAsync()).Result;
            OutgoingInvoices = new(outgoingInvoiceMigrations);
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }

        private async Task DoMigrateInvoicesCommandAsync(CancellationToken cancellationToken = default)
        {
            Ausgangsrechnung newRechnung;

            foreach (OutgoingInvoiceMigration currentInvoice in OutgoingInvoices)
            {
                if (currentInvoice.IsReadyForMigration)
                {
                    //TODO: Attach this to DoePaAdminService:
                    newRechnung = currentInvoice.CreateAusgangsrechnung();

                    await ACCService.AddAusgangsrechnungAsync(newRechnung, cancellationToken);
                }
            }
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
            IEnumerable<Kostenstelle> listCostCenters = await ACCService.GetKostenstellenAsync(cancellationToken);
            IEnumerable<Waehrung> listWaehrungen = await ACCService.GetWaehrungenAsync(cancellationToken);
            IEnumerable<Abrechnungseinheit> listAbrechnungseinheiten = await ACCService.GetAbrechnungseinheitenAsync(cancellationToken);
            IEnumerable<Auftrag> listAuftraege = await ACCService.GetAuftraegeAsync(cancellationToken);
            IEnumerable<Geschaeftsjahr> listGeschaeftsjahre = await ACCService.GetGeschaeftsjahreAsync(cancellationToken);
            IEnumerable<Debitor> listDebitoren = await ACCService.GetGeschaeftspartnerAsync<Debitor>(cancellationToken);
                        
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
