﻿using DoePaAdmin.ViewModel.Model;
using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.DataMigration;
using DoePaAdminDataModel.DPApp;
using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private OutgoingInvoiceEnumerable _outgoingInvoices = new();

        public OutgoingInvoiceEnumerable OutgoingInvoices
        {
            get => _outgoingInvoices;
            set => SetProperty(ref _outgoingInvoices, value, true);
        }

        public ImportOutgoingInvoicesViewModel(IDoePaAdminService doePaAdminService, IDPAppService dpAppService, IUserInteractionService userInteractionService) : base(doePaAdminService, userInteractionService)
        {
            DPAppService = dpAppService;

            IEnumerable<OutgoingInvoiceMigration> outgoingInvoiceMigrations = Task.Run(async () => await DPAppService.GetOutgoingInvoicesAsync()).Result;

            Task.Run(async () => await MapDPAppMasterdataAsync(outgoingInvoiceMigrations));

            OutgoingInvoices = new(outgoingInvoiceMigrations);
        }

        private async Task MapDPAppMasterdataAsync(IEnumerable<OutgoingInvoiceMigration> outgoingInvoiceMigrations, CancellationToken cancellationToken = default)
        {
            IEnumerable<Kostenstelle> listCostCenters = await DoePaAdminService.GetKostenstellenAsync(cancellationToken);

            foreach (OutgoingInvoiceMigration currentInvoice in outgoingInvoiceMigrations)
            {
                //Map cost centers first:
                foreach (OutgoingInvoicePositionMigration currentPosition in currentInvoice.OutgoingInvoicePositions)
                {
                    int? costCenterNo = currentPosition.OutgoingInvoicePositionForImport.RelatedCostCenter != null ? currentPosition.OutgoingInvoicePositionForImport.RelatedCostCenter.Number : null;

                    if (costCenterNo.HasValue)
                    {
                        currentPosition.RelatedKostenstelle = listCostCenters.Where(cc => cc.KostenstellenNummer.Equals(costCenterNo.Value)).FirstOrDefault();
                    }

                }



                //Order position next (maybe use the RAID?)

            }

        }

    }
}
