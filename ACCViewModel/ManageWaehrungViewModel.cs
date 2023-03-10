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
    public class ManageWaehrungViewModel : ACCViewModelBase
    {
        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }

        #region Waehrung
        private ObservableCollection<Waehrung> _waehrungen = new();
        public ObservableCollection<Waehrung> Waehrungen
        {
            get => _waehrungen;
            set => SetProperty(ref _waehrungen, value, true);
        }

        private Waehrung _selectedWaehrung = new();
        public Waehrung SelectedWaehrung
        {
            get => _selectedWaehrung;
            set => SetProperty(ref _selectedWaehrung, value, true);
        }
        #endregion

        public ManageWaehrungViewModel(IACCService accService) : base(accService)
        {
            AddCommand = new AsyncRelayCommand(DoAddAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveCommand = new RelayCommand(DoRemove);

            GetData();

            Messenger.Register<ManageWaehrungViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void GetData()
        {
            Waehrungen = new(Task.Run(async () => await ACCService.GetWaehrungenAsync()).Result);
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }

        private void DoRemove()
        {
            if (SelectedWaehrung != null)
            {
                ACCService.RemoveWaehrung(SelectedWaehrung);
                _ = Waehrungen.Remove(SelectedWaehrung);
            }
        }

        private async Task DoAddAsync(CancellationToken cancellationToken = default)
        {
            Waehrung waehrung = await ACCService.CreateWaehrungAsync(cancellationToken);
            waehrung.WaehrungISO = "ISO";
            waehrung.WaehrungName = "Neue Währung";
            waehrung.WaehrungZeichen = "Zeichen";

            Waehrungen.Add(waehrung);
        }
    }
}
