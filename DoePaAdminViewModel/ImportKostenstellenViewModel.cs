using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ImportKostenstellenViewModel : ObservableRecipient
    {

        private IDPAppService DPAppService { get; set; }

        private IDoePaAdminService DoePaAdminService { get; set; }

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

        public ImportKostenstellenViewModel(IDPAppService dpAppService, IDoePaAdminService doePaAdminService)
        {
            DPAppService = dpAppService;
            DoePaAdminService = doePaAdminService;

            CancellationTokenSource source = new();
            CancellationToken token = source.Token;

            Task[] readDataTasks = new Task[2];
            Task<DataTable> getCostCentersDPApp = Task.Run(() => DPAppService.GetCostCentersAsync(token));
            readDataTasks[0] = getCostCentersDPApp;
            Task<ObservableCollection<Kostenstelle>> getKostenstellenDoePaAdmin = Task.Run(() => DoePaAdminService.GetKostenstellenAsync(token));
            readDataTasks[1] = getKostenstellenDoePaAdmin;

            Task.WaitAll(readDataTasks, token);

            DataTable dtCostCenter = getCostCentersDPApp.Result;
            CostCenterData = dtCostCenter.DefaultView;

            Kostenstellen = getKostenstellenDoePaAdmin.Result;

        }

    }
}
