using DoePaAdmin.ViewModel.Model;
using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.DataMigration;
using DoePaAdminDataModel.DPApp;
using DoePaAdminDataModel.Stammdaten;
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

        private async Task MapDPAppMasterdataAsync(IEnumerable<OutgoingInvoiceMigration> outgoingInvoiceMigrations, CancellationToken cancellationToken = default)
        {
            IEnumerable<Kostenstelle> listCostCenters = await DoePaAdminService.GetKostenstellenAsync(cancellationToken);
            IEnumerable<Waehrung> listWaehrungen = await DoePaAdminService.GetWaehrungenAsync(cancellationToken);
            IEnumerable<Abrechnungseinheit> listAbrechnungseinheiten = await DoePaAdminService.GetAbrechnungseinheitenAsync(cancellationToken);
            IEnumerable<Auftragsposition> listAuftragspositionen = Auftragspositionen;

            string waehrungISO;
            string abrechnungseinheitName;

            foreach (OutgoingInvoiceMigration currentInvoice in outgoingInvoiceMigrations)
            {

                //Map currency (easy one):

                //TODO: There needs to be a place to store this mapping information:
                //Issue #80 was created for this one.
                waehrungISO = currentInvoice.OutgoingInvoiceForImport.Currency.Trim() switch
                {
                    "CHF" => "CHF",
                    "€" => "EUR",
                    _ => "EUR",
                };
                                
                currentInvoice.RelatedWaehrung = listWaehrungen.First(w => w.WaehrungISO.Equals(waehrungISO));

                foreach (OutgoingInvoicePositionMigration currentPosition in currentInvoice.OutgoingInvoicePositions)
                {

                    //Map cost centers first:
                    int? costCenterNo = currentPosition.OutgoingInvoicePositionForImport.RelatedCostCenter?.Number;

                    if (costCenterNo.HasValue)
                    {
                        currentPosition.RelatedKostenstelle = listCostCenters.FirstOrDefault(cc => cc.KostenstellenNummer.Equals(costCenterNo.Value));
                    }

                    //Map Abrechnungseinheit:

                    //TODO: Issue #80
                    abrechnungseinheitName = currentPosition.OutgoingInvoicePositionForImport.TypeOfSettlement switch
                    {
                        "Tage" => "Personentage",
                        "Stunden" => "Stunden",
                        "Preis" => "Stück",
                        _ => "Stunden"
                    };

                    currentPosition.RelatedAbrechnungseinheit = listAbrechnungseinheiten.First(ae => ae.Name.Equals(abrechnungseinheitName));

                    //Order position next (maybe use the RAID?)


                };



                

            }

        }

    }
}
