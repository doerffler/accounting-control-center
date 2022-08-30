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
    public class ManageKostenstellenartViewModel : DoePaAdminViewModelBase
    {
        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }

        #region Kostenstellenart
        private ObservableCollection<Kostenstellenart> _kostenstellenarten = new();
        public ObservableCollection<Kostenstellenart> Kostenstellenarten
        {
            get => _kostenstellenarten;
            set => SetProperty(ref _kostenstellenarten, value, true);
        }

        private Kostenstellenart _selectedKostenstellenart = new();
        public Kostenstellenart SelectedKostenstellenart
        {
            get => _selectedKostenstellenart;
            set => SetProperty(ref _selectedKostenstellenart, value, true);
        }
        #endregion

        public ManageKostenstellenartViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            Kostenstellenarten = new(Task.Run(async () => await DoePaAdminService.GetKostenstellenartenAsync()).Result);

            AddCommand = new AsyncRelayCommand(DoAddAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveCommand = new RelayCommand(DoRemove);
        }

        private void DoRemove()
        {
            if (SelectedKostenstellenart != null)
            {
                DoePaAdminService.RemoveKostenstellenart(SelectedKostenstellenart);
                _ = Kostenstellenarten.Remove(SelectedKostenstellenart);
            }
        }

        private async Task DoAddAsync(CancellationToken cancellationToken = default)
        {
            Kostenstellenart kostenstellenart = await DoePaAdminService.CreateKostenstellenartAsync(cancellationToken);
            kostenstellenart.Kostenstellenartbezeichnung = "Neue Kostenstellenart";

            Kostenstellenarten.Add(kostenstellenart);
        }
    }
}
