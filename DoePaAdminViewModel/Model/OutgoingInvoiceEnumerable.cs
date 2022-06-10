using DoePaAdminDataModel.DataMigration;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DoePaAdmin.ViewModel.Model
{
    public class OutgoingInvoiceEnumerable : ObservableCollection<OutgoingInvoiceMigration>
    {

        public OutgoingInvoiceEnumerable() : base()
        { }

        public OutgoingInvoiceEnumerable(IEnumerable<OutgoingInvoiceMigration> collection) : base(collection)
        { }

    }
}
