using ACC.ViewModel.Messages;
using ACC.ViewModel.Services;
using ACCDataModel.DPApp;
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
    public class ManagePostleitzahlenViewModel : ACCViewModelBase
    {
        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }

        #region Postleitzahl
        private Postleitzahl _selectedPostleitzahl;

        public Postleitzahl SelectedPostleitzahl
        {
            get => _selectedPostleitzahl;
            set => SetProperty(ref _selectedPostleitzahl, value, true);
        }

        private ObservableCollection<Postleitzahl> _postleitzahlen;

        public ObservableCollection<Postleitzahl> Postleitzahlen
        {
            get => _postleitzahlen;
            set => SetProperty(ref _postleitzahlen, value, true);
        }

        #endregion

        public ManagePostleitzahlenViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {
            AddCommand = new AsyncRelayCommand(DoAddAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveCommand = new RelayCommand(DoRemove);

            GetData();

            Messenger.Register<ManagePostleitzahlenViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void GetData()
        {
            Postleitzahlen = new(Task.Run(async () => await ACCService.GetPostleitzahlenAsync()).Result);

            Messenger.Send(new StatusbarMessage("ManagePostleitzahlenViewModel loaded"), "Statusbar");
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }

        private async Task DoAddAsync(CancellationToken cancellationToken = default)
        {

            Postleitzahl newPostleitzahl = await ACCService.CreatePostleitzahlAsync(cancellationToken);
            Postleitzahlen.Add(newPostleitzahl);

        }

        private void DoRemove()
        {
            if (SelectedPostleitzahl != null)
            {
                ACCService.RemovePostleitzahl(SelectedPostleitzahl);
                _ = Postleitzahlen.Remove(SelectedPostleitzahl);
            }
        }

    }
}
