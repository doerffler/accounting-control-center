using ACC.ViewModel.Messages;
using ACC.ViewModel.Services;
using ACCDataModel.Stammdaten;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel
{
    public class ManageAuftraegeViewModel : ACCViewModelBase
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

        public ManageAuftraegeViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {
            GetData();

            Messenger.Register<ManageAuftraegeViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void GetData()
        {
            Abrechnungseinheiten = new(Task.Run(async () => await ACCService.GetAbrechnungseinheitenAsync()).Result);
            Mitarbeiter = new(Task.Run(async () => await ACCService.GetMitarbeiterAsync()).Result);

            Messenger.Send(new StatusbarMessage("ManageAuftraegeViewModel loaded"), "Statusbar");
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }

    }
}
