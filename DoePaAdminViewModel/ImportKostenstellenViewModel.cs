using DoePaAdmin.ViewModel.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Data;

namespace DoePaAdmin.ViewModel
{
    public class ImportKostenstellenViewModel : ObservableRecipient
    {

        private IDPAppService DPAppService { get; set; }

        private DataView _costCenterData;
        public DataView CostCenterData
        {
            get => _costCenterData;
            set => SetProperty(ref _costCenterData, value, true);
        }

        public ImportKostenstellenViewModel(IDPAppService dpAppService)
        {
            DPAppService = dpAppService;
            CostCenterData = dpAppService.GetCostCentersAsync().Result.DefaultView;
        }

    }
}
