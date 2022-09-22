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
    public class ManageWaehrungViewModel : DoePaAdminViewModelBase
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

        public ManageWaehrungViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            Waehrungen = new(Task.Run(async () => await DoePaAdminService.GetWaehrungenAsync()).Result);

            AddCommand = new AsyncRelayCommand(DoAddAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveCommand = new RelayCommand(DoRemove);
        }

        private void DoRemove()
        {
            if (SelectedWaehrung != null)
            {
                DoePaAdminService.RemoveWaehrung(SelectedWaehrung);
                _ = Waehrungen.Remove(SelectedWaehrung);
            }
        }

        private async Task DoAddAsync(CancellationToken cancellationToken = default)
        {
            Waehrung waehrung = await DoePaAdminService.CreateWaehrungAsync(cancellationToken);
            waehrung.WaehrungISO = "ISO";
            waehrung.WaehrungName = "Neue Währung";
            waehrung.WaehrungZeichen = "Zeichen";

            Waehrungen.Add(waehrung);
        }
    }
}
