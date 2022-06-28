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

        private Auftrag _selectedAuftrag;
        public Auftrag SelectedAuftrag
        {
            get => _selectedAuftrag;
            set => SetProperty(ref _selectedAuftrag, value);
        }
        
        private ObservableCollection<Abrechnungseinheit> _abrechnungseinheiten = new();

        public ObservableCollection<Abrechnungseinheit> Abrechnungseinheiten
        {
            get => _abrechnungseinheiten;
            set => SetProperty(ref _abrechnungseinheiten, value, true);
        }

        private ObservableCollection<Mitarbeiter> _mitarbeiter = new();

        public ObservableCollection<Mitarbeiter> Mitarbeiter
        {
            get => _mitarbeiter;
            set => SetProperty(ref _mitarbeiter, value, true);
        }

        private ObservableCollection<Projekt> _projekte = new();

        public ObservableCollection<Projekt> Projekte
        {
            get => _projekte;
            set => SetProperty(ref _projekte, value, true);
        }

        public ManageAuftraegeViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            
            AddKundeCommand = new AsyncRelayCommand(DoAddKundeAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveKundeCommand = new RelayCommand(DoRemoveKunde);

            Kunden = new (Task.Run(async () => await DoePaAdminService.GetKundeAsync()).Result);
            Abrechnungseinheiten = new (Task.Run(async () => await DoePaAdminService.GetAbrechnungseinheitenAsync()).Result);
            Mitarbeiter = new (Task.Run(async () => await DoePaAdminService.GetMitarbeiterAsync()).Result);
            Projekte = new (Task.Run(async () => await DoePaAdminService.GetProjekteAsync()).Result);

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
            newKunde.Kundenname = "Neuer Kunde";
            Kunden.Add(newKunde);

            Auftrag newAuftrag = await DoePaAdminService.CreateAuftragAsync(cancellationToken);
            newAuftrag.Kunde = newKunde;
            newAuftrag.Auftragsname = "Neuer Auftrag";
            
        }
    }
}
