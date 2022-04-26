using DoePaAdmin.ViewModel.Services;
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
    public class ManageAuftraegeViewModel : DoePaAdminViewModelBase
    {
        // region Kunde
        private ObservableCollection<Kunde> _kunden = new();

        public ObservableCollection<Kunde> Kunden
        {
            get => _kunden;
            set => SetProperty(ref _kunden, value, true);
        }

        private Kunde _selectedKunde;

        public Kunde SelectedKunde
        {
            get => _selectedKunde;
            set => SetProperty(ref _selectedKunde, value);
        }

        public IRelayCommand AddKundeCommand { get; }

        public IRelayCommand RemoveKundeCommand { get; }
        // endregion


        // region Auftrag (oben rechts)
        private Auftrag _selectedAuftrag;
        public Auftrag SelectedAuftrag
        {
            get => _selectedAuftrag;
            set => SetProperty(ref _selectedAuftrag, value);
        }
        // endregion


        // region Auftragsposition (unten rechts)
        private Auftrag _selectedAuftragspositionen;
        public Auftrag SelectedAuftragspositionen
        {
            get => _selectedAuftragspositionen;
            set => SetProperty(ref _selectedAuftragspositionen, value);
        }
        // endregion


        public ManageAuftraegeViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            // Zeiger auf Methode: "DoAddKundeAsync" --> Delegate
            AddKundeCommand = new AsyncRelayCommand(DoAddKundeAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveKundeCommand = new RelayCommand(DoRemoveKunde);

            Kunden = Task.Run(async () => await DoePaAdminService.GetKundeAsync()).Result;

            //Taetigkeiten = Task.Run(async () => await DoePaAdminService.GetTaetigkeitenAsync()).Result;
        }

        private void DoRemoveKunde()
        {

            if (SelectedKunde != null)
            {
                _ = Kunden.Remove(SelectedKunde);
            }
        }

        private async Task DoAddKundeAsync(CancellationToken cancellationToken = default)
        {
            Kunde newKunde = await DoePaAdminService.CreateKundeAsync(cancellationToken);
            newKunde.Kundenname = "Pikachu";
            Kunden.Add(newKunde);

            Auftrag newAuftrag = await DoePaAdminService.CreateAuftragAsync(cancellationToken);
            newAuftrag.Kunde = newKunde;
            newAuftrag.Auftragsname = "Test";

            //newKunde.Auftraege.Add(newAuftrag);

            await DoePaAdminService.SaveChangesAsync(cancellationToken);

            /*
            Mitarbeiter newMitarbeiter = await DoePaAdminService.CreateMitarbeiterAsync(cancellationToken);
            newMitarbeiter.Vorname = "Neuer";
            newMitarbeiter.Nachname = "Mitarbeiter";
            Mitarbeiter.Add(newMitarbeiter);
            */

        }
    }
}
