using CommunityToolkit.Mvvm.Input;
using DoePaAdmin.ViewModel.Services;
using DoePaAdmin.ViewModel.Messages;
using DoePaAdminDataModel.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DoePaAdmin.ViewModel
{
    public class ExportChartDataViewModel : DoePaAdminViewModelBase
    {
        public ObservableCollection<RemainingBudgetOnOrdersDTO> Data { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public IRelayCommand CopyToClipboardCommand { get; }
        
        public ExportChartDataViewModel(IDoePaAdminService doePaAdminService, IUserInteractionService userInteractionService) : base(doePaAdminService, userInteractionService)
        {
            Messenger.Register<ExportChartDataViewModel, ExportMessage, string>(this, "ExportChartData", static (r, m) => r.OnExportMessageReceive(m));
 
            CopyToClipboardCommand = new RelayCommand(CopyToClipboard);
        }

        void OnExportMessageReceive(ExportMessage message)
        {
            Data = new ObservableCollection<RemainingBudgetOnOrdersDTO>(message.Data);
        }
        
        private void CopyToClipboard()
        {

        }
    }
}