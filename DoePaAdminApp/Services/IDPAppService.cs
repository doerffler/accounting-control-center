using DoePaAdminDataAdapter.DPApp.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DoePaAdminApp.Services
{
    public interface IDPAppService
    {

        ICollection<OutgoingInvoice> GetAusgangsrechnungen();

        Task<DataTable> GetCostCentersAsync();

    }
}
