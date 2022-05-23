using DoePaAdmin.ViewModel.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Options;
using DoePaAdminDataAdapter.DPApp;
using System.Threading.Tasks;
using System.Threading;
using DoePaAdminDataModel.DPApp;

namespace DoePaAdmin.ViewModel.Services
{
    public class DPAppService : IDPAppService
    {

        public string DPAppConnectionString { get; set; }

        public DPAppService(IOptions<DPAppConnectionSettings> dpAppSettings)
        {
            DPAppConnectionString = dpAppSettings.Value.ConnectionString;
        }

        public async Task <IEnumerable<OutgoingInvoice>> GetOutgoingInvoicesAsync(CancellationToken cancellationToken = default)
        {
            OutgoingInvoiceDAL dal = new(DPAppConnectionString);
            MasterdataDAL mdDal = new(DPAppConnectionString);
            
            Task<IEnumerable<OutgoingInvoice>> outgoingInvoicesTask = dal.ReadOutgoingInvoicesAsync(cancellationToken);
            Task<IEnumerable<OutgoingInvoicePosition>> outgoingInvoicePositionsTask = dal.ReadOutgoingInvoicePositionsAsync(cancellationToken);
            Task<IEnumerable<CostCenter>> costCentersTask = mdDal.ReadCostCentersAsync(cancellationToken);

            await Task.WhenAll(outgoingInvoicesTask, outgoingInvoicePositionsTask, costCentersTask);

            IEnumerable<OutgoingInvoice> outgoingInvoices = outgoingInvoicesTask.Result;
            IEnumerable<OutgoingInvoicePosition> outgoingInvoicePositions = outgoingInvoicePositionsTask.Result;
            IEnumerable<CostCenter> costCenters = costCentersTask.Result;

            foreach (OutgoingInvoice currentInvoice in outgoingInvoices)
            {
                currentInvoice.RelatedInvoicePositions = outgoingInvoicePositions.Where(oip => oip.RelatedInvoiceId.Equals(currentInvoice.Id)).ToList();
            }

            return outgoingInvoices;
        }

        public async Task<DataTable> GetCostCentersAsync(CancellationToken cancellationToken = default)
        {
            MasterdataDAL dal = new(DPAppConnectionString);
            return await dal.ReadCostCenterAsync(cancellationToken);
        }

    }
}
