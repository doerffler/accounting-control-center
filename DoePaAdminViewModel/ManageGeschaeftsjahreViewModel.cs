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
    public class ManageGeschaeftsjahreViewModel : DoePaAdminViewModelBase
    {
        #region Geschäftsjahr
        private ObservableCollection<Geschaeftsjahr> _geschaeftsjahre = new();
        public ObservableCollection<Geschaeftsjahr> Geschaeftsjahre
        {
            get => _geschaeftsjahre;
            set => SetProperty(ref _geschaeftsjahre, value, true);
        }

        private Geschaeftsjahr _selectedGeschaeftsjahr;
        public Geschaeftsjahr SelectedGeschaeftsjahr
        {
            get => _selectedGeschaeftsjahr;
            set => SetProperty(ref _selectedGeschaeftsjahr, value);
        }
        #endregion


        public IRelayCommand AddGeschaeftsjahrCommand { get; }
        public IRelayCommand RemoveGeschaeftsjahrCommand { get; }

        public ManageGeschaeftsjahreViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            Geschaeftsjahre = new(Task.Run(async () => await DoePaAdminService.GetGeschaeftsjahreAsync()).Result);

            AddGeschaeftsjahrCommand = new AsyncRelayCommand(DoAddGeschaeftsjahrAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveGeschaeftsjahrCommand = new RelayCommand(DoRemoveGeschaeftsjahr);
        }

        private async Task DoAddGeschaeftsjahrAsync(CancellationToken cancellationToken = default)
        {
            DateTime Zeit = DateTime.Now;

            Geschaeftsjahr geschaeftsjahr = await DoePaAdminService.CreateGeschaeftsjahrAsync(cancellationToken);
            geschaeftsjahr.Name = Zeit.Year.ToString();
            geschaeftsjahr.DatumVon = Zeit;
            geschaeftsjahr.DatumBis = Zeit;
            geschaeftsjahr.Rechnungsprefix = Zeit.Year.ToString();
            Geschaeftsjahre.Add(geschaeftsjahr);
        }

        private void DoRemoveGeschaeftsjahr()
        {
            if (SelectedGeschaeftsjahr != null)
            {
                _ = Geschaeftsjahre.Remove(SelectedGeschaeftsjahr);
            }
        }
    }
}