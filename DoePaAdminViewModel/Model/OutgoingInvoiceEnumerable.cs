using DoePaAdminDataModel.DPApp;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DoePaAdmin.ViewModel.Model
{
    public class OutgoingInvoiceEnumerable : ObservableCollection<OutgoingInvoice>
    {

        public OutgoingInvoiceEnumerable() : base()
        { }

        public OutgoingInvoiceEnumerable(IEnumerable<OutgoingInvoice> collection) : base(collection)
        { }

    }
}
