using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.DPApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ImportOutgoingInvoicesViewModel : DoePaAdminViewModelBase
    {

        private IDPAppService _dpAppservice;

        private IDPAppService DPAppService
        {
            get { return _dpAppservice; }
            set { _dpAppservice = value; }
        }

        private IEnumerable<OutgoingInvoice> _outgoingInvoices = new List<OutgoingInvoice>();

        public IEnumerable<OutgoingInvoice> OutgoingInvoices
        {
            get => _outgoingInvoices;
            set => SetProperty(ref _outgoingInvoices, value, true);
        }

        public ImportOutgoingInvoicesViewModel(IDoePaAdminService doePaAdminService, IDPAppService dpAppService) : base(doePaAdminService)
        {
            DPAppService = dpAppService;

            OutgoingInvoices = Task.Run(async () => await DPAppService.GetOutgoingInvoicesAsync()).Result;
        }

    }
}
