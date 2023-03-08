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
    public class ManageKundenViewModel : ACCViewModelBase
    {
        #region Kunde

        private ObservableCollection<Kunde> _kunden = new();

        public ObservableCollection<Kunde> Kunden
        {
            get => _kunden;
            set => SetProperty(ref _kunden, value, true);
        }

        private Kunde _selectedKunde = new();

        public Kunde SelectedKunde
        {
            get => _selectedKunde;
            set => SetProperty(ref _selectedKunde, value, true);
        }

        #endregion

        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }

        public ManageKundenViewModel(IACCService accServiceService, IUserInteractionService userInteractionService) : base(accServiceService)
        {
            
            AddCommand = new AsyncRelayCommand(DoAddAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveCommand = new RelayCommand(DoRemove);

            GetData();

            Messenger.Register<ManageKundenViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void GetData()
        {
            Kunden = new(Task.Run(async () => await ACCService.GetKundenAsync()).Result);
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }

        private async Task DoAddAsync(CancellationToken cancellationToken = default)
        {
            //TODO: Introduce string localization for this message:
            string kundenName = UserInteractionService.AskForUserInput("Bitte geben Sie den Namen des neuen Kunden ein:", "Kundennamen eingeben");

            Kunde kunde = await ACCService.CreateKundeAsync(cancellationToken);
            kunde.Langname = kundenName;

            Kunden.Add(kunde);
        }


        private void DoRemove()
        {
            if (SelectedKunde != null)
            {
                ACCService.RemoveKunde(SelectedKunde);
                _ = Kunden.Remove(SelectedKunde);
            }
        }

    }
}
