using DoePaAdmin.ViewModel.Model;
using DoePaAdminDataAdapter.DPApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Options;
using DoePaAdminDataAdapter.DPApp;
using System.Threading.Tasks;
using System.Threading;

namespace DoePaAdmin.ViewModel.Services
{
    public class DPAppService : IDPAppService
    {

        public string DPAppConnectionString { get; set; }

        public DPAppService(IOptions<DPAppConnectionSettings> dpAppSettings)
        {
            DPAppConnectionString = dpAppSettings.Value.ConnectionString;
        }

        public ICollection<OutgoingInvoice> GetAusgangsrechnungen()
        {
            throw new NotImplementedException();
        }

        public async Task<DataTable> GetCostCentersAsync(CancellationToken cancellationToken = default)
        {
            MasterdataDAL dal = new(DPAppConnectionString);
            return await dal.ReadCostCenterAsync(cancellationToken);
        }

    }
}
