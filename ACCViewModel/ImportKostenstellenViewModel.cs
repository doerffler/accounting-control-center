using ACC.ViewModel.Services;
using ACCDataModel.Stammdaten;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel
{
    public class ImportKostenstellenViewModel : ObservableRecipient
    {

        private IDPAppService DPAppService { get; set; }

        private IACCService DoePaAdminService { get; set; }

        private DataView _costCenterData;
        public DataView CostCenterData
        {
            get => _costCenterData;
            set => SetProperty(ref _costCenterData, value, true);
        }

        private ObservableCollection<Kostenstelle> _kostenstellen = new();
        public ObservableCollection<Kostenstelle> Kostenstellen
        {
            get => _kostenstellen;
            set => SetProperty(ref _kostenstellen, value, true);
        }

        public ImportKostenstellenViewModel(IDPAppService dpAppService, IACCService accService, IUserInteractionService userInteractionService)
        {
            DPAppService = dpAppService;
            DoePaAdminService = accService;

            CancellationTokenSource source = new();
            CancellationToken token = source.Token;

            Task[] readDataTasks = new Task[2];
            Task<DataTable> getCostCentersDPApp = Task.Run(() => DPAppService.GetCostCentersAsync(token));
            readDataTasks[0] = getCostCentersDPApp;
            Task<IEnumerable<Kostenstelle>> getKostenstellenDoePaAdmin = Task.Run(() => DoePaAdminService.GetKostenstellenAsync(token));
            readDataTasks[1] = getKostenstellenDoePaAdmin;

            Task.WaitAll(readDataTasks, token);

            DataTable dtCostCenter = getCostCentersDPApp.Result;
            CostCenterData = dtCostCenter.DefaultView;

            Kostenstellen = new (getKostenstellenDoePaAdmin.Result);

        }

    }
}
