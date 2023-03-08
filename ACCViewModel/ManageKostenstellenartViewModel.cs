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
    public class ManageKostenstellenartViewModel : ACCViewModelBase
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

        public ManageKostenstellenartViewModel(IACCService accService) : base(accService)
        {

            AddCommand = new AsyncRelayCommand(DoAddAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveCommand = new RelayCommand(DoRemove);

            GetData();

            Messenger.Register<ManageKostenstellenartViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void GetData()
        {
            Kostenstellenarten = new(Task.Run(async () => await ACCService.GetKostenstellenartenAsync()).Result);
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }

        private void DoRemove()
        {
            if (SelectedKostenstellenart != null)
            {
                ACCService.RemoveKostenstellenart(SelectedKostenstellenart);
                _ = Kostenstellenarten.Remove(SelectedKostenstellenart);
            }
        }

        private async Task DoAddAsync(CancellationToken cancellationToken = default)
        {
            Kostenstellenart kostenstellenart = await ACCService.CreateKostenstellenartAsync(cancellationToken);
            kostenstellenart.Kostenstellenartbezeichnung = "Neue Kostenstellenart";

            Kostenstellenarten.Add(kostenstellenart);
        }
    }
}
