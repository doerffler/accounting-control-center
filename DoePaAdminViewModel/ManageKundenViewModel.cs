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
    public class ManageKundenViewModel : DoePaAdminViewModelBase
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

        public ManageKundenViewModel(IDoePaAdminService doePaAdminService, IUserInteractionService userInteractionService) : base(doePaAdminService)
        {
            Kunden = new(Task.Run(async () => await DoePaAdminService.GetKundenAsync()).Result);
            
            AddCommand = new AsyncRelayCommand(DoAddAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveCommand = new RelayCommand(DoRemove);
        }

        private async Task DoAddAsync(CancellationToken cancellationToken = default)
        {
            Kunde kunde = await DoePaAdminService.CreateKundeAsync(cancellationToken);
            kunde.Langname = "Neuer Kunde";

            Kunden.Add(kunde);
        }


        private void DoRemove()
        {
            if (SelectedKunde != null)
            {
                DoePaAdminService.RemoveKunde(SelectedKunde);
                _ = Kunden.Remove(SelectedKunde);
            }
        }

    }
}
