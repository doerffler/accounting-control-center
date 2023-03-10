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
    public class ManageAbrechnungseinheitViewModel : ACCViewModelBase
    {
        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }

        #region Abrechnungseinheit
        private ObservableCollection<Abrechnungseinheit> _abrechnungseinheiten = new();
        public ObservableCollection<Abrechnungseinheit> Abrechnungseinheiten
        {
            get => _abrechnungseinheiten;
            set => SetProperty(ref _abrechnungseinheiten, value, true);
        }

        private Abrechnungseinheit _selectedAbrechnungseinheit = new();
        public Abrechnungseinheit SelectedAbrechnungseinheit
        {
            get => _selectedAbrechnungseinheit;
            set => SetProperty(ref _selectedAbrechnungseinheit, value, true);
        }
        #endregion

        public ManageAbrechnungseinheitViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {
            GetData();

            AddCommand = new AsyncRelayCommand(DoAddAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveCommand = new RelayCommand(DoRemove);

            Messenger.Register<ManageAbrechnungseinheitViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }

        private void GetData()
        {
            Abrechnungseinheiten = new(Task.Run(async () => await ACCService.GetAbrechnungseinheitenAsync()).Result);
        }

        private void DoRemove()
        {
            if (SelectedAbrechnungseinheit != null)
            {
                ACCService.RemoveAbrechnungseinheit(SelectedAbrechnungseinheit);
                _ = Abrechnungseinheiten.Remove(SelectedAbrechnungseinheit);
            }
        }

        private async Task DoAddAsync(CancellationToken cancellationToken = default)
        {
            Abrechnungseinheit abrechnungseinheit = await ACCService.CreateAbrechnungseinheitAsync(cancellationToken);
            abrechnungseinheit.Name = "Neue Abrechnungseinheit";

            Abrechnungseinheiten.Add(abrechnungseinheit);
        }
    }
}
