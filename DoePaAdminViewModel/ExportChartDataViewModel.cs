using DoePaAdmin.ViewModel.Services;
using DoePaAdmin.ViewModel.Messages;

namespace DoePaAdmin.ViewModel
{
    public class ExportChartDataViewModel : DoePaAdminViewModelBase
    {
        public object Data { get; set; }
        
        public ExportChartDataViewModel(IDoePaAdminService doePaAdminService, IUserInteractionService userInteractionService) : base(doePaAdminService, userInteractionService)
        {
            Messenger.Register<ExportChartDataViewModel, ExportMessage, string>(this, "ExportChartData",static (r, m) => r.OnExportMessageReceive(m));
        }

        void OnExportMessageReceive(ExportMessage message)
        {
            Data = message.Data;
            
        }
    }
}