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
    public class ManageTaetigkeitViewModel : ACCViewModelBase
    {
        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }

        #region Taetigkeit
        private ObservableCollection<Taetigkeit> _taetigkeiten = new();
        public ObservableCollection<Taetigkeit> Taetigkeiten
        {
            get => _taetigkeiten;
            set => SetProperty(ref _taetigkeiten, value, true);
        }

        private Taetigkeit _selectedTaetigkeit = new();
        public Taetigkeit SelectedTaetigkeit
        {
            get => _selectedTaetigkeit;
            set => SetProperty(ref _selectedTaetigkeit, value, true);
        }
        #endregion

        public ManageTaetigkeitViewModel(IACCService accService) : base(accService)
        {
            AddCommand = new AsyncRelayCommand(DoAddAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveCommand = new RelayCommand(DoRemove);

            GetData();

            Messenger.Register<ManageTaetigkeitViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void GetData()
        {
            Taetigkeiten = new(Task.Run(async () => await ACCService.GetTaetigkeitenAsync()).Result);

            Messenger.Send(new StatusbarMessage("ManageTaetigkeitViewModel loaded"), "Statusbar");
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }

        private void DoRemove()
        {
            if (SelectedTaetigkeit != null)
            {
                ACCService.RemoveTaetigkeit(SelectedTaetigkeit);
                _ = Taetigkeiten.Remove(SelectedTaetigkeit);
            }
        }

        private async Task DoAddAsync(CancellationToken cancellationToken = default)
        {
            Taetigkeit taetigkeit = await ACCService.CreateTaetigkeitAsync(cancellationToken);
            taetigkeit.Taetigkeitsbeschreibung = "Neue Tätigkeit";

            Taetigkeiten.Add(taetigkeit);
        }
    }
}
