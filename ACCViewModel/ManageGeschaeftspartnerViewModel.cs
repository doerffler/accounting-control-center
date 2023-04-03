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
    public abstract class ManageGeschaeftspartnerViewModel<T> : ACCViewModelBase where T : Geschaeftspartner, new()
    {

        private ObservableCollection<T> _geschaeftspartner;

        public ObservableCollection<T> Geschaeftspartner
        {
            get => _geschaeftspartner;
            set => SetProperty(ref _geschaeftspartner, value, true);
        }

        private T _selectedGeschaeftspartner;

        public T SelectedGeschaeftspartner
        {
            get => _selectedGeschaeftspartner;
            set => SetProperty(ref _selectedGeschaeftspartner, value, true);
        }

        public IRelayCommand AddGeschaeftspartnerCommand { get; set; }

        public IRelayCommand RemoveGeschaeftspartnerCommand { get; set; }

        public ManageGeschaeftspartnerViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {

            AddGeschaeftspartnerCommand = new AsyncRelayCommand(DoAddGeschaeftspartnerAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveGeschaeftspartnerCommand = new RelayCommand(DoRemoveGeschaeftspartner);

            GetData();

            Messenger.Register<ManageGeschaeftspartnerViewModel<T>, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void GetData()
        {
            Geschaeftspartner = new(Task.Run(async () => await ACCService.GetGeschaeftspartnerAsync<T>()).Result);

            Messenger.Send(new StatusbarMessage("ManageGeschaeftspartnerViewModel loaded"), "Statusbar");
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }


        public async Task DoAddGeschaeftspartnerAsync(CancellationToken cancellationToken = default)
        {
            T newGeschaeftspartner = await ACCService.CreateGeschaeftspartnerAsync<T>(cancellationToken);
            Geschaeftspartner.Add(newGeschaeftspartner);
        }

        public void DoRemoveGeschaeftspartner()
        {
            ACCService.RemoveGeschaeftspartner(SelectedGeschaeftspartner);
            Geschaeftspartner.Remove(SelectedGeschaeftspartner);
            SelectedGeschaeftspartner = null;
        }

    }
}
