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
    public class ManageAbrechnungseinheitViewModel : DoePaAdminViewModelBase
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

        public ManageAbrechnungseinheitViewModel(IDoePaAdminService doePaAdminService, IUserInteractionService userInteractionService) : base(doePaAdminService, userInteractionService)
        {
            Abrechnungseinheiten = new(Task.Run(async () => await DoePaAdminService.GetAbrechnungseinheitenAsync()).Result);

            AddCommand = new AsyncRelayCommand(DoAddAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveCommand = new RelayCommand(DoRemove);
        }

        private void DoRemove()
        {
            if (SelectedAbrechnungseinheit != null)
            {
                DoePaAdminService.RemoveAbrechnungseinheit(SelectedAbrechnungseinheit);
                _ = Abrechnungseinheiten.Remove(SelectedAbrechnungseinheit);
            }
        }

        private async Task DoAddAsync(CancellationToken cancellationToken = default)
        {
            Abrechnungseinheit abrechnungseinheit = await DoePaAdminService.CreateAbrechnungseinheitAsync(cancellationToken);
            abrechnungseinheit.Name = "Neue Abrechnungseinheit";

            Abrechnungseinheiten.Add(abrechnungseinheit);
        }
    }
}
