using CommunityToolkit.Mvvm.Input;
using DoePaAdmin.ViewModel.Services;
using DoePaAdmin.ViewModel.Messages;
using DoePaAdminDataModel.DTO;
using System.Collections.ObjectModel;
using System.Windows;

namespace DoePaAdmin.ViewModel
{
    public class ExportChartDataViewModel : DoePaAdminViewModelBase
    {
        private ObservableCollection<RemainingBudgetOnOrdersDTO> _data;
        public ObservableCollection<RemainingBudgetOnOrdersDTO> Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        public IRelayCommand CopyToClipboardCommand { get; }
        
        public ExportChartDataViewModel(IDoePaAdminService doePaAdminService, IUserInteractionService userInteractionService) : base(doePaAdminService, userInteractionService)
        {
            Messenger.Register<ExportChartDataViewModel, ExportMessage, string>(this, "ExportChartData", (r, m) => r.OnExportMessageReceive(m));
 
            CopyToClipboardCommand = new RelayCommand(CopyToClipboard);
        }

        void OnExportMessageReceive(ExportMessage message)
        {
            Data = new ObservableCollection<RemainingBudgetOnOrdersDTO>(message.Data);
        }
        
        private void CopyToClipboard()
        {
            Clipboard.Clear();
            
        }
    }
}