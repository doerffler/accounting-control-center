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
    public abstract class ManageGeschaeftspartnerViewModel<T> : DoePaAdminViewModelBase where T : Geschaeftspartner, new()
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

        public ManageGeschaeftspartnerViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {

            AddGeschaeftspartnerCommand = new AsyncRelayCommand(DoAddGeschaeftspartnerAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveGeschaeftspartnerCommand = new RelayCommand(DoRemoveGeschaeftspartner);

            Geschaeftspartner = new(Task.Run(async () => await DoePaAdminService.GetGeschaeftspartnerAsync<T>()).Result);

        }

        public async Task DoAddGeschaeftspartnerAsync(CancellationToken cancellationToken = default)
        {
            T newGeschaeftspartner = await DoePaAdminService.CreateGeschaeftspartnerAsync<T>(cancellationToken);
            Geschaeftspartner.Add(newGeschaeftspartner);
        }

        public void DoRemoveGeschaeftspartner()
        {
            DoePaAdminService.RemoveGeschaeftspartner(SelectedGeschaeftspartner);
            Geschaeftspartner.Remove(SelectedGeschaeftspartner);
            SelectedGeschaeftspartner = null;
        }

    }
}
