using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ManageTaetigkeitViewModel : DoePaAdminViewModelBase
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

        public ManageTaetigkeitViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            Taetigkeiten = new(Task.Run(async () => await DoePaAdminService.GetTaetigkeitenAsync()).Result);

            AddCommand = new AsyncRelayCommand(DoAddAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveCommand = new RelayCommand(DoRemove);
        }

        private void DoRemove()
        {
            if (SelectedTaetigkeit != null)
            {
                DoePaAdminService.RemoveTaetigkeit(SelectedTaetigkeit);
                _ = Taetigkeiten.Remove(SelectedTaetigkeit);
            }
        }

        private async Task DoAddAsync(CancellationToken cancellationToken = default)
        {
            Taetigkeit taetigkeit = await DoePaAdminService.CreateTaetigkeitAsync(cancellationToken);
            taetigkeit.Taetigkeitsbeschreibung = "Neue Tätigkeit";

            Taetigkeiten.Add(taetigkeit);
        }
    }
}
