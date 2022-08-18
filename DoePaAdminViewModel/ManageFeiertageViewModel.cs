using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ManageFeiertageViewModel : DoePaAdminViewModelBase
    {
        #region Feiertag
        private ObservableCollection<Datum> _datuemer = new();

        public ObservableCollection<Datum> Datuemer
        {
            get => _datuemer;
            set => SetProperty(ref _datuemer, value, true);
        }
        #endregion

        public ManageFeiertageViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            Datuemer = new(Task.Run(async () => await DoePaAdminService.GetDatuemerAsync()).Result);
        }
    }
}
