using ACC.ViewModel.Services;
using ACCDataModel.Stammdaten;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ACC.ViewModel
{
    public class ManageMitarbeiterViewModel : ACCViewModelBase
    {

        private ObservableCollection<Mitarbeiter> _mitarbeiter = new();

        public ObservableCollection<Mitarbeiter> Mitarbeiter
        {
            get => _mitarbeiter;
            set => SetProperty(ref _mitarbeiter, value, true);
        }

        private ObservableCollection<Taetigkeit> _taetigkeiten = new();

        public ObservableCollection<Taetigkeit> Taetigkeiten
        {
            get => _taetigkeiten;
            set => SetProperty(ref _taetigkeiten, value, true);
        }

        private Mitarbeiter _selectedMitarbeiter;

        public Mitarbeiter SelectedMitarbeiter
        {
            get => _selectedMitarbeiter;
            set => SetProperty(ref _selectedMitarbeiter, value, true);
        }

        

        public IRelayCommand AddMitarbeiterCommand { get; }

        public IRelayCommand RemoveMitarbeiterCommand { get; }

        public ManageMitarbeiterViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {
            AddMitarbeiterCommand = new AsyncRelayCommand(DoAddMitarbeiterAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveMitarbeiterCommand = new RelayCommand(DoRemoveMitarbeiter);

            Mitarbeiter = new(Task.Run(async () => await ACCService.GetMitarbeiterAsync()).Result);
            Taetigkeiten = new(Task.Run(async () => await ACCService.GetTaetigkeitenAsync()).Result);
            
        }

        private void DoRemoveMitarbeiter()
        {

            if (SelectedMitarbeiter != null)
            {
                _ = Mitarbeiter.Remove(SelectedMitarbeiter);
            }

        }

        private async Task DoAddMitarbeiterAsync(CancellationToken cancellationToken = default)
        {

            Mitarbeiter newMitarbeiter = await ACCService.CreateMitarbeiterAsync(cancellationToken);
            newMitarbeiter.Vorname = "Neuer";
            newMitarbeiter.Nachname = "Mitarbeiter";
            Mitarbeiter.Add(newMitarbeiter);

        }
    }
}
