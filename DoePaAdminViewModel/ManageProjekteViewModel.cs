using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoePaAdmin.ViewModel
{
    public class ManageProjekteViewModel : DoePaAdminViewModelBase
    {
        // region Projekt
        private ObservableCollection<Projekt> _projekte = new();

        public ObservableCollection<Projekt> Projekte
        {
            get => _projekte;
            set => SetProperty(ref _projekte, value, true);
        }

        private Projekt _selectedProjekt;

        public Projekt SelectedProjekt
        {
            get => _selectedProjekt;
            set => SetProperty(ref _selectedProjekt, value);
        }

        public IRelayCommand AddProjektCommand { get; }

        public IRelayCommand RemoveProjektCommand { get; }
        // endregion

        /*
        public IRelayCommand AddMitarbeiterCommand { get; }

        public IRelayCommand RemoveMitarbeiterCommand { get; }
        */

        public ManageProjekteViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            AddProjektCommand = new AsyncRelayCommand(DoAddProjektAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveProjektCommand = new RelayCommand(DoRemoveProjekt);

            Projekte = Task.Run(async () => await DoePaAdminService.GetProjekteAsync()).Result;
            
        }


        private async Task DoAddProjektAsync(CancellationToken cancellationToken = default)
        {

            Projekt newProjekt = await DoePaAdminService.CreateProjektAsync(cancellationToken);
            newProjekt.Projektname = "NeuesProjekt";
            Projekte.Add(newProjekt);
        }
        private void DoRemoveProjekt()
        {

            if (SelectedProjekt != null)
            {
                _ = Projekte.Remove(SelectedProjekt);
            }
        }
    }
}
