using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

        public ManageGeschaeftsjahreViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            Geschaeftsjahre = new(Task.Run(async () => await DoePaAdminService.GetGeschaeftsjahreAsync()).Result);
        }
    }
}