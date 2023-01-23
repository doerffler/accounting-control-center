using CommunityToolkit.Mvvm.Input;
using DoePaAdmin.ViewModel.Services;
using DoePaAdmin.ViewModel.Messages;
using System.Collections;
using DoePaAdminDataModel.DTO;
using System.Collections.ObjectModel;

namespace DoePaAdmin.ViewModel
{
    public class ExportChartDataViewModel : DoePaAdminViewModelBase
    {
        public ObservableCollection<RemainingBudgetOnOrdersDTO> Data { get; set; }
        
        public IRelayCommand ExportCsvCommand { get; }
        public IRelayCommand ExportXlsxCommand { get; }
        
        public ExportChartDataViewModel(IDoePaAdminService doePaAdminService, IUserInteractionService userInteractionService) : base(doePaAdminService, userInteractionService)
        {
            Messenger.Register<ExportChartDataViewModel, ExportMessage, string>(this, "ExportChartData", static (r, m) => r.OnExportMessageReceive(m));
 
            ExportCsvCommand = new RelayCommand(ExportCsv);
            ExportXlsxCommand = new RelayCommand(ExportXlsx);
        }

        void OnExportMessageReceive(ExportMessage message)
        {
            Data = new ObservableCollection<RemainingBudgetOnOrdersDTO>(message.Data);
        }
        
        private void ExportCsv()
        {
            
        }
        
        private void ExportXlsx()
        {
            
        }
    }
}