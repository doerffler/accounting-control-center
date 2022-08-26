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

        private Projekt _selectedProjekt;

        public Projekt SelectedProjekt
        {
            get => _selectedProjekt;
            set => SetProperty(ref _selectedProjekt, value);
        }

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

        public ManageAuftraegeViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            
            Abrechnungseinheiten = new (Task.Run(async () => await DoePaAdminService.GetAbrechnungseinheitenAsync()).Result);
            Mitarbeiter = new (Task.Run(async () => await DoePaAdminService.GetMitarbeiterAsync()).Result);
            
        }

    }
}
