using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Kostenrechnung;
using DoePaAdminDataModel.Stammdaten;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ManageAusgangsrechnungenViewModel : DoePaAdminViewModelBase
    {

        private Geschaeftsjahr _selectedGeschaeftsjahr;

        public Geschaeftsjahr SelectedGeschaeftsjahr
        {
            get => _selectedGeschaeftsjahr;
            set => SetProperty(ref _selectedGeschaeftsjahr, value, true);
        }

        private ObservableCollection<Geschaeftsjahr> _geschaeftsjahre;

        public ObservableCollection<Geschaeftsjahr> Geschaeftsjahre
        {
            get => _geschaeftsjahre;
            set => SetProperty(ref _geschaeftsjahre, value, true);
        }

        private Ausgangsrechnung _selectedRechnung;

        public Ausgangsrechnung SelectedRechnung
        {
            get => _selectedRechnung;
            set => SetProperty(ref _selectedRechnung, value, true);
        }

        private ObservableCollection<Ausgangsrechnung> _rechnungen;

        public ObservableCollection<Ausgangsrechnung> Rechnungen
        {
            get => _rechnungen;
            set => SetProperty(ref _rechnungen, value, true);
        }

        private ObservableCollection<Abrechnungseinheit> _abrechnungseinheiten;

        public ObservableCollection<Abrechnungseinheit> Abrechnungseinheiten
        {
            get => _abrechnungseinheiten;
            set => SetProperty(ref _abrechnungseinheiten, value, true);
        }
                
        public static Uri InvoiceDocumentUrl
        {
            //TODO: This is supposed to return something like SelectedRechnung.ZugehoerigesDokument.DokumentUrl
            get => new ("file:///D:/Cloud/b.telligent%20group/Doerffler%20Buchhaltung%20-%20Dokumente/Rechnungen_Ausgang/2021-2022/20210154_FREELANCER_Nachtrag_764.PDF");
        }

        public IRelayCommand AddRechnungCommand { get; }

        public IRelayCommand RemoveRechnungCommand { get; }


        public ManageAusgangsrechnungenViewModel(IDoePaAdminService doePaAdminService, IUserInteractionService userInteractionService) : base(doePaAdminService, userInteractionService)
        {

            //TODO: Implement CanExecute-Functionality
            AddRechnungCommand = new AsyncRelayCommand(DoAddRechnungAsync);
            RemoveRechnungCommand = new RelayCommand(DoRemoveRechnung);

            Abrechnungseinheiten = new(Task.Run(async () => await DoePaAdminService.GetAbrechnungseinheitenAsync()).Result);
            Geschaeftsjahre = new(Task.Run(async () => await DoePaAdminService.GetGeschaeftsjahreAsync()).Result);
            Rechnungen = new(Task.Run(async () => await DoePaAdminService.GetAusgangsrechnungenAsync()).Result);

            SelectedGeschaeftsjahr = CalculateCurrentGeschaeftsjahr();

        }

        private void DoRemoveRechnung()
        {

            if (SelectedRechnung != null)
            {
                DoePaAdminService.RemoveAusgangsrechnung(SelectedRechnung);
                _ = Rechnungen.Remove(SelectedRechnung);
            }

        }

        private async Task DoAddRechnungAsync(CancellationToken cancellationToken = default)
        {

            string rechnungsNummer = GetNextRechnungsnummer();

            if (rechnungsNummer != null)
            {
                Ausgangsrechnung newRechnung = await DoePaAdminService.CreateAusgangsrechnungAsync(cancellationToken);
                newRechnung.RechnungsNummer = rechnungsNummer;
                Rechnungen.Add(newRechnung);
            }
        }

        private string GetNextRechnungsnummer()
        {
            string nextNumber = string.Empty;
            //TODO: Finish function.

            return SelectedGeschaeftsjahr?.Rechnungsprefix ?? string.Empty + nextNumber;
        }

        private Geschaeftsjahr CalculateCurrentGeschaeftsjahr()
        {
            Geschaeftsjahr geschaeftsjahrToReturn = Geschaeftsjahre.Where(gj => gj.DatumVon <= DateTime.Now && gj.DatumBis >= DateTime.Now).FirstOrDefault();

            return geschaeftsjahrToReturn;
        }
    }
}
