using ACCDataModel.DataMigration;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel.Services
{
    public interface IDPAppService
    {

        Task <IEnumerable<OutgoingInvoiceMigration>> GetOutgoingInvoicesAsync(CancellationToken cancellationToken = default);

        Task<DataTable> GetCostCentersAsync(CancellationToken cancellationToken = default);

    }
}
