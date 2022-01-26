using DoePaAdminApp.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminApp.ViewModels
{
    public class ImportKostenstellenViewModel : ViewModelBase
    {

        private IDPAppService DPAppService { get; set; }

        private DataView _costCenterData;
        public DataView CostCenterData
        {
            get { return _costCenterData; }
            set
            {
                _costCenterData = value;
                RaisePropertyChanged(nameof(CostCenterData));
            }
        }

        public ImportKostenstellenViewModel(IDPAppService dpAppService)
        {
            DPAppService = dpAppService;

            CostCenterData = dpAppService.GetCostCentersAsync().
        }



    }
}
