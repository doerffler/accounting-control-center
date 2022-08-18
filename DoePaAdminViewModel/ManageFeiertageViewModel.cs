using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.APIFeiertage;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.Input;
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
        public IRelayCommand ImportDataCommand { get; }

        #region Feiertag
        private ObservableCollection<Datum> _datuemer = new();
        
        public ApiReciever<Feiertage> ApiReciever { get; set; }

        public ObservableCollection<Datum> Datuemer
        {
            get => _datuemer;
            set => SetProperty(ref _datuemer, value, true);
        }
        #endregion

        public ManageFeiertageViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            Datuemer = new(Task.Run(async () => await DoePaAdminService.GetDatuemerAsync()).Result);
         
            ApiReciever = new ApiReciever<Feiertage>("https://get.api-feiertage.de/?years=2022");

            ImportDataCommand = new AsyncRelayCommand(ImportDataAsync);
        }

        private async Task ImportDataAsync()
        {
            Feiertage Response = await ApiReciever.ReadData();
        }
    }
}
