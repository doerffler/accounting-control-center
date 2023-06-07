using ACCDataModel.DataMigration;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ACC.ViewModel.Model
{
    public class IncomingInvoiceEnumerable : ObservableCollection<IncomingInvoiceMigration>
    {

        public IncomingInvoiceEnumerable() : base()
        { }

        public IncomingInvoiceEnumerable(IEnumerable<IncomingInvoiceMigration> collection) : base(collection)
        { }

    }
}
