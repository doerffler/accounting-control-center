using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.API;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ReceiveMitarbeiterPerformanceViewModel : DoePaAdminViewModelBase
    {
        #region InvoicedHours
        private IEnumerable<EmployeeInvoicedHours> _invoicedHours;
        public IEnumerable<EmployeeInvoicedHours> InvoicedHours
        {
            get => _invoicedHours;
            set => SetProperty(ref _invoicedHours, value, true);
        }
        #endregion

        public ReceiveMitarbeiterPerformanceViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {

        }

        public async Task<IEnumerable<EmployeeInvoicedHours>> GetEmployeeInvoicedHours(string email, DateTime from, DateTime to)
        {            
            return await DoePaAdminService.GetEmployeeInvoicedHours(email, from, to, default);
        }
    }
}
