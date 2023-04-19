using CommunityToolkit.Mvvm.Input;
using ACC.ViewModel.Services;
using ACC.ViewModel.Messages;
using ACCDataModel.DTO;
using System.Collections.ObjectModel;
using System.Windows;

namespace ACC.ViewModel
{
    public class ExportChartDataViewModel : ACCViewModelBase
    {
        private ObservableCollection<RemainingBudgetOnOrdersDTO> _data;
        public ObservableCollection<RemainingBudgetOnOrdersDTO> Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        public IRelayCommand CopyToClipboardCommand { get; }
        
        public ExportChartDataViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {
            Messenger.Register<ExportChartDataViewModel, ExportMessage, string>(this, "ExportChartData", (r, m) => r.OnExportMessageReceive(m));
        }

        private void OnExportMessageReceive(ExportMessage message)
        {
            GetData(message);
        }

        private void GetData(ExportMessage message)
        {
            Data = new ObservableCollection<RemainingBudgetOnOrdersDTO>(message.Data);
            Messenger.Send(new StatusbarMessage("ExportChartDataViewModel loaded"), "Statusbar");
        }
    }
}