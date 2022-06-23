using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Kostenrechnung;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.Input;
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

        private ObservableCollection<Geschaeftsjahr> _geschaeftsjahre = new();

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

        private ObservableCollection<Ausgangsrechnung> _rechnungen = new();

        public ObservableCollection<Ausgangsrechnung> Rechnungen
        {
            get => _rechnungen;
            set => SetProperty(ref _rechnungen, value, true);
        }

        public IRelayCommand AddRechnungCommand { get; }

        public IRelayCommand RemoveRechnungCommand { get; }


        public ManageAusgangsrechnungenViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {

            //TODO: Implement CanExecute-Functionality
            AddRechnungCommand = new AsyncRelayCommand(DoAddRechnungAsync);
            RemoveRechnungCommand = new RelayCommand(DoRemoveRechnung);

            Geschaeftsjahre = Task.Run(async () => await DoePaAdminService.GetGeschaeftsjahreAsync()).Result;
            Rechnungen = Task.Run(async () => await DoePaAdminService.GetAusgangsrechnungenAsync()).Result;

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
